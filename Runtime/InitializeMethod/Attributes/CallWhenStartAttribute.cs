using System;
using Cobilas.Collections;

namespace Cobilas.Unity.Management.Runtime {
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class CallWhenStartAttribute : BaseLauncherAttribute {
        private string[] idCall;
        private InitializePriority priority;

        public string[] IDCall => idCall;
        public InitializePriority Priority => priority;
        public int Count => ArrayManipulation.ArrayLength(idCall);

        public CallWhenStartAttribute(InitializePriority priority, params string[] idCall) {
            this.idCall = idCall;
            this.priority = priority;
        }

        public CallWhenStartAttribute(params string[] idCall) :
            this(InitializePriority.Low, idCall) { }
    }
}