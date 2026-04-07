# Tests — Services (Application Layer)

Ce document décrit la stratégie de tests pour les services de l’application, en particulier
`ChefManagementService`, qui orchestre la validation, le mapping et l’accès au repository.

---

# 🟩 ChefManagementService

## Objectif
Assurer que le service :

- valide correctement les DTO via FluentValidation  
- applique les règles métier (ID forcé lors d’un update)  
- mappe correctement DTO ↔ Domain  
- appelle le repository avec les bonnes entités  
- remonte les exceptions appropriées  
- renvoie les bons DTO  

---

# 🟦 1. GetAllUsersAsync

| ID | Description | Input | Expected | Test |
|----|-------------|--------|----------|------|
| SRV-01 | Liste vide | Repo retourne `[]` | Retourne `[]` | `GetAllUsersAsync_Should_Return_Empty_List_When_Repo_Returns_Empty` |
| SRV-02 | Mapping correct | Repo retourne 2 Users | Retourne 2 UserDto mappés | `GetAllUsersAsync_Should_Map_Users_To_Dtos` |

---

# 🟦 2. AddUserAsync

| ID | Description | Input | Expected | Test |
|----|-------------|--------|----------|------|
| SRV-03 | Validation KO | DTO invalide | `ValidationException` | `AddUserAsync_Should_Throw_When_Validation_Fails` |
| SRV-04 | Ajout OK | DTO valide | Repo appelé + DTO retourné | `AddUserAsync_Should_Add_User_And_Return_Dto` |

---

# 🟦 3. UpdateUserAsync

| ID | Description | Input | Expected | Test |
|----|-------------|--------|----------|------|
| SRV-05 | Validation KO (1+ erreurs) | DTOUpdate invalide | `ValidationException` | `UpdateUserAsync_Should_Throw_When_Validation_Fails` |
| SRV-06 | Update OK | DTOUpdate valide | Repo appelé + DTO mis à jour | `UpdateUserAsync_Should_Update_User_And_Return_Updated_Dto` |
| SRV-07 | ID forcé | DTOUpdate.Id ≠ existing.Id | ID final = existing.Id | `UpdateUserAsync_Should_Update_User_And_Return_Updated_Dto_With_Fixed_ID` |

---

# 🟦 Notes importantes

### ✔️ Validation
Le service utilise FluentValidation.  
En cas d’erreurs multiples, `ValidationException` contient **toutes** les erreurs.

### ✔️ ID forcé lors d’un update
`userUpdate.Id` est systématiquement remplacé par `existing.Id`.  
Ce comportement est testé explicitement (SRV‑07).

### ✔️ Mapping
Le service utilise les extensions du `UserMapper` :

- `User → UserDto`
- `UserDto → User`

Les tests vérifient que les entités envoyées au repository sont correctes.

### ✔️ Repository
Le repository est mocké via NSubstitute.  
Les tests vérifient :

- le nombre d’appels  
- les valeurs des entités passées en argument  

---

# 🟩 Résultat

Cette documentation couvre :

- tous les flux du service  
- tous les cas d’erreur  
- tous les cas de succès  
- les règles métier (ID forcé)  
- les interactions avec le repository  
- les comportements attendus  

Elle est maintenant **parfaitement alignée** avec tes tests actuels.

