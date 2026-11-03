# ContentCMS

A small full-stack content management application by [Eduardo Salazar](https://eduardosalazar.tech/), built with ASP.NET Core and Angular on top of ASP.NET Boilerplate.

The custom content module connects an Angular rich-text editor to authenticated application services and SQL Server persistence. It demonstrates creating, editing, listing, and displaying pages, with validation and automated tests.

## What is implemented

- Create pages with a name and HTML content, or update an existing page by selecting it or entering its ID.
- Display saved pages and add newly created pages to the sidebar navigation.
- Validate required fields and maximum lengths: 128 characters for page names and 2,048 for HTML content.
- Return an application error when a requested content ID does not exist.
- Test content persistence, retrieval, validation, and frontend list updates.

Authentication, user and role management, application infrastructure, and the AdminBSB-based interface come from the ASP.NET Boilerplate starter. The content-specific domain model, services, editor, navigation integration, and tests are the project-specific additions.

## Code to review

| Area | Entry point |
| --- | --- |
| Domain model and constraints | [Content.cs](src/ContentCMS.Core/Contents/Content.cs) |
| Repository-backed persistence | [ContentManager.cs](src/ContentCMS.Core/Contents/ContentManager.cs) |
| Authenticated application service | [ContentAppService.cs](src/ContentCMS.Application/Contents/ContentAppService.cs) |
| Input validation | [UpsertContentInput.cs](src/ContentCMS.Application/Contents/Dtos/UpsertContentInput.cs) |
| Angular content editor | [ManageContentComponent](src/ContentCMS.Web.Host/src/app/manage-content/manage-content.component.ts) |
| Saved-page rendering | [ContentComponent](src/ContentCMS.Web.Host/src/app/content/content.component.ts) |
| Backend tests | [ContentAppService_Tests.cs](test/ContentCMS.Tests/Contents/ContentAppService_Tests.cs) |
| Frontend regression tests | [manage-content.component.spec.ts](src/ContentCMS.Web.Host/src/app/manage-content/manage-content.component.spec.ts) |

The save flow is Angular editor → generated service proxy → application service → content manager → Entity Framework Core → SQL Server. Read results travel back through the same layers as DTOs.

## Technology

| Layer | Stack |
| --- | --- |
| Backend | C#, ASP.NET Core 2.2, ASP.NET Boilerplate 4.9 |
| Frontend | Angular 8, TypeScript 3.4, RxJS 6, Angular Material, angular-editor |
| Persistence | Entity Framework Core 2.2, SQL Server |
| Tests | xUnit, Shouldly, EF Core InMemory, Jasmine, Karma |

This repository is a legacy portfolio sample. Its framework versions and starter configuration should be reviewed and upgraded before a production deployment.

## Run locally

### Prerequisites

- .NET Core SDK **2.2.207** and the .NET Core 2.2 runtime. The root `global.json` selects this SDK.
- Node.js **12.22.12** and npm **6.14.18** for the Angular 8 toolchain. The frontend directory includes `.nvmrc`.
- A local SQL Server or SQL Server Express LocalDB instance.
- Chrome for the headless frontend tests.

### Backend and database

From the repository root, configure a local database and matching server/client URLs. For example, in PowerShell:

```powershell
$env:ConnectionStrings__Default = 'Server=(localdb)\MSSQLLocalDB;Database=ContentCMS;Trusted_Connection=True;'
$env:App__ServerRootAddress = 'http://localhost:21021/'
$env:App__ClientRootAddress = 'http://localhost:4200/'
$env:App__CorsOrigins = 'http://localhost:4200'

dotnet restore ContentCMS.sln
dotnet run --project src/ContentCMS.Migrator/ContentCMS.Migrator.csproj -- -q
dotnet run --project src/ContentCMS.Web.Host/ContentCMS.Web.Host.csproj
```

The migrator creates/updates the configured local database and seeds the starter data. Use the same connection string for the migrator and host; keep machine-specific credentials out of committed files. The API runs at `http://localhost:21021`, with Swagger at `/swagger`.

### Frontend

In a second terminal:

```sh
cd src/ContentCMS.Web.Host
npm ci --ignore-scripts
npm start
```

Open `http://localhost:4200`. Its API URL is configured in `src/assets/appconfig.json`; update that file if you choose a different local API port. The generated API client is already checked in, so installation does not require package lifecycle scripts.

The checked-in lockfile is used by npm. Use the versions above when reproducing this legacy application.

## Verification

Backend tests use EF Core InMemory and do not require SQL Server:

```sh
dotnet test test/ContentCMS.Tests/ContentCMS.Tests.csproj
```

From `src/ContentCMS.Web.Host`, run the content-editor regression suite:

```sh
npm run test:content
```

It covers updating the first list entry, updating a later entry, handling an ID absent from the loaded list without writing a `-1` array property, and adding a newly created page to the list and navigation. This focused command does not run the unrelated starter component specs.

To compile the frontend for production:

```sh
npm run ng -- build --prod
```

## Attribution and license

Built on [ASP.NET Boilerplate](https://github.com/aspnetboilerplate/aspnetboilerplate) and its Angular starter, with the [AdminBSB theme](https://github.com/gurayyarar/AdminBSBMaterialDesign). The starter's account, role, and tenant infrastructure remains part of the codebase.

[MIT license](LICENSE).
