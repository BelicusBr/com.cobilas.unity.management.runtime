using System;
using Cobilas.Collections;
using UnityEngine.SceneManagement;

namespace Cobilas.Unity.Management.Container {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AddSceneContainerAttribute : Attribute {
        private readonly int[] buildIndex;
    
        public int[] BuildIndex => buildIndex;
    
        /// <param name="buildIndex">-1 for all scenes.</param>
        public AddSceneContainerAttribute(params int[] buildIndex) {
            this.buildIndex = buildIndex;
        }
    
        /// <param name="buildIndex">-1 for all scenes.</param>
        public AddSceneContainerAttribute(int buildIndex) : this(new int[] { buildIndex }) { }
    
        /// <param name="buildIndex">None/none for all scenes.</param>
        public AddSceneContainerAttribute(params string[] sceneNames) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(sceneNames); I++) {
                if (sceneNames[I].ToLower() == "none") {
                    buildIndex = new int[] { -1 };
                    return;
                }
                Scene scene = SceneManager.GetSceneByName(sceneNames[I]);
                if (scene.IsValid())
                    ArrayManipulation.Add(scene.buildIndex, ref buildIndex);
            }
        }
    
        /// <param name="buildIndex">None/none for all scenes.</param>
        public AddSceneContainerAttribute(string sceneName) : this(new string[] { sceneName }) { }
    
        /// <summary>By default it is -1</summary>
        public AddSceneContainerAttribute() : this(-1) { }
    }
}
