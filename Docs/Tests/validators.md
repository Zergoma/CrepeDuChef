# Tests — Validators

Ce document décrit la stratégie de tests pour les validators de l’application.

---

# 🟩 UserDtoAddValidator

## Objectif
Valider les règles de création d’un utilisateur.

## Cas de test

| ID | Description | Input | Expected | Test automatisé |
|----|-------------|--------|----------|------------------|
| ADD-01 | FirstName vide | "" | Erreur | Should_Fail_When_FirstName_Is_Empty_Or_Whitespace |
| ADD-02 | FirstName whitespace | "   " | Erreur | Should_Fail_When_FirstName_Is_Empty_Or_Whitespace |
| ADD-03 | LastName vide | "" | Erreur | Should_Fail_When_LastName_Is_Empty_Or_Whitespace |
| ADD-04 | LastName whitespace | " " | Erreur | Should_Fail_When_LastName_Is_Empty_Or_Whitespace |
| ADD-05 | FirstName + LastName invalides | "" + "" | 2 erreurs | Should_Fail_When_FirstName_And_LastName_Are_Invalid |
| ADD-06 | Cas valide | "John", "Doe" | OK | Should_Pass_When_Data_Is_Valid |

---

# 🟩 UserDtoUpdateValidator

## Objectif
Valider les règles de mise à jour d’un utilisateur.

## Cas de test

| ID | Description | Input | Expected | Test automatisé |
|----|-------------|--------|----------|------------------|
| UPD-01 | FirstName vide | "" | Erreur | Should_Fail_When_FirstName_Is_Empty_Or_Whitespace |
| UPD-02 | FirstName whitespace | "   " | Erreur | Should_Fail_When_FirstName_Is_Empty_Or_Whitespace |
| UPD-03 | LastName vide | "" | Erreur | Should_Fail_When_LastName_Is_Empty_Or_Whitespace |
| UPD-04 | LastName whitespace | " " | Erreur | Should_Fail_When_LastName_Is_Empty_Or_Whitespace |
| UPD-05 | FirstName + LastName invalides | "" + "" | 2 erreurs | Should_Fail_When_FirstName_And_LastName_Are_Invalid |
| UPD-06 | Cas valide | Id=1, "John", "Doe" | OK | Should_Pass_When_Data_Is_Valid |
