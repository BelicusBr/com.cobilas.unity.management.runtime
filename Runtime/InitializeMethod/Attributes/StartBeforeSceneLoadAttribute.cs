using System;

namespace Cobilas.Unity.Management.Runtime {
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class StartBeforeSceneLoadAttribute : BootPriorityAttribute {

        public StartBeforeSceneLoadAttribute(InitializePriority priority) :
            base(AffiliationPriority.StartBefore, priority) { }

        public StartBeforeSceneLoadAttribute(long priority) : 
            base(AffiliationPriority.StartBefore, priority) { }

        public StartBeforeSceneLoadAttribute(string idCall, InitializePriority priority) :
            base(idCall, AffiliationPriority.StartBefore, priority) { }

        public StartBeforeSceneLoadAttribute(string idCall, long priority) : 
            base(idCall, AffiliationPriority.StartBefore, priority) { }
    }
}