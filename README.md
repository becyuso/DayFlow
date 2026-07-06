務實 DDD　+ Modular Monolith + Cqrs + Transaction Boundary

完整目錄結構參考
DayFlow.Modules.Identity
│
├─ Domain                                      // 領域層
│
│  ├─ Entities                                // 實體
│  │
│  │  ├─ User.cs                              // 使用者聚合根
│  │  ├─ RefreshToken.cs                      // Refresh Token
│  │  ├─ UserSession.cs                       // 登入工作階段
│  │  ├─ EmailVerificationToken.cs            // Email驗證Token
│  │  └─ PasswordResetToken.cs                // 密碼重設Token
│  │
│  ├─ ValueObjects                            // 值物件
│  │
│  │  ├─ UserEmail.cs                         // 使用者Email
│  │  ├─ PasswordHash.cs                      // 密碼雜湊
│  │  ├─ DisplayName.cs                       // 顯示名稱
│  │  ├─ RefreshTokenValue.cs                 // Refresh Token值
│  │  └─ VerificationTokenValue.cs            // 驗證Token值
│  │
│  ├─ Enums                                   // 列舉
│  │
│  │  ├─ UserStatus.cs                        // 使用者狀態
│  │  └─ SessionStatus.cs                     // 工作階段狀態
│  │
│  ├─ Events                                  // 領域事件
│  │
│  │  ├─ UserRegistered.cs                    // 使用者註冊
│  │  ├─ UserLoggedIn.cs                      // 使用者登入
│  │  ├─ UserLoggedOut.cs                     // 使用者登出
│  │  ├─ EmailVerified.cs                     // Email驗證成功
│  │  └─ PasswordResetCompleted.cs            // 密碼重設完成
│  │
│  ├─ Errors                                  // 領域錯誤
│  │
│  │  └─ IdentityErrors.cs                    // Identity錯誤定義
│  │
│  ├─ Repositories                            // Repository介面
│  │
│  │  ├─ IUserRepository.cs                   // 使用者Repository
│  │  ├─ IRefreshTokenRepository.cs           // RefreshToken Repository
│  │  └─ IUserSessionRepository.cs            // Session Repository
│  │
│  └─ Services                                // Domain Service
│
│      ├─ IPasswordHasher.cs                  // 密碼加密服務
│      ├─ IJwtTokenGenerator.cs               // JWT產生器
│      └─ ITokenGenerator.cs                  // Token產生器
│
├─ Application                                // 應用層
│
│  ├─ Commands                                // Command
│  │
│  │  ├─ RegisterUser                         // 使用者註冊
│  │  │  ├─ RegisterUserCommand.cs
│  │  │  └─ RegisterUserCommandHandler.cs
│  │  │
│  │  ├─ LoginUser                            // 使用者登入
│  │  │  ├─ LoginUserCommand.cs
│  │  │  └─ LoginUserCommandHandler.cs
│  │  │
│  │  ├─ LogoutUser                           // 使用者登出
│  │  │
│  │  ├─ VerifyEmail                          // 驗證Email
│  │  │
│  │  ├─ ForgotPassword                       // 忘記密碼
│  │  │
│  │  └─ ResetPassword                        // 重設密碼
│  │
│  ├─ Queries                                 // Query
│  │
│  │  ├─ GetCurrentUser                       // 取得目前使用者
│  │  │
│  │  ├─ GetUserProfile                       // 取得使用者資料
│  │  │
│  │  └─ GetUserSessions                      // 取得登入紀錄
│  │
│  ├─ DTOs                                    // 資料傳輸物件
│  │
│  │  ├─ UserDto.cs                           // 使用者DTO
│  │  ├─ LoginResultDto.cs                    // 登入結果DTO
│  │  └─ SessionDto.cs                        // Session DTO
│  │
│  ├─ Contracts                               // 模組對外契約
│  │
│  │  └─ IIdentityModule.cs                   // Identity模組介面
│  │
│  └─ Behaviors                               // MediatR Pipeline
│
├─ Infrastructure                             // 基礎設施層
│
│  ├─ Persistence                             // 資料存取
│  │
│  │  ├─ Configurations                       // EF Mapping
│  │  │
│  │  │  ├─ UserConfiguration.cs              // users表映射
│  │  │  ├─ RefreshTokenConfiguration.cs      // refresh_tokens映射
│  │  │  ├─ UserSessionConfiguration.cs       // user_sessions映射
│  │  │  ├─ EmailVerificationConfiguration.cs // email驗證映射
│  │  │  └─ PasswordResetConfiguration.cs     // 密碼重設映射
│  │  │
│  │  └─ Repositories                         // Repository實作
│  │
│  │      ├─ UserRepository.cs                // User Repository
│  │      ├─ RefreshTokenRepository.cs        // RefreshToken Repository
│  │      └─ UserSessionRepository.cs         // Session Repository
│  │
│  ├─ Authentication                          // 驗證實作
│  │
│  │  ├─ JwtTokenGenerator.cs                 // JWT產生器
│  │  ├─ PasswordHasher.cs                    // 密碼雜湊器
│  │  └─ TokenGenerator.cs                    // Token產生器
│  │
│  ├─ Notifications                           // 通知實作
│  │
│  │  ├─ EmailVerificationSender.cs           // Email驗證通知
│  │  └─ PasswordResetSender.cs               // 重設密碼通知
│  │
│  └─ BackgroundJobs                          // 背景工作
│
│      ├─ ExpiredTokenCleanupJob.cs           // Token清理工作
│      └─ SessionCleanupJob.cs                // Session清理工作
│
└─ Presentation                               // 對外API入口
   │
   ├─ Endpoints
   │
   │  ├─ RegisterUserEndpoint.cs              // 註冊API
   │  ├─ LoginUserEndpoint.cs                 // 登入API
   │  ├─ LogoutUserEndpoint.cs                // 登出API
   │  ├─ VerifyEmailEndpoint.cs               // Email驗證API
   │  ├─ ForgotPasswordEndpoint.cs            // 忘記密碼API
   │  └─ ResetPasswordEndpoint.cs             // 重設密碼API
   │
   └─ IdentityModule.cs                       // 模組註冊入口















   Database 
   └─ dayflow 
   ├─ Schema: identity 
   │ ├─ Table: identity.Users 
   │ └─ Table: identity.Roles 
   │ ├─ Schema: workflow 
   │ ├─ Table: workflow.Workflows 
   │ └─ Table: workflow.WorkflowSteps 
   │ ├─ Schema: task 
   │ ├─ Table: task.Tasks 
   │ └─ Table: task.TaskLogs 
   │ └─ Schema: notification 
   ├─ Table: notification.Messages 
   └─ Table: notification.Templates













   需要加入什麼參考？

