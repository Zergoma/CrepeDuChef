# Tests — Mappers (Application)

Ce document décrit la stratégie de tests pour les mappers Domain ↔ DTO.

---

# 🟩 UserMapper

## Objectif
Assurer la conversion correcte entre les entités Domain (`User`) et les DTO (`UserDto`).

---

## 🟦 User → UserDto

| ID | Description | Input | Expected | Test |
|----|-------------|--------|----------|------|
| MAP-01 | Mapping complet | User valide | DTO identique | ToDto_Should_Map_All_Fields_Correctly |

---

## 🟦 UserDto → User

| ID | Description | Input | Expected | Test |
|----|-------------|--------|----------|------|
| MAP-02 | Mapping complet | DTO valide | User identique | ToEntity_Should_Map_All_Fields_Correctly |
| MAP-03 | FirstName invalide | "", " ", "\t"… | EmptyFirstNameException | ToEntity_Should_Throw_When_FirstName_Is_Invalid |
| MAP-04 | LastName invalide | "", " ", "\t"… | EmptyLastNameException | ToEntity_Should_Throw_When_LastName_Is_Invalid |
