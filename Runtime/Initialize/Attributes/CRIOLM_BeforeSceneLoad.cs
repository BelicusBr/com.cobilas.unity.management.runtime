using System;

namespace Cobilas.Unity.Management.RuntimeInitialize {
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CRIOLM_BeforeSceneLoadAttribute : CRIOLMBaseAttribute {

        public CRIOLM_BeforeSceneLoadAttribute(int priority) : base(priority) {
            type = CRIOLMType.BeforeSceneLoad;
        }

        public CRIOLM_BeforeSceneLoadAttribute(CRIOLMPriority priorityType) : base(priorityType) {
            type = CRIOLMType.BeforeSceneLoad;
        }
        
        public CRIOLM_BeforeSceneLoadAttribute() : base() {
            type = CRIOLMType.BeforeSceneLoad;
        }
    }
}