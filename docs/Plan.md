# 設計規劃 (Plan.md)
* **介面架構**：設計 `IMenuController` 介面，並由 `MainMenuController` 實作。
* **策略模式**：針對不同的功能頁面 (YouTube, Settings)，採用「分頁列表管理」策略，透過 `SwitchPage` 統一管理。
* **介面設計**：採用 `Floating_Canvas` 作為所有小工具的父物件，便於整體隱藏或顯示。
* **導航路由**：在 Dashboard 設置四個按鈕，分別對應 `Toggle_Pomodoro()`、`Toggle_TodoList()` 等事件。
* **層級互斥**：透過 `SwitchPage` 邏輯，確保使用者在設定詳細參數時，不會被浮動小工具遮擋視線。
