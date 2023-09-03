using UnityEngine.SceneManagement;

namespace Cobilas.Unity.Management.Container {
    public interface ISceneContainerItem {
        void sceneUnloaded(Scene scene);
        void sceneLoaded(Scene scene, LoadSceneMode mode);
    }
}
