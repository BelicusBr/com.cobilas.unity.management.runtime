using System;
using Cobilas.Collections;

namespace Cobilas.Unity.Management.Runtime {
    internal sealed class BootContainer {
        private MethodManifest[] manifests;

        public int Count => ArrayManipulation.ArrayLength(manifests);

        public MethodManifest this[int index] => manifests[index];

        public void Add(MethodManifest manifest)
            => ArrayManipulation.Add(manifest, ref manifests);

        public void Insert(int index, MethodManifest manifest)
            => ArrayManipulation.Insert(manifest, index, ref manifests);

        public int IDCallIndex(string idCall) {
            for (int I = 0; I < ArrayManipulation.ArrayLength(manifests); I++)
                if (this[I].IDCall == idCall)
                    return I;
            return -1;
        }

        public bool ContainsIDCall(string idCall)
            => IDCallIndex(idCall) >= 0;

        public void Clear() {
            for (int I = 0; I < ArrayManipulation.ArrayLength(manifests); I++)
                manifests[I].Dispose();
            ArrayManipulation.ClearArraySafe(ref manifests);
        }

        public void Reorder() {
            MethodManifest[] newManifests = new MethodManifest[0];

            for (int I = 0; I < ArrayManipulation.ArrayLength(manifests); I++) {
                if (manifests[I].Priority != InitializePriority.None) continue;
                bool add = true;
                for (int J = 0; J < ArrayManipulation.ArrayLength(newManifests); J++) {
                    if (manifests[I].Order < newManifests[J].Order) {
                        ArrayManipulation.Insert(manifests[I], J, ref newManifests);
                        add = false;
                        break;
                    }
                }
                if (add)
                    ArrayManipulation.Add(manifests[I], ref newManifests);
            }

            manifests = newManifests;
        }
    }
}