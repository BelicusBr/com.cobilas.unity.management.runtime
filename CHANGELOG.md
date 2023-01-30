# Changelog
## [1.0.12] - 30/01/2023
### Changed
- Remoção de atribuições desnecessárias.
- Simplificação de operações matemáticas.
- Transformando possiveis campos em `readonly`.
## [1.0.11] 24/11/2022
### Change
O atributo `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]`
foi adicionado em `void StartMethodOnRun.Init()` para a versão `UNITY_2019_2_OR_NEWER`.
## [1.0.10] 17/11/2022
### Change
O `CobilasRuntimeInitializeOnLoadMethod` foi substituido pelo `StartMethodOnRun`,</br>
o `StartMethodOnRun` mantem o suporte com o `CobilasRuntimeInitializeOnLoadMethod`.
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
- > A instrução `if (RunList == null) RunList = new Dictionary<CRIOLMType, RunItem>();` foi adicionada em `private static void Init(); private static void CheckCRIOLMPriority();`.
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
### Repositorio com.cobilas.unity.management.runtime iniciado
- Lançado para o GitHub