using System;

namespace Cobilas.Unity.Management.RuntimeInitialize {
    [Obsolete]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CRIOLM_AfterSceneLoadAttribute : CRIOLMBaseAttribute {

        public CRIOLM_AfterSceneLoadAttribute(int priority) : base(priority) {
            type = CRIOLMType.AfterSceneLoad;
        }

        public CRIOLM_AfterSceneLoadAttribute(CRIOLMPriority priorityType) : base(priorityType) {
            type = CRIOLMType.AfterSceneLoad;
        }

        public CRIOLM_AfterSceneLoadAttribute() : base() {
            type = CRIOLMType.AfterSceneLoad;
        }
    }
}