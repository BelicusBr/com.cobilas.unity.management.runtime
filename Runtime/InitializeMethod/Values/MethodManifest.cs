using System;
using System.Reflection;

namespace Cobilas.Unity.Management.Runtime {
    internal struct MethodManifest : IDisposable {
        private long order;
        private string idCall;
        private MethodInfo info;
        private InitializePriority priority;

        public long Order => order;
        public string IDCall => idCall;
        public MethodInfo Info => info;
        public InitializePriority Priority => priority;

        public MethodManifest(MethodInfo info, string idCall, long order, InitializePriority priority) {
            this.info = info;
            this.order = order;
            this.priority = priority;
            this.idCall = idCall;
        }

        public MethodManifest(MethodInfo info, long order, InitializePriority priority) :
            this(info, string.Empty, order, priority) { }
        
        public void Dispose() {
            order = default;
            info = (MethodInfo)null;
            priority = default;
            idCall = (string)null;
        }

        public void Invok()
            => info.Invoke((object)null, (object[])null);
    }
}
