#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Cobilas.Unity.Management.Build {
    public static class CobilasEditorProcessor {
        public enum PriorityProcessor {
            High = 0,
            Middle = 1,
            Low = 2
        }

        public static event Action<PriorityProcessor> quitting;
        public static event Action<PriorityProcessor> projectChanged;
        public static event Action<PriorityProcessor> hierarchyChanged;
        public static event Action<PriorityProcessor, PauseState> pauseStateChanged;
        public static event Action<PriorityProcessor, PlayModeStateChange> playModeStateChanged;

        [InitializeOnLoadMethod]
        private static void Init() {
            EditorApplication.hierarchyChanged += () => {
                for (byte I = 0; I < 3; I++)
                    hierarchyChanged?.Invoke((PriorityProcessor)I);
            };
            EditorApplication.playModeStateChanged += (p) => {
                for (byte I = 0; I < 3; I++)
                    playModeStateChanged?.Invoke((PriorityProcessor)I, p);
            };
            EditorApplication.pauseStateChanged += (p) => {
                for (byte I = 0; I < 3; I++)
                    pauseStateChanged?.Invoke((PriorityProcessor)I, p);
            };
            EditorApplication.projectChanged += () => {
                for (byte I = 0; I < 3; I++)
                    projectChanged?.Invoke((PriorityProcessor)I);
            };
            EditorApplication.quitting += () => {
                for (byte I = 0; I < 3; I++)
                    quitting?.Invoke((PriorityProcessor)I);
            };
        }
    }
}
#endif