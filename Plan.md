# 設計規劃 (Plan.md)
* **介面架構**：設計 `IMenuController` 介面，並由 `MainMenuController` 實作。
* **策略模式**：針對不同的功能頁面 (YouTube, Settings)，採用「分頁列表管理」策略，透過 `SwitchPage` 統一管理。
