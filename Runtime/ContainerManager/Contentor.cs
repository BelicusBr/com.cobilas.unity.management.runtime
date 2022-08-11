﻿using System;
using UnityEngine;
using System.Reflection;
using System.Diagnostics;
using Cobilas.Unity.Mono;
using Cobilas.Collections;
using CCobilasConsole = Cobilas.CobilasConsole;

namespace Cobilas.Unity.Management.Container {
    public class Contentor : CobilasBehaviour {

        public Component AddComponent(Type component) {
            if (!VerifikInitContainerManager())
                throw new MethodAccessException("AddComponent was called outside of ContainerManager.Init()");
            return gameObject.AddComponent(component);
        }

        public T AddComponent<T>() where T : Component
            => (T)AddComponent(typeof(T));

        public bool ContainsComponent(Type component)
            => GetComponent(component) != null;

        public bool ContainsComponent<T>()
            => ContainsComponent(typeof(T));
        //Init
        private bool VerifikInitContainerManager() {
            StackFrame[] frames = CCobilasConsole.TrackMethod();
            for (int I = 0; I < ArrayManipulation.ArrayLength(frames); I++) {
                MethodBase method = frames[I].GetMethod();
                if (method.Name == "Init" && method.ReflectedType == typeof(ContainerManager))
                    return true;
            }

            return false;
        }
    }
}
