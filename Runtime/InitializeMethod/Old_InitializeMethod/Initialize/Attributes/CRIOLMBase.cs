using System;

namespace Cobilas.Unity.Management.Runtime {
    public class CRIOLMBaseAttribute : Attribute {
        private readonly int priority;
        protected readonly CRIOLMType type;
        protected readonly CRIOLMPriority priorityType;

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

        protected CRIOLMBaseAttribute(int priority, CRIOLMType type) : this(priority) {
            this.type = type;
        }

        protected CRIOLMBaseAttribute(CRIOLMPriority priorityType, CRIOLMType type) : this(priorityType) {
            this.type = type;
        }

        protected CRIOLMBaseAttribute(CRIOLMType type) : this(0, type) { }

        protected CRIOLMBaseAttribute() : this(0) { }
    }
}
