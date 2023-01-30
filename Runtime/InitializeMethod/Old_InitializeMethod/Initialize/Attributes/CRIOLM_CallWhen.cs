using System;

namespace Cobilas.Unity.Management.Runtime {
    [Obsolete]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CRIOLM_CallWhenAttribute : CRIOLMBaseAttribute {
        private readonly Type target;

        public Type Target => target;

        public CRIOLM_CallWhenAttribute(Type target, CRIOLMType type) : base(type) {
            this.target = target;
        }

        public CRIOLM_CallWhenAttribute(Type target) : this(target, CRIOLMType.AfterSceneLoad) { }
    }
}
