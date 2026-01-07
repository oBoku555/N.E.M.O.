# 開發憲法 (Constitution.md)
* **架構規範**：強制採用 UI 控制器 (MainMenuController) 與 功能模組 (Floating Tools) 分離架構。
* **開發紀律**：禁止直接在 UI 物件上撰寫邏輯，必須透過 Controller 統一調度狀態。
* **AI 協作守則**：所有代碼實作前必須先定義 Spec。AI 產出後需經由人工 Audit 驗證邏輯與憲法相符。
