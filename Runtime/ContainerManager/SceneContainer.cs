using System;
using UnityEngine;
using Cobilas.Collections;
using Cobilas.Unity.Utility;
using UnityEngine.SceneManagement;

namespace Cobilas.Unity.Management.Container {
    public sealed class SceneContainer : MonoBehaviour {

        [SerializeField] private MonoBehaviour[] containers;
        [SerializeField, HideInInspector] private bool no_OnEnable;

        private void Awake() {
            no_OnEnable = false;
            OnEnable();
            no_OnEnable = true;
        }

        private void OnEnable() {
            if (no_OnEnable) {
                no_OnEnable = false;
                return;
            }
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        }

        public void Add(SceneContainerManager.ContainerItem item) {
            Type type = UnityTypeUtility.GetType(item.FullName);
            ArrayManipulation.Add((MonoBehaviour)gameObject.AddComponent(type), ref containers);
        }

        public void Clear() {
            for (int I = 0; I < ArrayManipulation.ArrayLength(containers); I++)
                Destroy(containers[I]);
        }

        private void SceneManager_sceneUnloaded(Scene arg0) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(containers); I++)
                (containers[I] as ISceneContainerItem).sceneUnloaded(arg0);
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(containers); I++)
                (containers[I] as ISceneContainerItem).sceneLoaded(arg0, arg1);
        }
    }
}