using System;
using UnityEngine;
using System.Reflection;
using Cobilas.Collections;
using UnityEngine.SceneManagement;
using Cobilas.Unity.Management.Runtime;

namespace Cobilas.Unity.Management.Container {
    public sealed class SceneContainerManager : MonoBehaviour {
        [SerializeField] private ContainerItem[] itens;
        [SerializeField] private SceneContainer volatileContainer;
        [SerializeField] private SceneContainer permanentContainer;
    
        [StartBeforeSceneLoad("#SceneContainerManager")]
        private static void Init() {
            SceneContainerManager game = new GameObject("Scene container manager", typeof(SceneContainerManager)).GetComponent<SceneContainerManager>();
            DontDestroyOnLoad(game);
            
            Type[] types = TypeUtilitarian.GetTypes();
            for (int A = 0; A < ArrayManipulation.ArrayLength(types); A++)
                if (types[A].GetCustomAttribute<AddSceneContainerAttribute>() is AddSceneContainerAttribute addScene)
                    ArrayManipulation.Add(new ContainerItem(types[A].FullName, addScene.BuildIndex), ref game.itens);
            game.Ignition();
        }
    
        private void Ignition() {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            permanentContainer = new GameObject("Permanent container").AddComponent<SceneContainer>();
            DontDestroyOnLoad(permanentContainer);
            for (int I = 0; I < ArrayManipulation.ArrayLength(itens); I++)
                if (itens[I].Contains(-1))
                    permanentContainer.Add(itens[I]);
            SceneManager_sceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
    
        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(itens); I++) {
                if (itens[I].Contains(arg0.buildIndex)) {
                    if (volatileContainer == null)
                        volatileContainer = new GameObject("Volatile Container").AddComponent<SceneContainer>();
                    volatileContainer.Add(itens[I]);
                }
            }
        }
    
        private void OnDestroy() {
            for (int I = 0; I < ArrayManipulation.ArrayLength(itens); I++)
                itens[I].Dispose();
            ArrayManipulation.ClearArraySafe(ref itens);
            volatileContainer =
                permanentContainer = null;
        }
    
        [Serializable]
        public struct ContainerItem : IDisposable {
            [SerializeField] private string fullName;
            [SerializeField] private int[] buildIndex;
    
            public string FullName => fullName;
            public int[] BuildIndex => buildIndex;
    
            public ContainerItem(string fullName, int[] buildIndex) {
                this.fullName = fullName;
                this.buildIndex = buildIndex;
            }
    
            public bool Contains(int index) {
                for (int I = 0; I < ArrayManipulation.ArrayLength(buildIndex); I++)
                    if (buildIndex[I] == index)
                        return true;
                return false;
            }
    
            public void Dispose() {
                fullName = null;
                buildIndex = default;
            }
        }
    }
}