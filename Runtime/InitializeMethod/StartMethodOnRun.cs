using System;
using UnityEngine;
using System.Text;
using System.Reflection;
using Cobilas.Collections;
using Cobilas.Unity.Utility;
using System.Collections.Generic;
using Cobilas.Unity.Management.RuntimeInitialize;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Cobilas.Unity.Management.Runtime {
    public static class StartMethodOnRun {
        private static Dictionary<AffiliationPriority, BootContainer> boots;

#if UNITY_2019_1_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
#elif UNITY_2017_1_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        private static void Init() {
            boots = new Dictionary<AffiliationPriority, BootContainer>();
            boots.Add(AffiliationPriority.StartBefore, new BootContainer());
            boots.Add(AffiliationPriority.StartLater, new BootContainer());
            boots.Add(AffiliationPriority.AfterAssembliesLoaded, new BootContainer());
            boots.Add(AffiliationPriority.BeforeSplashScreen, new BootContainer());
            boots.Add(AffiliationPriority.SubsystemRegistration, new BootContainer());
            BuildStockList(boots);
        }
#if UNITY_2019_1_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void AfterAssembliesLoaded()
            => RunBoot(AffiliationPriority.AfterAssembliesLoaded, boots);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void BeforeSplashScreen()
            => RunBoot(AffiliationPriority.BeforeSplashScreen, boots);  
#endif
#if UNITY_2019_2_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void SubsystemRegistration()
            => RunBoot(AffiliationPriority.SubsystemRegistration, boots);
#endif
        //Antes do carregamento da cena
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
            => RunBoot(AffiliationPriority.StartBefore, boots);

        //Após o carregamento da cena
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoad()
            => RunBoot(AffiliationPriority.StartLater, boots);

        private static void RunBoot(AffiliationPriority target, Dictionary<AffiliationPriority, BootContainer> boots) {
            BootContainer temp = boots[target];
            for (int J = 0; J < temp.Count; J++) {
                try {
                    temp[J].Invok();
                } catch (Exception ex) {
                    Debug.LogException(ex);
                }
            }
        }
#if UNITY_EDITOR
        [MenuItem("Tools/Print order")]
        private static void Print() {
            Dictionary<AffiliationPriority, BootContainer> print = new Dictionary<AffiliationPriority, BootContainer>();
            print.Add(AffiliationPriority.StartBefore, new BootContainer());
            print.Add(AffiliationPriority.StartLater, new BootContainer());
            print.Add(AffiliationPriority.AfterAssembliesLoaded, new BootContainer());
            print.Add(AffiliationPriority.BeforeSplashScreen, new BootContainer());
            print.Add(AffiliationPriority.SubsystemRegistration, new BootContainer());
            BuildStockList(print);
            for (int I = 0; I < 5; I++) {
                BootContainer temp = print[(AffiliationPriority)I];
                StringBuilder builder = new StringBuilder();
                builder.Append("//");
                builder.Append((AffiliationPriority)I);
                builder.Append("=====================\n");
                for (int J = 0; J < temp.Count; J++)
                    builder.AppendFormat("{0}:{1}\n", temp[J].Info.ReflectedType, temp[J].Info);
                Debug.Log(builder.ToString());
            }
        }
#endif
        private static MethodInfo[] GetMethodWithAttribute<T>(Type[] types) where T : Attribute {
            MethodInfo[] Res = (MethodInfo[])null;
            for (int I = 0; I < ArrayManipulation.ArrayLength(types); I++) {
                MethodInfo[] methods = GetMethods(types[I]);
                for (int J = 0; J < ArrayManipulation.ArrayLength(methods); J++)
                    if (methods[J].GetCustomAttribute<T>() != null)
                        ArrayManipulation.Add(methods[J], ref Res);
            }
            return Res;
        }

        private static void BuildStockList(Dictionary<AffiliationPriority, BootContainer> boots) {
            Type[] types = UnityTypeUtility.GetAllTypes();
            MethodInfo[] startScene = GetMethodWithAttribute<StartBaseSceneLoadAttribute>(types);
            MethodInfo[] callMe = GetMethodWithAttribute<CallWhenStartAttribute>(types);

            for (int I = 0; I < ArrayManipulation.ArrayLength(startScene); I++) {
                StartBaseSceneLoadAttribute sbsla = startScene[I].GetCustomAttribute<StartBaseSceneLoadAttribute>();
                boots[sbsla.BootType].Add(new MethodManifest(startScene[I], sbsla.Order, sbsla.Priority));
            }

            Old_BuildStockList(types, boots);

            for (int I = 0; I < 5; I++)
                boots[(AffiliationPriority)I].Reorder();

            for (int I = 0; I < ArrayManipulation.ArrayLength(callMe); I++) {
                CallWhenStartAttribute call = callMe[I].GetCustomAttribute<CallWhenStartAttribute>();

                for (int J = 0; J < call.Count; J++)
                    for (int L = 0; L < 5; L++)
                        if (boots[(AffiliationPriority)L].ContainsIDCall(call.IDCall[J])) {
                            int index = boots[(AffiliationPriority)L].IDCallIndex(call.IDCall[J]);
                            index = index + (call.Priority == InitializePriority.High ? 0 : 1);
                            boots[(AffiliationPriority)L].Insert(index,
                                new MethodManifest(callMe[I], 0L, call.Priority));
                        }
            }

            Old_BuildCallMeList(types, boots);
        }

#pragma warning disable CS0612
        private static void Old_BuildCallMeList(Type[] types, Dictionary<AffiliationPriority, BootContainer> boots) {
            MethodInfo[] callMe = GetMethodWithAttribute<CRIOLM_CallWhenAttribute>(types);
            for (int I = 0; I < ArrayManipulation.ArrayLength(callMe); I++) {
                CRIOLM_CallWhenAttribute call = callMe[I].GetCustomAttribute<CRIOLM_CallWhenAttribute>();
                int index = 0;
                if (call.Target == callMe[I].ReflectedType) {
                    Debug.LogError($"[CRIOLM]CallWhen.Target: {callMe[I]}");
                    continue;
                }

                if (boots[AffiliationPriority.StartBefore].ContainsIDCall(call.Target.FullName)) {
                    index = boots[AffiliationPriority.StartBefore].IDCallIndex(call.Target.FullName);
                    index = call.Type == CRIOLMType.AfterSceneLoad ? index + 1 : index;

                    boots[AffiliationPriority.StartBefore].Insert(
                        index, new MethodManifest(callMe[I], 0L,
                         (call.Type == CRIOLMType.AfterSceneLoad) ?
                            InitializePriority.Low : InitializePriority.High));
                } else if (boots[AffiliationPriority.StartLater].ContainsIDCall(call.Target.FullName)) {
                    index = boots[AffiliationPriority.StartLater].IDCallIndex(call.Target.FullName);
                    index = call.Type == CRIOLMType.AfterSceneLoad ? index + 1 : index;
                    
                    boots[AffiliationPriority.StartLater].Insert(
                        index, new MethodManifest(callMe[I], 0L,
                         (call.Type == CRIOLMType.AfterSceneLoad) ?
                            InitializePriority.Low : InitializePriority.High));
                }
            }
        }

        private static void Old_BuildStockList(Type[] types, Dictionary<AffiliationPriority, BootContainer> boots) {
            MethodInfo[] startScene = GetMethodWithAttribute<CRIOLM_AfterSceneLoadAttribute>(types);
            ArrayManipulation.Add(GetMethodWithAttribute<CRIOLM_BeforeSceneLoadAttribute>(types), ref startScene);

            for (int I = 0; I < ArrayManipulation.ArrayLength(startScene); I++) {
                CRIOLMBaseAttribute batt = startScene[I].GetCustomAttribute<CRIOLMBaseAttribute>();
                long order = 0L;
                InitializePriority priority = default(InitializePriority);
                switch (batt.PriorityType) {
                    case CRIOLMPriority.Comum :
                        order = batt.Priority;
                        priority = InitializePriority.None;
                        break;
                    default:
                        priority = (InitializePriority)(((int)batt.PriorityType) + 1);
                        break;
                }
                if (batt is CRIOLM_AfterSceneLoadAttribute) {
                    boots[AffiliationPriority.StartLater].Add(new MethodManifest(
                        startScene[I], startScene[I].ReflectedType.FullName, order, priority));
                } else if (batt is CRIOLM_BeforeSceneLoadAttribute) {
                    boots[AffiliationPriority.StartBefore].Add(new MethodManifest(
                        startScene[I], startScene[I].ReflectedType.FullName, order, priority));
                }
            }
        }
#pragma warning restore CS0612

        private static MethodInfo[] GetMethods(Type type)
            => type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
    }
}