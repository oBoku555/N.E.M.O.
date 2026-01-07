# 軟體設計規格書 (Software Design Description, SDD)

## 1. 專案概述
本專案為一款基於 Unity 引擎開發的二次元風格工作陪伴工具。核心設計理念結合了《鳴潮》的非對稱美學 UI 與實用的生產力工具，旨在提升使用者在長時辦公下的沉浸感與效率。

## 2. 系統架構設計

### 2.1 UI 分層邏輯
系統採用兩層式導航架構，確保操作路徑清晰：
* **第一層 (Dashboard)**：功能入口層。透過 ESC 鍵觸發開啟，提供 YouTube、番茄鐘等常用工具的快速開關（Toggle）。
* **第二層 (MenuContainer)**：詳細設定層。透過 Dashboard 的齒輪圖示進入，管理音量、圖形及系統偏好設定。

### 2.2 控制器實作 (MainMenuController)
核心控制器負責管理 UI 狀態與物理時間縮放：
* **狀態切換**：透過 `ToggleMenu()` 同步管理選單根物件的 Active 狀態。
* **時間縮放**：當選單開啟時，將 `Time.timeScale` 設為 0 以暫停背景運算；關閉後恢復為 1。

## 3. 模組技術細節

### 3.1 媒體模組 (Media Module)
整合第三方插件 ZFBrowser 實現網頁影音播放。
* **播放控制難題**：傳統 `SetActive(false)` 無法停止 Chromium 內核的音訊播放。
* **技術突破**：採用 JavaScript 注入方案。在隱藏物件前，透過 `browserEngine.LoadURL("javascript:...")` 指令強制暫停所有 `<video>` 標籤，確保影音即時同步停止。

### 3.2 佈局規範 (Layout Specification)
* **自適應網格**：使用 `Grid Layout Group` 實現自動化排列。
* **視覺間距**：設定 Padding 為 100 像素，維持介面呼吸感與內容集中度。

## 4. AI 協作開發流程
本專案開發期間廣泛應用 AI (Gemini) 輔助技術決策：
* **API 歧義修正**：針對不同版本插件之 API (如 `Eval` 與 `ExecuteFunction`) 之命名衝突，AI 提供了具備高度相容性的 URL 協議注入建議。
* **架構重構**：AI 協助優化了 `MainMenuController` 的列表管理邏輯，從單頁顯示提升為多頁面動態切換架構。

## 5. 未來擴展計劃
* **自定義 CSS 注入**：預計透過 CSS 控制隱藏網頁原生標頭，提升視覺整合度。
* **數據持久化**：導入 JSON 格式儲存使用者自定義的待辦清單與設定參數。
