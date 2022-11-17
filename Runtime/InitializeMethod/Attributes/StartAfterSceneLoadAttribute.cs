using System;

namespace Cobilas.Unity.Management.Runtime {
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class StartAfterSceneLoadAttribute : BootPriorityAttribute {

        public StartAfterSceneLoadAttribute(InitializePriority priority) :
            base(AffiliationPriority.StartLater, priority) { }

        public StartAfterSceneLoadAttribute(long priority) :
            base(AffiliationPriority.StartLater, priority) { }

        public StartAfterSceneLoadAttribute(string idCall, InitializePriority priority) :
            base(idCall, AffiliationPriority.StartLater, priority) { }

        public StartAfterSceneLoadAttribute(string idCall, long priority) :
            base(idCall, AffiliationPriority.StartLater, priority) { }
    }
}