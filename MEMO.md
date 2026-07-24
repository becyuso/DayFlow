
# 個人學習隨手筆記

## 下載後 Build:找不到 'vite/client' 的類型定義檔案。
專案 > 相依性 > npm > 右鍵 > 還原封裝

## 執行npm
先 
cd /d "E:\Project\DayFlow\DayFlow.Web\frontend"
再操作npm指令，安裝套件。

### 其它指令
|            npm command             |                   description                                          |
| ---------------------------------- | ---------------------------------------------------------------------- |
| npm run build                      | 執行專案設定好的「建置（build）」流程，產生可部署或正式使用的版本。    | 
| npm run dev                        | 啟動開發環境                                                           | 

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

|                  npm command                   | description           |
| ---------------------------------------------- | --------------------- |
| npm install @tiptap/core @tiptap/starter-kit   |                       |
| npm install @tiptap/extension-image            | 插入圖片              |
| npm install @tiptap/extension-text-style       | FontSize / FontFamily |
| npm install @tiptap/extension-font-family      | font-family           |

## toast                                                    

**Node Package Manager**

|                  npm command                   | description |
| ---------------------------------------------- | ----------- |
| npm install toastr                             |             |
| npm install @types/toastr --save-dev           |  型別定義   |

## .NET CLI 的指令

先 cd /d "E:\Project\DayFlow\專案名稱"

|          .NET CLI 指令          |                           description                             |
| ------------------------------- | ----------------------------------------------------------------- |
| dotnet package list	          | 顯示目前專案所安裝的 NuGet 套件。查看是否有可更新版本或過時套件。 | 
| dotnet package list --outdated  | 檢查有哪些套件可升級。                                            | 
| dotnet add package	          | 安裝 NuGet 套件。                                                 | 
| dotnet remove package           | 移除 NuGet 套件。                                                 | 
| dotnet restore	              | 還原 NuGet 套件。                                                 | 
| dotnet build	                  | 建置專案。                                                        | 
| dotnet run	                  | 執行專案。(建置階段查看錯誤)                                      | 
| dotnet test	                  | 執行單元測試。                                                    | 
| dotnet clean	                  | 清除建置產物。                                                    | 
| dotnet nuget locals all --clear | 清除 NuGet 快取（解決套件異常時很有用）                           | 

## 找出專案目前目錄底下所有 DLL 檔案（包含子目錄），移除 Windows 對它們標記的「來自網際網路」封鎖標記
PowerShell執行：
```text
Get-ChildItem "E:\Project\DayFlow" -Recurse | Unblock-File
```

## 檢查DLL是否可正常載入

Get-Item "E:\Project\DayFlow\DayFlow.Web\bin\Debug\net10.0\DayFlow.Modules.Notes.dll" -Stream *

## 其它訊息

事件檢視器 → 應用程式與服務記錄 → Microsoft → Windows → CodeIntegrity → Operational
PowerShell執行：
```text
Get-WinEvent -LogName "Microsoft-Windows-CodeIntegrity/Operational" -MaxEvents 5
```

## 其它問題  

Q: Windows啟用了 WDAC（Windows Defender Application Control）/ Code Integrity 企業簽章政策，要求載入的 DLL 必須符合「Enterprise signing level」要求。
A: Windows 11 較新版本有個 Smart App Control 功能，是微軟自己的 WDAC 應用，一旦開啟後沒有官方方式關閉，只能重灌系統才能徹底移除。它會封鎖未簽章或信譽不足的執行檔/DLL，尤其是自己編譯出來、動態載入的 DLL 特別容易中招。
PowerShell執行：
```text
Get-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Control\CI\Policy" -Name "VerifiedAndReputablePolicyState" -ErrorAction SilentlyContinue
```
解決方式：
若這個值存在且為 1（開啟中）或 2（評估模式），八九不離十就是它在擋。
設定 → 隱私權與安全性 → Windows 安全性 → App 與瀏覽器控制 → Smart App Control(智慧型應用程式控制) → 關閉（如果選項還在，代表可以直接關）
如果選項已經消失（代表已經正式生效、鎖死），唯一解法是重新安裝 Windows——這是微軟刻意設計的單向開關，是這個功能最常被開發者詬病的地方

