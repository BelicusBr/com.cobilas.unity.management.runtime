using System;
using UnityEngine;
using System.Reflection;
using Cobilas.Collections;
using Cobilas.Unity.Utility;
using UnityEngine.SceneManagement;
using Cobilas.Unity.Management.RuntimeInitialize;
using UEObject = UnityEngine.Object;

namespace Cobilas.Unity.Management.Container {
    public static class ContainerManager {

        public const string cmVersion = "1.5";

        private static Type[] typeAddToContainer;
        private static Contentor permanentContainer;
        private static Contentor container;

        private static Contentor PermanentContainer =>
            permanentContainer == (Contentor)null ? 
            permanentContainer = CreateContainer("Cobilas itens[Permanent Container]", true) : permanentContainer;

        private static Contentor Container =>
            container == (Contentor)null ?
            container = CreateContainer("Cobilas itens[Container]", false) : container;

        [CRIOLM_BeforeSceneLoad(CRIOLMPriority.Low)]
        private static void Init() {
            Type[] types = UnityTypeUtility.GetAllTypes();
            for (int A = 0; A < ArrayManipulation.ArrayLength(types); A++) {
                AddToPermanentContainerAttribute permanentContainer = null;
                if ((permanentContainer = types[A].GetCustomAttribute<AddToPermanentContainerAttribute>()) != null)
                    if (permanentContainer.DoNotDuplicate) {
                        if (!PermanentContainer.ContainsComponent(types[A]))
                            PermanentContainer.AddComponent(types[A]);
                    }
                    else PermanentContainer.AddComponent(types[A]);

                if (types[A].GetCustomAttribute<AddToContainerAttribute>() != null)
                    ArrayManipulation.Add(types[A], ref typeAddToContainer);
            }

            SceneManager.sceneLoaded += (S, L) => {
                AddToContainer(S.buildIndex);
            };
        }

        private static void AddToContainer(int indexScene) {
            for (int A = 0; A < ArrayManipulation.ArrayLength(typeAddToContainer); A++) {
                AddToContainerAttribute addToContainer = typeAddToContainer[A].GetCustomAttribute<AddToContainerAttribute>();
                if (addToContainer.Contains(indexScene))
                    if (addToContainer.DoNotDuplicate) {
                        if (!Container.ContainsComponent(typeAddToContainer[A]))
                            Container.AddComponent(typeAddToContainer[A]);
                    } else Container.AddComponent(typeAddToContainer[A]);
            }
        }

        private static Contentor CreateContainer(string name, bool dontDestroyOnLoad) {
            GameObject game = new GameObject(name);
            game.isStatic = true;
            game.SetPosition(Vector3.zero);
            if (dontDestroyOnLoad) UEObject.DontDestroyOnLoad(game);
            return game.AddComponent<Contentor>();
        }
    }
}
