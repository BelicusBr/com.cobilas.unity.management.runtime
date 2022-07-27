using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;
//using Cobilas.Unity.Mono;
using Cobilas.Collections;
using Cobilas.Unity.Utility;
using System.Collections.Generic;

namespace Cobilas.Unity.Management.RuntimeInitialize {

    public enum CRIOLMType {
        AfterSceneLoad = 0,
        BeforeSceneLoad = 1
    }

    public enum CRIOLMPriority {
        High = 0,
        Low = 1,
        Comum = 3
    }

    public static class CobilasRuntimeInitializeOnLoadMethod {

        public const string criolmVersion = "1.9";
        private static Dictionary<CRIOLMType, RunItem> RunList = new Dictionary<CRIOLMType, RunItem>();

#if UNITY_2017 || UNITY_2018
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init() {
            if (RunList == null) RunList = new Dictionary<CRIOLMType, RunItem>();
            GetCRIOLMPriority();
            BeforeSceneLoad();
            Application.quitting += ClearList;
        }
#elif UNITY_2019_1_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init() {
            if (RunList == null) RunList = new Dictionary<CRIOLMType, RunItem>();
            GetCRIOLMPriority();
            Application.quitting += ClearList;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        private static void BeforeSceneLoad() {
            RunItem runItem = RunList[CRIOLMType.BeforeSceneLoad];
            for (int I = 0; I < runItem.CountMethodItemList; I++)
                try { runItem[I].method.Invoke(null, (object[])null); } 
                catch (Exception e) { Debug.LogException(e); }
            RunList[CRIOLMType.BeforeSceneLoad].Dispose();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoad() {
            RunItem runItem = RunList[CRIOLMType.AfterSceneLoad];
            for (int I = 0; I < runItem.CountMethodItemList; I++)
                try { runItem[I].method.Invoke(null, (object[])null); }
                catch (Exception e) { Debug.LogException(e); }
            RunList[CRIOLMType.AfterSceneLoad].Dispose();
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Cobilas/Check CRIOLM priority")]
        private static void CheckCRIOLMPriority() {
            if (RunList == null) RunList = new Dictionary<CRIOLMType, RunItem>();
            GetCRIOLMPriority();
#warning CobilasBehaviour.ClearLog comentado
            //CobilasBehaviour.ClearLog();
            PrintCRIOLMPriority(CRIOLMType.BeforeSceneLoad);
            PrintCRIOLMPriority(CRIOLMType.AfterSceneLoad);
            RunList.Clear();
        }

        private static void PrintCRIOLMPriority(CRIOLMType type) {
            RunItem runItem = RunList[type];
            //CobilasBehaviour.print($"RuntimeInitializeLoadType:{type}");
            Debug.Log($"RuntimeInitializeLoadType:{type}");
            for (int I = 0; I < runItem.CountMethodItemList; I++) {
                string Res = null;
                if (runItem[I].CRIOLMBase.CompareType<CRIOLM_CallWhenAttribute>()) {
                    switch (runItem[I].CRIOLMBase.Type) {
                        case CRIOLMType.AfterSceneLoad:
                            Res = $"After {(runItem[I].CRIOLMBase as CRIOLM_CallWhenAttribute).Target.FullName}|";
                            break;
                        case CRIOLMType.BeforeSceneLoad:
                            Res = $"Before {(runItem[I].CRIOLMBase as CRIOLM_CallWhenAttribute).Target.FullName}|";
                            break;
                    }
                } else
                    switch (runItem[I].CRIOLMBase.PriorityType) {
                        case CRIOLMPriority.Comum:
                            Res = $"Priority:{runItem[I].CRIOLMBase.Priority}|";
                            break;
                        default:
                            Res = $"Priority:{runItem[I].CRIOLMBase.PriorityType}|";
                            break;
                    }
                Debug.Log($"{Res}Method name:{runItem[I].method.ReflectedType}/{runItem[I].method}");
                //CobilasBehaviour.print($"{Res}Method name:{runItem[I].method.ReflectedType}/{runItem[I].method}");
            }
            RunList[type].Dispose();
        }
#endif

        private static void GetCRIOLMPriority() {
            RunList.Add(CRIOLMType.BeforeSceneLoad, new RunItem());
            RunList.Add(CRIOLMType.AfterSceneLoad, new RunItem());
            List<int> BeforeSceneLoadOrder = new List<int>();
            List<int> AfterSceneLoadOrder = new List<int>();
            List<MethodItem> ListComum = new List<MethodItem>();
            List<MethodItem> PriorityList = new List<MethodItem>();
            List<MethodItem> CallWhenList = new List<MethodItem>();

            Type[] types = UnityTypeUtility.GetAllTypes();
            for (int A = 0; A < ArrayManipulation.ArrayLength(types); A++) {
                MethodInfo[] methods = GetMethodInfos(types[A]);
                for (int B = 0; B < ArrayManipulation.ArrayLength(methods); B++) {
                    CRIOLMBaseAttribute baseAttribute;
                    if ((baseAttribute = methods[B].GetCustomAttribute<CRIOLMBaseAttribute>()) != null) {
                        if (baseAttribute.CompareType(
                            typeof(CRIOLM_AfterSceneLoadAttribute),
                            typeof(CRIOLM_BeforeSceneLoadAttribute))) {
                            switch (baseAttribute.PriorityType) {
                                case CRIOLMPriority.Comum:
                                    switch (baseAttribute.Type) {
                                        case CRIOLMType.AfterSceneLoad:
                                            if (!AfterSceneLoadOrder.Contains(baseAttribute.Priority))
                                                AfterSceneLoadOrder.Add(baseAttribute.Priority);
                                            break;
                                        case CRIOLMType.BeforeSceneLoad:
                                            if (!BeforeSceneLoadOrder.Contains(baseAttribute.Priority))
                                                BeforeSceneLoadOrder.Add(baseAttribute.Priority);
                                            break;
                                    }
                                    ListComum.Add(new MethodItem(methods[B], baseAttribute));
                                    break;
                                default:
                                    PriorityList.Add(new MethodItem(methods[B], baseAttribute));
                                    break;
                            }
                        } else if (baseAttribute.CompareType<CRIOLM_CallWhenAttribute>()) {
                            CallWhenList.Add(new MethodItem(methods[B], baseAttribute));
                        }
                    }
                }
                ArrayManipulation.ClearArraySafe(ref methods);
            }

            BeforeSceneLoadOrder.Reorder();
            AfterSceneLoadOrder.Reorder();

            foreach (int item in new int[] { 0, 1 }) {
                CRIOLMType type = (CRIOLMType)item;
                RunItem runTemp = RunList[type];
                runTemp.StartLists();
                List<int> listTemp = null;
                switch (type) {
                    case CRIOLMType.AfterSceneLoad:
                        listTemp = AfterSceneLoadOrder;
                        break;
                    case CRIOLMType.BeforeSceneLoad:
                        listTemp = BeforeSceneLoadOrder;
                        break;
                }

                for (int A = 0; A < listTemp.Count; A++)
                    for (int B = 0; B < ListComum.Count; B++)
                        if (ListComum[B].CRIOLMBase.Type == type)
                            if (ListComum[B].CRIOLMBase.Priority == listTemp[A])
                                runTemp.methods.Add(ListComum[B]);

                RunList[type] = runTemp;
            }

            foreach (int item in new int[] { 0, 1 }) {
                CRIOLMType type = (CRIOLMType)item;
                RunItem runTemp = RunList[type];
                runTemp.StartLists();
                for (int A = 0; A < PriorityList.Count; A++)
                    if(PriorityList[A].CRIOLMBase.Type == type)
                        switch (PriorityList[A].CRIOLMBase.PriorityType) {
                            case CRIOLMPriority.High:
                                runTemp.methods.Insert(0, PriorityList[A]);
                                break;
                            case CRIOLMPriority.Low:
                                runTemp.methods.Add(PriorityList[A]);
                                break;
                        }

                RunList[type] = runTemp;
            }

            RunItem runTemp1 = RunList[CRIOLMType.BeforeSceneLoad];
            RunItem runTemp2 = RunList[CRIOLMType.AfterSceneLoad];
            runTemp1.StartLists();
            runTemp2.StartLists();
            while (CallWhenList.Count > 0) {
                for (int A = 0; A < CallWhenList.Count; A++) {
                    CRIOLM_CallWhenAttribute call = (CRIOLM_CallWhenAttribute)CallWhenList[A].CRIOLMBase;
                    bool runTemp1ConRType = ContainsReflectedType(runTemp1.methods, call.Target);
                    bool runTemp2ConRType = ContainsReflectedType(runTemp2.methods, call.Target);
                    bool CallWhenListConRType = ContainsReflectedType(CallWhenList, call.Target);
                    if (CallWhenList[A].IsLoop || (!runTemp1ConRType && !runTemp2ConRType && !CallWhenListConRType)) {
                        CallWhenList.RemoveRange(A, 1);
                        break;
                    }
                    bool call_Break = false;
                    if (runTemp1ConRType)
                        for (int B = 0; B < runTemp1.methods.Count; B++)
                            if (runTemp1.methods[B].ReflectedType == call.Target) {
                                switch (call.Type) {
                                    case CRIOLMType.AfterSceneLoad:
                                        runTemp1.methods.Insert(B + 1, CallWhenList[A]);
                                        break;
                                    case CRIOLMType.BeforeSceneLoad:
                                        runTemp1.methods.Insert(B, CallWhenList[A]);
                                        break;
                                }
                                CallWhenList.RemoveRange(A, 1);
                                call_Break = true;
                                break;
                            }
                    if (call_Break) break;
                    if (runTemp2ConRType)
                        for (int B = 0; B < runTemp2.methods.Count; B++)
                            if (runTemp2.methods[B].ReflectedType == call.Target) {
                                switch (call.Type) {
                                    case CRIOLMType.AfterSceneLoad:
                                        runTemp2.methods.Insert(B + 1, CallWhenList[A]);
                                        break;
                                    case CRIOLMType.BeforeSceneLoad:
                                        runTemp2.methods.Insert(B, CallWhenList[A]);
                                        break;
                                }
                                CallWhenList.RemoveRange(A, 1);
                                call_Break = true;
                                break;
                            }
                    if (call_Break) break;
                }
            }
            RunList[CRIOLMType.BeforeSceneLoad] = runTemp1;
            RunList[CRIOLMType.AfterSceneLoad] = runTemp2;

            ListComum.Clear();
            PriorityList.Clear();
            CallWhenList.Clear();
            AfterSceneLoadOrder.Clear();
            BeforeSceneLoadOrder.Clear();
            ListComum.Capacity = 0;
            PriorityList.Capacity = 0;
            CallWhenList.Capacity = 0;
            AfterSceneLoadOrder.Capacity = 0;
            BeforeSceneLoadOrder.Capacity = 0;

            ArrayManipulation.ClearArraySafe(ref types);
        }

        private static bool ContainsReflectedType(List<MethodItem> list, Type type) {
            for (int I = 0; I < (list == null ? 0 : list.Count); I++)
                if (list[I].ReflectedType == type)
                    return true;
            return false;
        }

        private static void ClearList() {
            //AfterSceneLoadList.Dispose();
            //BeforeSceneLoadList.Dispose();
            RunList.Clear();
            RunList = null;
        }

        private static MethodInfo[] GetMethodInfos(Type type)
            => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        private struct RunItem : IDisposable {
            public List<MethodItem> methods;
            public int CountMethodItemList => methods == null ? 0 : methods.Count;

            public MethodItem this[int index] => methods[index];

            public void StartLists() {
                if (methods == null)
                    methods = new List<MethodItem>();
            }

            public void Dispose() {
                if (methods != null) {
                    for (int I = 0; I < CountMethodItemList; I++)
                        methods[I].Dispose();
                    methods.Clear();
                    methods.Capacity = 0;
                }
            }
        }

        private struct MethodItem : IDisposable {
            public MethodInfo method;
            public CRIOLMBaseAttribute CRIOLMBase;
            public Type ReflectedType => method.ReflectedType;
            public bool IsLoop => CRIOLMBase.CompareType<CRIOLM_CallWhenAttribute>() ?
                (CRIOLMBase as CRIOLM_CallWhenAttribute).Target == ReflectedType : false;

            public MethodItem(MethodInfo method, CRIOLMBaseAttribute baseAttribute) {
                this.method = method;
                this.CRIOLMBase = baseAttribute;
            }

            public void Dispose() {
                method = (MethodInfo)null;
                CRIOLMBase = (CRIOLMBaseAttribute)null;
            }
        }
    }
}