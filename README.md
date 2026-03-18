# 🥞 CrepeDuChef

[![.NET](https://img.shields.io/badge/.NET-10.0-blue?logo=dotnet)](https://dotnet.microsoft.com/)
[![MAUI](https://img.shields.io/badge/MAUI-10.0.30-brightgreen?logo=dotnet)](https://learn.microsoft.com/dotnet/maui/)
[![xUnit](https://img.shields.io/badge/xUnit-2.4-orange?logo=xunit)](https://xunit.net/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---

## 🎯 Concept  & Fun

CrepeDuChef est une application MAUI, le but est simple : savoir **qui aura la crêpe du chef** tout en se faisant plaisir à coder.  
L'app permet de gérer les “chefs” et de décider de manière **aléatoire et équitable** qui aura la fameuse `crêpe du chef` (aka : la dernière qui a été faite, plus petite et avec une forme aléatoire).  
L’application garde en mémoire locale (sqlite) qui a déjà été choisi pour assurer une rotation juste.  

C’est surtout un **prétexte pour faire de la belle programmation** avec un framework moderne et puissant (MAUI 😎). Amusez-vous, explorez MAUI !!  
Si vous aimez l’application, vous pouvez même **m’offrir une bière ou une pizza** 🍺🍕 !

💡 À terme, une version avec **persistance via WebAPI** est prévue.

---

## 📂 Structure de la solution

- CrepeDuChef.sln
  - CrepeDuChef.Common
    - DTOs
    - Interfaces
    - Mappers
  - CrepeDuChef.Common.Tests
    - Tests unitaires pour la logique métier (xUnit + FluentAssertions)
  - CrepeDuChef.Maui
    - Views
    - ViewModels
    - Resources
    - Services (UserDtoPopupService, DialogService, etc.)
  - CrepeDuChef.Maui.Tests
    - Tests MAUI pour services dépendants de la plateforme (Roadmap)

---

## 🛠 Principaux projets

| Projet | Description |
|--------|------------|
| CrepeDuChef.Common | Logique métier, DTOs, interfaces, mappers |
| CrepeDuChef.Common.Tests | Tests unitaires pour Common |
| CrepeDuChef.Maui | Projet MAUI multiplateforme, UI, popups, ressources |
| CrepeDuChef.Maui.Tests | Tests pour services MAUI – à ajouter plus tard |

---

## ⚡ Technologies & dépendances clés

- **.NET 10 / MAUI**  
- **MAUI Toolkit** : `CommunityToolkit.Maui`, `CommunityToolkit.Mvvm`  
- **UX & UI** : `UXDivers.Popups.Maui`, `UraniumUI.Material`, `SkiaSharp.Extended.UI.Maui`  
- **Persistence locale** : `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Sqlite`  
- **Logging / Essentials** : `Microsoft.Maui.Controls`, `Microsoft.Maui.Essentials`, `Microsoft.Extensions.Logging.Debug`  

> ✅ Les versions exactes sont gérées via NuGet.

---

## 📐 Conventions / bonnes pratiques

- Mapper / validation dans `Common` → testable sans MAUI  
- Services dépendants de MAUI → testables via `Class Library MAUI` avec mocks  
- SemaphoreSlim pour séquentialiser les popups  
- Décorateurs et localisations injectées via DI  
- Tests unitaires **Common** séparés de tests **MAUI**, pour rester CI-friendly  

---

## 🚀 Build & Run

1. Ouvrir la solution dans Visual Studio 2026  
2. Définir `CrepeDuChef.Maui` comme projet de démarrage  
3. Sélectionner la plateforme souhaitée (Android, Windows, [je n'ai pas pu tester pour iOS, et Catalys])  

---

## ✅ Tests

- **Common** : `CrepeDuChef.Common.Tests` → tests rapides, CI-friendly  
- **MAUI** : `CrepeDuChef.Maui.Tests` → tests pour services MAUI, mock Popups/Dialogs (à ajouter plus tard)  

---

## 🛣 Roadmap

- Ajouter tests MAUI pour services dépendants de la plateforme  
- Version avec persistance WebAPI  
- Amélioration des popups et UI  
- Possibilité d’ajouter d’autres types de “sélections équitables” pour les chefs  

---

## 🎉 Fun

Le but est simple : savoir **qui aura la crêpe du chef** tout en se faisant plaisir à coder.  
Amusez-vous, explorez MAUI, et n’hésitez pas à me **payer une bière ou une pizza** si vous aimez l’app ! 🍕🍺