using System;

namespace Cobilas.Unity.Management.Container {
    public abstract class AddToContainerBaseAttribute : Attribute {
        protected bool doNotDuplicate;
        public bool DoNotDuplicate => doNotDuplicate;

        protected AddToContainerBaseAttribute(bool doNotDuplicate)
            => this.doNotDuplicate = doNotDuplicate;
    }
}