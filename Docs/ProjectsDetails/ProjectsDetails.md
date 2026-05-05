# 📦 Détails des projets

## 🧬 Domain (`CrepeDuChef.Domain`)

Le **cœur métier pur**, sans dépendance externe.

Contient :
- Entities : `User`, `CrepesParty`
- Domain Services : `ChefRotationAlgorithm`
- ValueObjects : `ChefSelection`, etc.
- Interfaces : `IRandomProvider`, `ICrepePartyRepository`
- Exceptions : `EmptyFirstNameException`, `EmptyLastNameException`, `NoChefException`, `NoChefSelectionException`
- Extensions : `RandomExtensions`

➡️ **Aucune dépendance vers Application, Infrastructure ou MAUI**

---

## 🧠 Application (`CrepeDuChef.Application`)

La couche **cas d’usage**.  
Elle orchestre le métier et les implémentations techniques.

Contient :
- DTOs : `UserDto`, `UserDtoAdd`, `UserDtoUpdate`, `CrepesPartyDto`
- Services : `ChefManagementService`, `ChefRotationService`, `CrepePartyService`
- Orchestrator : `UserApplicationOrchestrator`
- Interfaces : `IChefManagementService`, `IChefRotationService`, `ICrepePartyService`, `IUserApplicationOrchestrator`
- Models : `CrepeDisplayItem`, `CrepePartySession`
- ValueObjects : `ChefSelectionResult`, `UserSelectionResult`, `UserFormData`, `UserOperationResult`, `OperationStatus`
- Validation : `UserDtoAddValidator`, `UserDtoUpdateValidator`
- Mappers : `UserMapper`, `CrepePartyMapper`
- Extensions : `ApplicationExtension`, `UserDtoExtension`, `UserDtoUpdateExtension`, `UserExtension`, `UserOperationResultExtensions`


➡️ **Ne dépend que du Domain**

---

## 🗄 Infrastructure (`CrepeDuChef.Infrastructure`)

La couche technique.

Contient :
- EF Core : `CrepeDbContext`, `CrepeDbContextDesignTimeFactory`
- Repositories : `CrepePartyRepository`
- Services : `DefaultRandomProvider`
- Migrations : `Init`, `SecureUserData`, `CrepeDbContextModelSnapshot`
- Extensions : `InfrastructureExtensions`, `RandomExtensions`

➡️ **Implémente les interfaces du Domain**

---
## 🌍 Localization (`CrepeDuChef.Localization`)
- `CrepePartyResources.resx` (+ `.fr`)
- `ValidationResources.resx` (+ `.fr`)
- `languages/Traduction.resx` (+ `.fr`)
---

## 🖥 ViewModels partagés (`CrepeDuChef.ViewModels`)
- ViewModels : `CrepeSessionsViewModel`, `SelectUsersDialogViewModel`, `UserEditorViewModel`, `UserSettingViewModel`
- Presenters : `IDialogPresenter`, `IUserFormPresenter`, `IUserSelectionPresenter`
---

## 🖥 MAUI (UI)

Organisation principale :

### MVVM
- ViewModels : `CrepeSessionsViewModelMauiAdapter`
- Views : `CrepeDuChefView`, `UserSettingView`, `MainPage`
- Models UI : `CrepePartyGroup`
 
### UI Logic
- Behaviors : `PulseAnimationBehavior`, `PulseManager`
- Converters : `IsTodayConverter`
- Mappers : `CrepePartySessionToGroupMapper`

### DI
- `MauiViewModelsModule`, `MauiViewsModule`, `MauiPresentersModule`, `MauiPopupsModule`, `DIValidator`

### Popups

UI/Popups/  
│  
├── Core/  
│   ├── `PopupCoordinator`  
│   └── `PopupOptionsFactory`  
├── Factories/  
│   ├── `IMessagePopupFactory`, `IUserFormPopupFactory`, `IUserSelectionPopupFactory`  
│   ├── `MessagePopupFactory`, `UserPopCommunautyFactory`, `UserSelectionPopupFactory`  
├── Presenters/  
│   ├── `MauiDialogPresenter`  
│   ├── `MauiUserFormPresenter`  
│   └── `MauiUserSelectionPresenter`  
├── ViewModels/  
│   └── `UserFormPopupViewModel`  
└── Views/  
    ├── `SelectUsersPopup`  
    ├── `TitledMessagePopup`  
    └── `UserFormPopup`
### Resources
- Fonts, Styles (`Styles.xaml`, `CrepeDuChefStyles.xaml`, `CrepeDuChefColors.xaml`)
- Images, Splash, Raw (`BingePanda.json`)

---
## 🖥 Avalonia (`CrepeDuChef.Avalonia`)
- App : `App.axaml`, `Program.cs`, `app.manifest`
- DI : `AvaloniaDialogsModule`, `AvaloniaPresentersModule`, `AvaloniaViewModelsModule`, `AvaloniaViewsModule`, `AvaloniaWindowing`, `MainWindowHolder`
- Dialogs :
  - Interfaces : `IDialog`
  - Factories : `AvaloniaMessageDialogFactory`, `AvaloniaUserFormDialogFactory`, `AvaloniaUserSelectionDialogFactory`
  - Message dialogs : `BaseMessageDialogWindow`, `BaseMessageDialogViewModel`, `MessageDialog`, `ErrorDialog`, `WarningDialog`
  - Windows : `SelectUsersDialog`, `UserEditorWindow`
  - Presenters : `AvaloniaDialogPresenter`, `AvaloniaUserFormPresenter`, `AvaloniaUserSelectionPresenter`
- Mappers : `CrepeDisplayToAvaMapper`, `CrepePartySessionToGroupMapper_Ava`
- Models UI : `CrepePartyGroup_Ava`, `CrepePartyItem_Ava`, `SelectableUser`
- Services : `DialogService`, `INavigationService`, `NavigationService`
- Styles : `ControlsStyles.axaml`
- ViewModels : `MainViewModel`, `CrepeSessionsViewModelAvaloniaAdapter`, `SelectUsersDialogViewModelAvaloniaAdapter`
- Views : `MainWindow`, `CrepeSessionsView`, `UserSettingsView`


---

## 🧪 Tests (`CrepeDuChef.Tests`)

Le projet `CrepeDuChef.Tests` contient les tests unitaires organisés par couche :

### Domain
- `ChefRotationAlgorithmTests`

### Application
- Services : `ChefManagementServiceTests`
- Mappers : `UserMapperTests`
- Validation :  
  - `UserDtoAddValidatorTests`  
  - `UserDtoUpdateValidatorTests`

### Fakes & Helpers
- Fakes : `FakeRandomProvider`, `FakeLocalizer`
- Helpers : `TestData`

### Technos :
- **xUnit**
- **FluentAssertions**
- **NSubstitute**

---