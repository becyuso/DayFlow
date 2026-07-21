
# 專案架構

## 專案架構與設計

**採用務實的 Domain-Driven Design (DDD)，並結合 Modular Monolith、CQRS、Transaction Boundary，以及 Clean Architecture 與 Vertical Slice Architecture 的混合式架構設計**

|           名稱            |   類型   |           負責什麼            |
| ------------------------- | -------- | ----------------------------- |
| 務實 DDD (Pragmatic DDD)  | 設計方法 | 如何設計業務模型              |
| Modular Monolith          | 系統架構 | 如何組織整個系統              |
| CQRS                      | 架構模式 | 如何分離讀取與寫入            |
| Transaction Boundary      | 設計原則 | 如何控制交易(Transaction)範圍 |

## 模組目錄參考（ASCII Tree）

**模組Identity**

```text
DayFlow.Modules.Identity
├── Application
│   ├── Features
│   │   └── Authentication
│   │       └── Login
│   │           ├── Command.cs
│   │           └── Handler.cs
│   ├── Security
│   │   └── IPasswordHasher.cs
│   ├── Repositories
│   └── IdentityApplication.cs
├── Domain
│   ├── Entities
│   │   └── User.cs
│   ├── Factory
│   │   └── UserFactory.cs
│   ├── ValueObjects
│   ├── Enums
│   └── Events
├── Infrastructure
│   ├── Database
│   │   └── IdentityDbContext.cs
│   ├── Repositories
│   │   └── UserRepository.cs
│   ├── Security
│   └── IdentityInfrastructure.cs
├── Presentation
│   ├── Api
│   │   └── Authentication
│   ├── Web
│   │   ├── Controllers
│   │   ├── ViewModels
│   │   └── Views
│   └── Resource
├── IdentityEndpoint.cs
└── IdentityModule.cs
```

## 模組目錄參考（AI參考專案）

**模組Identity**

```text
DayFlow.Modules.Identity/

Application/
    Features/
        Authentication/
            Login/
                Command.cs
                Handler.cs

    Security/
        IPasswordHasher.cs

    Repositories/

    IdentityApplication.cs

Domain/
    Entities/
        User.cs

    Factory/
        UserFactory.cs

    ValueObjects/

    Enums/

    Events/

Infrastructure/
    Database/
        IdentityDbContext.cs

    Repositories/
        UserRepository.cs

    Security/

    IdentityInfrastructure.cs

Presentation/
    Api/
        Authentication/

    Web/
        Controllers/
        ViewModels/
        Views/

    Resource/

IdentityEndpoint.cs

IdentityModule.cs
```

**模組Notes**

```text
DayFlow.Modules.Note/
    NoteModule.cs
    NoteEndpoint.cs

    Application/
        NoteApplication.cs
        Features/
            Notebook/
                Create/
                    Command.cs
                    Handler.cs
                Delete/
                    Handler.cs
                List/
                    Handler.cs
                    Query.cs

    Domain/
        Entities/
            Notebook.cs
            Note.cs
            Tag.cs
            NoteTag.cs

    Infrastructure/
        NoteInfrastructure.cs
        Database/
            NoteDbContext.cs
        Repositories/
            NotebookRepository.cs
            NotesRepository.cs
            NoteTagRepository.cs
            TagRepository.cs

    Presentation/
        Web/
            _ViewStart.cshtml
            _ViewImports.cshtml
            Notebook/
                Controllers/
                    NotebookController.cs
                ViewModels/
                    NotebookEditViewModel.cs
                    NotebookListItemViewModel.cs
                    NotebookViewModel.cs
                Views/
                    Notebook/
                        Index.cshtml
                        Create.cshtml
                        Edit.cshtml
                        Delete.cshtml
```

**DayFlow.Web**

```text
DayFlow.Web/
    DayFlow.Web.csproj
    Program.cs
    Controllers/
        HomeController.cs
    Models/
        ErrorViewModel.cs
    Views/
        _ViewImports.cshtml
        _ViewStart.cshtml
        Home/
            Index.cshtml
        Shared/
            _Layout.cshtml
    frontend/
        app/
            editor.ts
            editor.d.ts
        editors/
            tiptap/
                index.d.ts
        styles/
            editor.css
        types/                      <- 空資料夾（在 csproj 中列出）
        vite.config.ts
        package.json
        tsconfig.json
    wwwroot/
        lib/
            bootstrap/
                dist/...
            jquery/
                LICENSE.txt
```