
# 個人學習隨手筆記

## 下載後 Build:找不到 'vite/client' 的類型定義檔案。
專案 > 相依性 > npm > 右鍵 > 還原封裝

## TypeScript                                                    

**Microsoft.TypeScript.MSBuild** 

是一個 NuGet 套件，它將 TypeScript 編譯器 (tsc) 與 MSBuild 整合
專案: ASP.NET Framework、ASP.NET Core（使用 MSBuild 編譯 TypeScript）、MVC、Razor Pages

**Node Package Manager**

|            npm command             |                   description                  |
| ---------------------------------- | ---------------------------------------------- |
| npm install typescript --save-dev  | 接著 npx tsc --init (產生 tsconfig.json)       | 
| npm install vite --save-dev        | 用來開發和打包 JavaScript/TypeScript 應用程式  | 
| npm install -D @types/node         | 是 Node.js 的 TypeScript 型別定義              | 

## tiptap                                                    

**Node Package Manager**

|                  npm command                   | description |
| ---------------------------------------------- | ----------- |
| npm install @tiptap/core @tiptap/starter-kit   |             |
| npm install @tiptap/extension-image            | 插入圖片    |

## 執行npm

先 
cd /d "E:\Code\C#\DayFlow\專案名稱"
再操作npm指令，安裝套件。

## .NET CLI 的指令

先 cd /d "E:\Code\C#\DayFlow\專案名稱"

|          .NET CLI 指令          |                           description                             |
| ------------------------------- | ----------------------------------------------------------------- |
| dotnet package list	          | 顯示目前專案所安裝的 NuGet 套件。查看是否有可更新版本或過時套件。 | 
| dotnet package list --outdated  | 檢查有哪些套件可升級。                                            | 
| dotnet add package	          | 安裝 NuGet 套件。                                                 | 
| dotnet remove package           | 移除 NuGet 套件。                                                 | 
| dotnet restore	              | 還原 NuGet 套件。                                                 | 
| dotnet build	                  | 建置專案。                                                        | 
| dotnet run	                  | 執行專案。                                                        | 
| dotnet test	                  | 執行單元測試。                                                    | 
| dotnet clean	                  | 清除建置產物。                                                    | 
| dotnet nuget locals all --clear | 清除 NuGet 快取（解決套件異常時很有用）                           | 

## Other                                                  

```text
Modular Monolith

1. .csproj 加
<Project Sdk="Microsoft.NET.Sdk.Razor">

2. .csproj 加 ASP.NET Core Framework Reference
<ItemGroup>
    <FrameworkReference Include="Microsoft.AspNetCore.App" />
</ItemGroup>

3. 如果模組使用 MVC Controller
加入：
<ItemGroup>
    <PackageReference 
        Include="Microsoft.AspNetCore.Mvc.Core"
        Version="8.0.0" />
</ItemGroup>
但 .NET 6/7/8 通常不需要，FrameworkReference 已包含。

4. Web Host 引用 Module
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
```