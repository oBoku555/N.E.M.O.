# N.E.M.O

這是一款以 Unity 工作陪伴工具。旨在提供使用者一個沉浸式的二次元辦公與學習環境，整合了媒體播放、時間管理與多功能小工具。

## 核心功能 (Key Features)

* **雙層選單 UI 架構**：
    * **Dashboard (功能表)**：快速啟動各類工作小工具，如 YouTube、時鐘、番茄鐘、日曆。
    * **Settings (詳細設定)**：底層系統參數調整，具備數據持久化功能。
* **多媒體整合**：內建 YouTube 瀏覽器，支援背景音樂管理，並具備視窗關閉自動暫停影片功能。
* **生產力工具**：包含實時系統時鐘同步、番茄鐘計時器及待辦事項清單。
* **狀態管理**：開啟選單時自動暫停背景時間流逝，關閉後恢復同步。

## AI 工具協作揭露 (AI Collaboration)

本專案在開發過程中與 AI (Gemini) 進行技術協作，解決了以下關鍵技術難點：

1. **多媒體 API 衝突解決**：
    * 難題：第三方插件 ZFBrowser 在 UI 物件關閉後，網頁影片聲音仍持續播放。
    * 解決方案：AI 協助定位 Browser 組件層級，並建議改採 LoadURL("javascript:...") 方式強制暫停網頁影片標籤，成功達成影音同步效果。
2. **系統架構優化**：
    * 透過 AI 協助重構 MainMenuController.cs，將選單擴展為支援「功能入口」與「詳細設定」的兩層式導航架構。
3. **環境調試**：
    * 解決了 Visual Studio 的 Unicode 編碼衝突問題，確保跨平台開發時腳本文字的一致性。
4. **README 撰寫與文件工程**：
   * 協助內容：AI 協助規劃了專業的 README.md 結構，包含安裝指南、功能簡介與法律免責聲明。
   * 成效：確保了專案交付的完整度，並清晰地向其他開發者解釋了因插件限制而產生的檔案缺失問題。
## 安裝與編譯指南 (Installation & Build Guide)

* 本專案使用付費插件 ZFBrowser，因版權限制未包含在 Repo 中。
* 
1. **取得原始碼**：
   使用 git clone [https://github.com/oBoku555/N.E.M.O./tree/main] 下載專案。
2. **Unity 版本需求**：
   建議使用 Unity 2022.3 LTS 或更高版本開啟。
3. **依賴插件**：
   * ZenFulcrum Embedded Browser (ZFBrowser)
   * TextMeshPro
4. **初次開啟**：
   使用 Unity Hub 開啟資料夾後，系統會自動重建 Library 資料夾（需耗時數分鐘）。請開啟 Assets/Scenes/MainScene.unity 場景。
   然後會進入到unity預設的MainScene **請不要存檔**
　
   點開Assets/Scenes找到MainScene並打開。
6. **編譯發布**：
   透過 File > Build Settings 選擇 PC 平台進行導出。

## 專案架構說明 (Project Architecture)

* Assets/Scripts/Button/：收納 UI 交互邏輯（如 YouTube 開關控制）。
* Assets/Scripts/Controller/：核心控制器，負責頁面切換與時間縮放。
* Floating_Canvas/：所有浮動工具物件（時鐘、播放器等）。
* MainMenu_Canvas/：背景遮罩與設定選單層級。

## 授權與發布

* 發布平台：itch.io
* 版本控制：GitHub
