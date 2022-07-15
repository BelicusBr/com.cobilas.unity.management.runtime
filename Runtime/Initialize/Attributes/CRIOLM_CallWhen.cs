using System;

namespace Cobilas.Unity.Management.RuntimeInitialize {
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CRIOLM_CallWhenAttribute : CRIOLMBaseAttribute {
        private Type target;

        public Type Target => target;

        public CRIOLM_CallWhenAttribute(Type target, CRIOLMType type) : base() {
            this.target = target;
            this.type = type;
        }

        public CRIOLM_CallWhenAttribute(Type target) : this(target, CRIOLMType.AfterSceneLoad) { }
    }
}