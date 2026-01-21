## ⚽ IFK MANAGER 2026 - Database & Management System

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet)
![Microsoft SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge&logo=dotnet)

Ett kraftfullt och visuellt tilltalande managementsystem för fotbollsligor, byggt med fokus på **Clean Code**, **SOLID-principer** och avancerad **SQL Server**-logik. Applikationen hanterar allt från spelarstatistik och löneförhandlingar till historiska transferloggar och systemaudit.
<img width="2816" height="1536" alt="erd" src="https://github.com/user-attachments/assets/6d46a04a-d5b9-4f64-a8ab-eb3041e30744" />
<img width="1108" height="628" alt="image" src="https://github.com/user-attachments/assets/294eaa03-f06a-4991-a4a1-0b9c1a468b8c" />
<img width="1111" height="623" alt="image" src="https://github.com/user-attachments/assets/efa09fa2-77fc-4f64-a899-ed54fb63ead9" />
<img width="1109" height="320" alt="image" src="https://github.com/user-attachments/assets/356d52da-9cef-45c3-9fd8-a38e91744c0b" />





---

## 🌟 Nyckelfunktioner

- **📊 Master-rapport**: En omfattande vy som aggregerar data från 5+ tabeller i realtid.
- **💰 Dynamisk Löneförhandling**: Uppdatera spelarkontrakt direkt från konsolen med omedelbar databasvalidering.
- **📜 Transferhistorik**: Fullständig spårbarhet av spelarrörelser mellan klubbar.
- **🛡️ MVG+ Audit Trail**: Avancerad loggning via **SQL Triggers** som fångar alla databasändringar (även de gjorda direkt i SSMS).
- **🎨 Spectre.Console UI**: Ett modernt CLI med interaktiva menyer, tabeller och färgkodad feedback.

---

## 🏗️ Arkitektur & Design

Projektet är strikt uppdelat enligt **Single Responsibility Principle (SRP)** för att säkerställa hög testbarhet och underhållsvänlighet:

- **`Data/`**: Innehåller `AppDbContext` och konfiguration för EF Core Fluent API.
- **`Models/`**: Domänmodeller som mappar mot databasens normaliserade struktur (3NF).
- **`Services/`**: Repository-mönster som kapslar in all dataåtkomstlogik.
- **`UI/`**: Presentationslager byggt med Spectre.Console för optimal UX.

### Databasdesign (ERD)
Systemet bygger på en robust relationell databasmodell designad för skalbarhet och dataintegritet.

![Entity Relationship Diagram](./erd.png)

---

## ⚡ Avancerad SQL Implementering

För att nå MVG-nivå har tyngdpunkten lagts på logik inuti SQL Server:

- **Audit Triggers**: Automatiserad loggning av löneändringar som inkluderar namn-lookup via JOINS inuti triggern.
- **Eager Loading**: Användning av `.Include()` och `.ThenInclude()` för att minimera antalet databasanrop (N+1 problemet).
- **Normalisering**: Databasen är designad i **3NF** för att eliminera redundans.

---

## 🚀 Installation & Setup

1. **Kör SQL-skripten**: Kör filerna `01_...` till `11_AuditLogging.sql` i din SQL Server Management Studio.
2. **Konfigurera Connection String**: Justera servernamnet i `AppDbContext.cs`:
   ```csharp
   optionsBuilder.UseSqlServer(@"Server=DIN_SERVER;Database=FootballLeagueDB;...");

3. **Kör applikationen**:
   Öppna terminalen i projektmappen och kör följande kommando:
   ```bash
   dotnet run

## 👤 Utvecklare

**Klas Olsson** - .NET System Developer

- 🌐 Portfolio: [klasolsson.se](https://klasolsson.se)
- 📧 Email: klasolsson81@gmail.com
- 💼 LinkedIn: [linkedin.com/in/klasolsson81](https://www.linkedin.com/in/klasolsson81/)
- 🐙 GitHub: [@klasolsson81](https://github.com/klasolsson81)

## 🎓 Kursmål & Kravuppfyllnad

Projektet är utvecklat för att demonstrera avancerad kunskap inom databasteknik och systemutveckling:

* **CRUD-funktionalitet**: Fullständig implementation för att hantera spelare, kontrakt och loggar via EF Core.
* **Avancerad Querying**: Utnyttjar komplexa JOIN-operationer och aggregering (SUM/COUNT) för statistik.
* **Säkerhet & Audit Trail (MVG+)**: SQL Triggers säkerställer att ingen löneändring sker utan att sparas i en audit-logg med full historik.
* **Clean Code & SRP**: En arkitektur som separerar ansvar mellan Repository, Service och UI-lager.
* **UX i Konsolen**: Interaktivt gränssnitt med Spectre.Console som inkluderar felhantering och logisk sortering.

---
*Detta projekt är en del av utbildningen till .NET-utvecklare 2025-2027.*
