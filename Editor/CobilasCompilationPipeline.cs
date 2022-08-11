using System;
using UnityEditor;
using UnityEditor.Compilation;

namespace Cobilas.Unity.Management.Build {
    public static class CobilasCompilationPipeline {

        public static event Action<object> compilationStarted;
        public static event Action<object> compilationFinished;
        public static event Action<string> assemblyCompilationStarted;
        public static event Action<string, CompilerMessage[]> assemblyCompilationFinished;

        [InitializeOnLoadMethod]
        private static void Init() {
            CompilationPipeline.compilationStarted += compilationStarted;
            CompilationPipeline.compilationFinished += compilationFinished;
            CompilationPipeline.assemblyCompilationStarted += assemblyCompilationStarted;
            CompilationPipeline.assemblyCompilationFinished += assemblyCompilationFinished;
        }
    }
}