## WDAC政策

**確認是否有 WDAC Policy**
系統管理員 PowerShell執行： dir C:\Windows\System32\CodeIntegrity
確認Length Name是否有.p7b
Get-ItemProperty -Path "HKLM:\SYSTEM\CurrentControlSet\Control\CI\Policy" -Name "VerifiedAndReputablePolicyState" -ErrorAction SilentlyContinue

## 個人憑證(針對某個.dll作為範例)
以 E:\Project\DayFlow\DayFlow.Web\bin\Debug\net10.0\DayFlow.Modules.Notes.dll 為例

**查詢個人憑證用途**
PowerShell執行：
```text
Get-ChildItem Cert:\CurrentUser\My |
Format-List Subject, Thumbprint, EnhancedKeyUsageList, NotBefore, NotAfter, HasPrivateKey
```

```text
Get-ChildItem Cert:\LocalMachine\My |
Format-List Subject, Thumbprint, EnhancedKeyUsageList, NotBefore, NotAfter, HasPrivateKey
```

是否類似 
EnhancedKeyUsageList : Code Signing
HasPrivateKey : True

**個人開發電腦建立一張 Dev Code Signing 憑證**
PowerShell執行：
```text
New-SelfSignedCertificate `
-Type CodeSigningCert `
-Subject "CN=DayFlow Development Code Signing" `
-CertStoreLocation Cert:\CurrentUser\My
```

**確認**
PowerShell執行：
```text
Get-ChildItem Cert:\CurrentUser\My |
Where-Object {
    $_.Subject -like "*DayFlow*"
} |
Format-List Subject, EnhancedKeyUsageList, HasPrivateKey
```

**確認是否安裝signtool**
PowerShell執行：
```text
Get-ChildItem "C:\Program Files (x86)\Windows Kits\10\bin\" -Recurse -Filter "signtool.exe" -ErrorAction SilentlyContinue
Get-ChildItem "C:\Program Files\Microsoft Visual Studio\" -Recurse -Filter "signtool.exe" -ErrorAction SilentlyContinue
```

**若是安裝不在C:**
PowerShell執行：
```text
reg query "HKLM\SOFTWARE\Microsoft\Windows Kits\Installed Roots"
```
查看有沒有類似 KitsRoot10    REG_SZ    C:\Program Files (x86)\Windows Kits\10\

**安裝signtool**
PowerShell執行：
1.找尋最新版本:
```text
winget search "Windows SDK"
```
2. 安裝:
```text
winget install --id Microsoft.WindowsSDK.10.0.18362
```

**簽 DLL**
尋找完整路徑，PowerShell執行：
```text
Get-ChildItem "E:\Windows Kits\10" -Recurse -Filter signtool.exe
```
例: E:\Windows Kits\10\bin\10.0.28000.0\x64\signtool.exe
PowerShell執行：
```text
& "E:\Windows Kits\10\bin\10.0.28000.0\x64\signtool.exe" sign `
 /fd SHA256 `
 /n "DayFlow Development Code Signing" `
 "E:\Project\DayFlow\DayFlow.Web\bin\Debug\net10.0\DayFlow.Modules.Notes.dll"
 ```
 
**確認簽章成功**
PowerShell執行：
```text
Get-AuthenticodeSignature `
"E:\Project\DayFlow\DayFlow.Web\bin\Debug\net10.0\DayFlow.Modules.Notes.dll"
```

**最後確認**
1. DLL 是否有 Authenticode 簽章
2. Code Integrity 是否接受這個簽章
PowerShell執行：
```text
[System.Reflection.Assembly]::LoadFrom(
"E:\Project\DayFlow\DayFlow.Web\bin\Debug\net10.0\DayFlow.Modules.Notes.dll"
)
 ```

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