你的 Module 不應該只是普通 Class Library。

1. Project SDK 改成 Razor SDK

DayFlow.Modules.Identity.csproj

<Project Sdk="Microsoft.NET.Sdk.Razor">

不是：

<Project Sdk="Microsoft.NET.Sdk">
2. 加 ASP.NET Core Framework Reference
<ItemGroup>
    <FrameworkReference Include="Microsoft.AspNetCore.App" />
</ItemGroup>

提供：

Controller
HttpContext
IActionResult
View()
ModelState
Razor MVC
3. 如果使用 MVC Controller

加入：

<ItemGroup>
    <PackageReference 
        Include="Microsoft.AspNetCore.Mvc.Core"
        Version="8.0.0" />
</ItemGroup>

但 .NET 6/7/8 通常不需要，FrameworkReference 已包含。

4. Web Host 引用 Module

DayFlow.Web.csproj

<ItemGroup>
    <ProjectReference Include="..\DayFlow.Modules.Identity\DayFlow.Modules.Identity.csproj" />
</ItemGroup>
5. 啟用 Module Controller 掃描

Web:

builder.Services
    .AddControllersWithViews()
    .AddApplicationPart(
        typeof(LoginController).Assembly);
6. Razor View 找不到時

需要：

builder.Services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();

以及 Module：

<PropertyGroup>
    <PreserveCompilationContext>true</PreserveCompilationContext>
</PropertyGroup>