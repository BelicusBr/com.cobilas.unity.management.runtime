using System;

namespace Cobilas.Unity.Management.RuntimeInitialize {
    public class CRIOLMBaseAttribute : Attribute {
        private int priority;
        protected CRIOLMType type;
        protected CRIOLMPriority priorityType;

        public int Priority => priority;
        public CRIOLMType Type => type;
        public CRIOLMPriority PriorityType => priorityType;

        protected CRIOLMBaseAttribute(int priority) {
            this.priority = priority;
            priorityType = CRIOLMPriority.Comum;
        }

        protected CRIOLMBaseAttribute(CRIOLMPriority priorityType) {
            this.priority = 0;
            this.priorityType = priorityType;
        }

        protected CRIOLMBaseAttribute() : this(0) { }
    }
}