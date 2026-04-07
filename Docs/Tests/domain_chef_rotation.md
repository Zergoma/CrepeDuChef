# 🟦 Tests — Domain: ChefRotationAlgorithm

Ce document décrit la stratégie de tests pour l’algorithme de rotation des chefs
(`ChefRotationAlgorithm.SelectNextChef`), situé dans la couche Domain.

Objectifs :
- Vérifier les erreurs Domain
- Vérifier la logique de session
- Vérifier la sélection du prochain chef
- Vérifier le comportement avec `IRandomProvider`
- Vérifier les cas limites et les règles avancées

------------------------------------------------------------
# 🟦 1. Cas d’erreur
------------------------------------------------------------

| ID     | Description            | Input               | Expected               | Test                                                         |
|--------|------------------------|---------------------|------------------------|--------------------------------------------------------------|
| ROT-01 | Aucun chef             | `allChefs = []`     | `NoChefException`      | `SelectNextChef_NoChef_ShouldThrowNoChefException`           |
| ROT-02 | Chefs null             | `allChefs = null`   | `ArgumentNullException`| `SelectNextChef_ChefIsNull_ShouldThrowArgumentNullException` |
| ROT-03 | Sessions null          | `sessionCrepes=null`| `ArgumentNullException`| `SelectNextChef_SessionChefIsNull_ShouldThrowArgumentNullException` |
| ROT-04 | Random null            | `random = null`     | `ArgumentNullException`| `SelectNextChef_RandomIsNull_ShouldThrowArgumentNullException` |

------------------------------------------------------------
# 🟦 2. Cas simples
------------------------------------------------------------

| ID     | Description   | Input | Expected                 | Test |
|--------|---------------|--------|--------------------------|------|
| ROT-10 | Un seul chef  | `[1]`  | Chef 1 + session++       | Dans `SelectNextChef_AllChefs` |

------------------------------------------------------------
# 🟦 3. Cas normaux (tous les chefs disponibles)
------------------------------------------------------------

Tests regroupés dans `SelectNextChef_AllChefs`.

| ID     | Description                    | Chefs        | Session     | Random | Expected Session | Expected Chef |
|--------|--------------------------------|--------------|-------------|--------|------------------|----------------|
| ROT-20 | 2 chefs, pas de session        | `[1,2]`      | `[]`        | 0/1    | stable           | 1/2            |
| ROT-21 | 2 chefs, session partielle     | `[1,2]`      | `[1]`       | any    | stable           | 2              |
| ROT-22 | 2 chefs, session complète      | `[1,2]`      | `[1,2]`     | any    | +1               | != dernier     |
| ROT-23 | 3 chefs, session partielle     | `[1,2,3]`    | `[1]`       | any    | stable           | 2 ou 3         |
| ROT-24 | 3 chefs, session complète      | `[1,2,3]`    | `[1,2,3]`   | any    | +1               | != dernier     |
| ROT-25 | Random fixe                    | `[1,2,3]`    | `[]`        | 0/1/2  | stable           | 1/2/3          |

------------------------------------------------------------
# 🟦 4. Cas avancés (availableChefs)
------------------------------------------------------------

Tests regroupés dans `SelectNextChef_WithAvailableChefs`.

| ID     | Description                        | All          | Session     | Available   | Random | Expected Session | Expected Chef |
|--------|------------------------------------|--------------|-------------|-------------|--------|------------------|----------------|
| ROT-30 | Tous disponibles, pas de session   | `[1,2,3]`    | `[]`        | `[1,2,3]`   | 0      | stable           | 1              |
| ROT-31 | Tous disponibles, session partielle| `[1,2,3]`    | `[1]`       | `[1,2,3]`   | any    | stable           | 2              |
| ROT-32 | Tous disponibles, session complète | `[1,2,3]`    | `[1,2,3]`   | `[1,2,3]`   | any    | +1               | != dernier     |
| ROT-33 | 1 seul disponible                  | `[1,2,3]`    | `[]`        | `[1]`       | any    | +1               | 1              |
| ROT-34 | 1 seul disponible + session part.  | `[1,2,3]`    | `[1]`       | `[1]`       | any    | +1               | 1              |
| ROT-35 | 2 disponibles, session partielle   | `[1,2,3]`    | `[1]`       | `[1,2]`     | any    | stable           | 2              |
| ROT-36 | 2 disponibles, session complète    | `[1,2,3]`    | `[1,2]`     | `[1,2]`     | any    | +1               | != dernier     |

------------------------------------------------------------
# 🟦 5. Cas limites
------------------------------------------------------------

| ID     | Description                           | Input              | Expected                     | Test |
|--------|---------------------------------------|--------------------|------------------------------|------|
| ROT-50 | `availableChefs = null`               | null               | NormalSelection              | `SelectNextChef_AvailableChefsNull_ShouldFallbackToNormalSelection` |
| ROT-51 | `availableChefs = []`                 | empty              | NormalSelection              | `SelectNextChef_AvailableChefsEmpty_ShouldFallbackToNormalSelection` |
| ROT-52 | Session contient un ID inconnu        | `[99]`             | ID ignoré                    | `SelectNextChef_SessionContainsUnknownId_ShouldIgnoreIt` |
| ROT-53 | Random index hors limites             | `random = 10`      | `ArgumentOutOfRangeException`| `SelectNextChef_RandomIndexTooLarge_ShouldThrow` |
| ROT-54 | Chefs non triés                       | `[3,1,2]`          | Respect ordre d’entrée       | `SelectNextChef_UnsortedChefs_ShouldRespectInputOrder` |
| ROT-55 | availableChefs avec doublons          | `[1,1,2]`          | Fonctionnement normal        | `SelectNextChef_AvailableChefsWithDuplicates_ShouldWorkNormally` |
| ROT-56 | session vide mais sessionId != 0      | sessionId = `10`   | sessionId conservé           | `SelectNextChef_EmptySessionButNonZeroSessionId_ShouldKeepSessionId` |

------------------------------------------------------------
# 🟦 6. Notes importantes
------------------------------------------------------------

- Random déterministe via `FakeRandomProvider`
- Une session est complète quand tous les chefs ont été utilisés
- Le dernier chef d’une session ne peut pas être le premier de la suivante
- `availableChefs` :
  - `null` -> NormalSelection
  - `[]` -> NormalSelection
  - `[1]` -> session++ + chef 1
  - `[1,2]` -> règles avancées

------------------------------------------------------------
# 🟦 7. Résultat
------------------------------------------------------------

Cette documentation couvre :
- cas d’erreur
- cas normaux
- cas avancés
- cas limites
- règles métier
- comportement random
- gestion des sessions

Elle est alignée avec les tests existants et les tests ajoutés.
