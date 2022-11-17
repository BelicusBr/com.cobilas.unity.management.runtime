using System;

namespace Cobilas.Unity.Management.Runtime {
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class BootPriorityAttribute : StartBaseSceneLoadAttribute {
        private long order;
        private string idCall;
        private InitializePriority priority;
        private AffiliationPriority bootType;

        public override long Order => order;
        public override string IDCall => idCall;
        public override InitializePriority Priority => priority;
        public override AffiliationPriority BootType => bootType;

        public BootPriorityAttribute(string idCall, AffiliationPriority bootType, long order) {
            this.order = order;
            this.idCall = idCall;
            this.bootType = bootType;
        }

        public BootPriorityAttribute(string idCall, AffiliationPriority bootType, InitializePriority priority) :
            this(idCall, bootType, 0L) {
            this.priority = priority;
        }

        public BootPriorityAttribute(AffiliationPriority bootType, long order) :
            this(string.Empty, bootType, order) { }

        public BootPriorityAttribute(AffiliationPriority bootType, InitializePriority priority) :
            this(string.Empty, bootType, priority) { }
    }
}