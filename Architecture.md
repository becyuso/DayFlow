
# 專案架構

## 專案架構與設計
**務實 DDD　+ Modular Monolith + Cqrs + Transaction Boundary**

| ------------------------- | -------- | ----------------------------- |
|           名稱            |   類型   |           負責什麼            |
| ------------------------- | -------- | ----------------------------- |
| 務實 DDD (Pragmatic DDD)  | 設計方法 | 如何設計業務模型              |
| Modular Monolith          | 系統架構 | 如何組織整個系統              |
| CQRS                      | 架構模式 | 如何分離讀取與寫入            |
| Transaction Boundary      | 設計原則 | 如何控制交易(Transaction)範圍 |
| ------------------------- | -------- | ----------------------------- |


## 模組目錄參考
DayFlow.Modules.Identity 
│ ├─ Domain 
│ │  
│ │  ├─ Entities
│ │  
│ │  │  ├─ User.cs 
│ │  
│ │  ├─ Factory 
│ │  
│ │  │  ├─ UserFactory.cs 
│ │ 
│ │  ├─ ValueObjects 
│ │  │  
│ │  ├─ Enums 
│ │  │  
│ │  ├─ Events 
│ │  
│ ├─ Application 
│ │  
│ │  ├─ Features 
│ │  
│ │  │  ├─ Authentication
│ │  
│ │  │  │  ├─ LoginCommand.cs 
│ │  
│ │  │  │  ├─ LoginHandler.cs 
│ │  
│ │  ├─ Security 
│ │  
│ │  │  ├─ IPasswordHasher.cs 
│ │  
│ │  └─ IdentityApplication.cs 
│ │  
│ ├─ Infrastructure 
│ │  
│ │  ├─ Database
│ │  
│ │  │  │  ├─ IdentityDbContext.cs 
│ │  
│ │  ├─ Repositories
│ │  
│ │  │  │  ├─ UserRepository.cs 
│ │  
│ │  │  ├─ Security 
│ │  
│ │  ├─ IdentityInfrastructure.cs 
│ │  
│ ├─ Presentation 
│ │  
│ │  ├─ Controllers 
│ │  │  
│ │  ├─ ViewModels 
│ │  │  
│ │  └─ Views 
│ │     
│ └─ IdentityModule.cs 


## Api與Use Case業務邏輯對應參考
**模組Identity**
Application
└── Features
    └── Authentication
        └── Login
            ├── LoginCommand.cs(LoginResult.cs)
            ├── LoginHandler.cs

Presentation
└── Api
    └── Authentication
        └── Login
            ├── Endpoint.cs
            ├── Request.cs
            ├── Response.cs
            └── Mapping.cs