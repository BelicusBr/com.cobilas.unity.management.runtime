using System;

namespace Cobilas.Unity.Management.Container {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AddToContainerAttribute : AddToContainerBaseAttribute {
        private readonly int[] indexScenes;

        public AddToContainerAttribute(bool doNotDuplicate, params int[] indexScenes) : base(doNotDuplicate)
            => this.indexScenes = indexScenes;

        public AddToContainerAttribute(params int[] indexScenes) : this(true, indexScenes) { }

        public bool Contains(int indexScene) {
            for (int I = 0; I < (indexScenes == null ? 0 : indexScenes.Length); I++)
                if (indexScenes[I] == indexScene)
                    return true;
            return false;
        }
    }
}
