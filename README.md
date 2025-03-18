# BLOC4-API

L'API du BLOC4, permet d'accéder aux données de la base contenant des salariés et les sites/services auxquels ils sont affectés.
Les routes POST/UPDATE/DELETE nécessitent un Token pour être utilisées, un nouveau Token est généré à chaque connexion et devient NULL après une déconnexion.

## Technologies
- Langage : C# 
- Framework : ASP .Net Core
- Dépendences : BCrypt.Net-Next 4.0.3, Microsoft.EntityFrameworkCore 8.0.2, Pomelo.EntityFrameworkCore.MySql 8.0.2, Swashbuckle.AspNetCore 6.5.0
- Runtime : Net 8.0
- Plateforme : Windows

## Procédure d'installation
Prérequis :
- MySQL/MariaDB installé et lancés
- Runtime compatible DotNet 8.0
- Extracteur d'archive compatible avec .7z


	
#### 1. Télécharger la dernière version
Depuis ce dépot, dans l'onglet Release, télécharger l'archive 7z "BLOC4-API-vX.X.X-net8.0" où X.X.X correspond au numéro de version.

#### 2. Extraire l'archive 
Extraire l'archive .7z à l'emplacement de votre choix.

#### 3. Importer la base de donnée MYSQL 
En utilisant le terminal ou votre logiciel de gestion de base de données, importer le schéma *bloc4db.sql*.

Le port par défaut est le port **5290**, il peut être changé depuis le fichier *appsettings.Production.json* 

