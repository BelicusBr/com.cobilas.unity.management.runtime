# Changelog
## [2.1.0] 25/01/2024
### Changed
A change in package dependencies.
## [2.0.0] - 03/09/2023
### Changed
- `ContainerManager` has been replaced by `SceneContainerManager`.
### Removed
```c#
     public static class ContainerManager;
     public class Container;
     public sealed class AddToPermanentContainerAttribute;
     public abstract class AddToContainerBaseAttribute;
     public sealed class AddToContainerAttribute;
```
### Added
```c#
     public sealed class SceneContainerManager;
     public sealed class SceneContainer;
     public interface ISceneContainerItem;
     public sealed class AddSceneContainerAttribute;
```
## [1.15.0] - 29/08/2023
### Changed
- Package dependencies have been changed.
## [1.14.0-ch1] - 28/08/2023
### Changed
- The package author was changed from `Cobilas CTB` to `BélicusBr`.
## [1.0.12] - 30/01/2023
### Changed
- Removal of unnecessary assignments.
- Simplification of mathematical operations.
- Transforming possible fields into `readonly`.
###Fixed
- Now the `CRIOLM_BeforeSceneLoadAttribute` list is handled before being merged with the `startScene` list to avoid inserting null references.
## [1.0.11] 24/11/2021
###Change
The `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]` attribute
was added in `void StartMethodOnRun.Init()` for version `UNITY_2019_2_OR_NEWER`.
## [1.0.10] 17/11/2022
###Change
`CobilasRuntimeInitializeOnLoadMethod` has been replaced by `StartMethodOnRun`,</br>
`StartMethodOnRun` maintains support with `CobilasRuntimeInitializeOnLoadMethod`.
## [1.0.8] 13/08/2022
- Move Editor\CobilasBuildProcessor.cs > Runtime\Build\CobilasBuildProcessor.cs
- Move Editor\CobilasCompilationPipeline.cs > Runtime\Build\CobilasCompilationPipeline.cs
- Move Editor\CobilasEditorProcessor.cs > Runtime\Build\CobilasEditorProcessor.cs
## [1.0.8] 10/08/2022
- Merge com.cobilas.unity.management.container@1.0.5
- Merge com.cobilas.unity.management.build@1.0.5
## [1.0.7] 31/07/2022
- Add CHANGELOG.md
- Fix package.json
- Add Cobilas MG Runtime.asset
- Remove Runtime\DependencyWarning.cs
## [1.0.6] 27/07/2022
- Fix CHANGELOG.md
- Fix package.json
- Fix CobilasRuntimeInitializeOnLoadMethod.cs
- > The statement `if (RunList == null) RunList = new Dictionary<CRIOLMType, RunItem>();` was added in `private static void Init(); private static void CheckCRIOLMPriority();`.
## [1.0.5] 23/07/2022
- Add CHANGELOG.md
- Fix package.json
## [1.0.4] 23/07/2022
- Fix package.json
## [1.0.3] 22/07/2022
- Add Runtime/DependencyWarning.cs
- Fix LICENSE.md
- Fix Cobilas.Unity.Management.Runtime.asmdef
## [1.0.2] 17/07/2022
- Delete main.yml
- Delete README.md
- Fix package.json
## [1.0.0] 15/07/2022
- Add package.json
- Add LICENSE.md
- Add folder:Runtime
## [0.0.1] 15/07/2022
### Repository com.cobilas.unity.management.runtime started
- Released to GitHub