namespace Cobilas.Unity.Management.Runtime {
    public abstract class StartBaseSceneLoadAttribute : BaseLauncherAttribute {
        public abstract long Order { get; }
        public abstract string IDCall { get; }
        public abstract InitializePriority Priority { get; }
        public abstract AffiliationPriority BootType { get; }
    }
}