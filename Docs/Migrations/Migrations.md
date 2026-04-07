# 📌 Comment créer une migration EF Core (MAUI + SQLite)

---
Date création de 2026_04_01  
Auteur: Zergoma

---
## 1. Ouvrir un terminal dans la racine de la solution
> Package Manager Console
## 2. Lancer la commande suivante :
>dotnet ef migrations add NomDeLaMigration --project CrepeDuChef.Infrastructure

## 3. Ne pas exécuter database update

L’application applique automatiquement les migrations au démarrage via :
```csharp
db.Database.Migrate();
```

## 4. Où sont stockées les migrations ?
Dans :
>CrepeDuChef.Infrastructure/Migrations/

## 5. Pourquoi ça marche sans --startup-project ?

Parce que le projet contient un IDesignTimeDbContextFactory,
ce qui permet à EF Core de créer le DbContext sans charger MAUI.