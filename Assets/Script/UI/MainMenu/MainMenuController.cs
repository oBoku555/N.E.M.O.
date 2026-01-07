using UnityEngine;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    [Header("層級根物件")]
    public GameObject mainMenuRoot;    // 總選單根物件
    public GameObject dashboardPanel;  // 第一層：功能表 (新建立的物件)
    public GameObject menuContainer;   // 第二層：詳細設定頁面

    [Header("設定分頁")]
    public List<GameObject> contentPages; // 儲存設定頁內的分頁
    private GameObject activePage = null;

    void Start()
    {
        // 預設關閉總選單
        mainMenuRoot.SetActive(false);

        // 預設初始化設定頁面的第一個分頁
        if (contentPages.Count > 0) SwitchPage(contentPages[0].name);
    }

    // 1. ESC 鍵控制：切換總選單開關
    public void ToggleMenu()
    {
        bool isActive = !mainMenuRoot.activeSelf;
        mainMenuRoot.SetActive(isActive);

        // 切換時暫停/恢復遊戲時間
        Time.timeScale = isActive ? 0f : 1f;

        if (isActive)
        {
            // 每次按下 ESC 打開時，預設顯示「功能表」，隱藏「詳細設定」
            OpenDashboard();
        }
    }

    // 2. 切換至：功能表 (Dashboard)
    // 綁定給設定頁面中的「返回」按鈕
    public void OpenDashboard()
    {
        dashboardPanel.SetActive(true);
        menuContainer.SetActive(false);
    }

    // 3. 切換至：詳細設定 (Settings)
    // 綁定給功能表中的「齒輪」按鈕
    public void OpenSettings()
    {
        dashboardPanel.SetActive(false);
        menuContainer.SetActive(true);
    }

    // 4. 設定分頁切換邏輯 (保留原功能)
    public void SwitchPage(string pageName)
    {
        if (activePage != null) activePage.SetActive(false);

        foreach (var page in contentPages)
        {
            if (page.name == pageName)
            {
                page.SetActive(true);
                activePage = page;
                break;
            }
        }
    }
}