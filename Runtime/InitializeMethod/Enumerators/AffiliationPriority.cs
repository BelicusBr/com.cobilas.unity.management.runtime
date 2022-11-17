namespace Cobilas.Unity.Management.Runtime {
    public enum AffiliationPriority {
        StartBefore = 0,
        StartLater = 1,
        AfterAssembliesLoaded = 2,
        BeforeSplashScreen = 3,
        SubsystemRegistration = 4
    }
}