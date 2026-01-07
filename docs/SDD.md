# 軟體設計規格書 (Software Design Description, SDD)
## 專案名稱：N.E.M.O. - 工作陪伴軟體

## 1. 實踐路徑：從 Constitution 到 Audit (SDLC)
本專案嚴格遵循「規格先行」的循環開發路徑，確保每一行代碼皆有據可依。



### 1.1 澄清與規劃 (Clarify & Plan)
* **狀態定義**：透過與 AI 的 Clarify 階段，確立選單開啟時應「凍結物理時間（Time.timeScale = 0）」的規格，以避免背景邏輯在選單操作時產生偏移。
* **系統建模**：設計 `MainMenuController` 作為中樞，管理所有分頁（ContentPages）的顯示狀態。

### 1.2 實作階段 (Implement)
* **規格執行**：AI 依照 `Tasks.md` 拆解的任務，逐一實作番茄鐘、TodoList、時鐘與日曆的 C# 邏輯。
* **程式碼品質**：強制執行 `Constitution.md` 規範，禁止 Code-behind 雜亂邏輯，確保代碼具備高度可讀性與模組化。

### 1.3 審計與驗證 (Audit)
* **功能驗證**：最終 Audit 階段確認番茄鐘在時間凍結下能正確暫停，且 TodoList 數據符合預期規格。
* **規格一致性**：驗證所有功能（包含 YouTube 瀏覽器的 JS 暫停邏輯）皆完全符合 `Spec.md` 定義的防呆機制。

---

## 2. 核心技術規格彙整 (Technical Summary)

| 功能模組 | 關鍵規格 (Spec) | 實作邏輯 (Logic) |
| :--- | :--- | :--- |
| **系統控制器** | 開啟 UI 時暫停背景物理 | `Time.timeScale = 0f` |
| **番茄鐘** | 支援專注/休息雙模式 | 倒數計時器邏輯串接 UI 文字 |
| **TodoList** | 動態生成與數據存儲 | 使用 Instantiate 產生成員物件 |
| **YouTube 控制** | 關閉視窗同步停止音訊 | 注入 `javascript:pause()` 指令 |
