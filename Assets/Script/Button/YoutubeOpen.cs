using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using ZenFulcrum.EmbeddedBrowser;

public class YoutubeOpen : MonoBehaviour
{
    [Header("引用 YoutubeBrowser (包含控制 Panel 的父物件)")]
    public GameObject youtubeBrowser;

    public void ToggleYoutube()
    {
        // 1. 取得目前顯示狀態
        bool isOpening = !youtubeBrowser.activeSelf;

        if (!isOpening)
        {
            // 修正：因為 Browser 組件在子物件 Browser (GUI) 身上，所以改用 InChildren
            var browserEngine = youtubeBrowser.GetComponentInChildren<Browser>();

            if (browserEngine != null)
            {
                // 使用萬用 JavaScript 注入方式暫停影片
                browserEngine.LoadURL("javascript:document.querySelectorAll('video').forEach(v => v.pause());", true);
            }
            else
            {
                // 明確指定 UnityEngine 解決歧義
                UnityEngine.Debug.LogWarning("在子物件中仍找不到 Browser 組件！請確認 Browser (GUI) 是否掛有 Browser 腳本。");
            }
        }

        // 2. 切換父物件(包含 Header 和 Panel)的顯示狀態
        youtubeBrowser.SetActive(isOpening);
    }
}
