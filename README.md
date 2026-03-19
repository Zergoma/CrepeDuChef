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

## 🧩 Architecture de la solution

- 📦 **Common**  
  DTOs, Models, Interfaces, Mappers, Extensions, Exceptions  

- 🧠 **Application**  
  Logique applicative et cas d’usage (Services)  

- 🗄️ **Infrastructure**  
  Accès aux données (Entities, Migrations, Extensions)  

- 🖥️ **Maui**  
  UI MAUI avec MVVM (Models, ViewModels, Views),  
  Behaviors, Converters, Services, PopupElements, Resources, Platforms  

- 🧪 **Tests**  
  Tests unitaires par couche : Application, Common, Infrastructure 

---

## ⚡ Technologies & dépendances clés

- **.NET 10 / MAUI**  
- **MAUI Toolkit** : `CommunityToolkit.Maui`, `CommunityToolkit.Mvvm`  
- **UX & UI** : `UraniumUI.Material`, `SkiaSharp.Extended.UI.Maui`  
- **Persistence locale** : `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Sqlite`  
- **Logging / Essentials** : `Microsoft.Maui.Controls`, `Microsoft.Maui.Essentials`, `Microsoft.Extensions.Logging.Debug`  


---

## 📐 Conventions / bonnes pratiques

- Mapper / validation dans `Common` → testable sans MAUI (planifié dans les prochaines versions)  
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
