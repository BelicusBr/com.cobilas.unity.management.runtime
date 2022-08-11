using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Cobilas.Unity.Management.Build {
    using PriorityProcessor = CobilasEditorProcessor.PriorityProcessor;
    public class CobilasBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport {

        public int callbackOrder => 0;

        public static event Action<PriorityProcessor, BuildReport> EventOnPreprocessBuild;
        public static event Action<PriorityProcessor, BuildReport> EventOnPostprocessBuild;

        /// <summary>
        /// Implemente esta função para receber um retorno de chamada antes que a compilação seja iniciada.
        /// </summary>
        public void OnPreprocessBuild(BuildReport report) {
            for (byte I = 0; I < 3; I++)
                EventOnPreprocessBuild?.Invoke((PriorityProcessor)I, report);
        }

        /// <summary>
        /// Implemente esta função para receber um retorno de chamada após a compilação ser concluída.
        /// </summary>
        public void OnPostprocessBuild(BuildReport report) {
            for (byte I = 0; I < 3; I++)
                EventOnPostprocessBuild?.Invoke((PriorityProcessor)I, report);
        }
    }
}