using System;

namespace Cobilas.Unity.Management.Runtime {
    [Obsolete]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CRIOLM_AfterSceneLoadAttribute : CRIOLMBaseAttribute {

        public CRIOLM_AfterSceneLoadAttribute(int priority) : base(priority, CRIOLMType.AfterSceneLoad) { }

        public CRIOLM_AfterSceneLoadAttribute(CRIOLMPriority priorityType) : base(priorityType, CRIOLMType.AfterSceneLoad) { }

        public CRIOLM_AfterSceneLoadAttribute() : base(CRIOLMType.AfterSceneLoad) { }
    }
}
