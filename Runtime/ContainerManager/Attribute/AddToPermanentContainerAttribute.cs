using System;

namespace Cobilas.Unity.Management.Container {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AddToPermanentContainerAttribute : AddToContainerBaseAttribute {
        public AddToPermanentContainerAttribute(bool doNotDuplicate) : base(doNotDuplicate) { }

        public AddToPermanentContainerAttribute() : base(true) { }
    }
}