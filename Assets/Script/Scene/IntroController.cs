using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // 引入 Video 命名空間
using System.Collections;

public class VideoIntroController : MonoBehaviour
{
    // 在 Inspector 中將 Video Player 組件拖曳到這個欄位
    public VideoPlayer videoPlayer; 
    
    // 主選單或遊戲場景的名稱，請替換成您實際的場景名稱
    public string nextSceneName = "MainScene"; 

    void Start()
    {
        // 確保 VideoPlayer 存在
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer 引用缺失，無法播放開頭影片！");
            // 嘗試手動載入場景作為備份
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        // 1. 綁定影片播放結束的事件
        // 當影片播放到結尾時，會觸發這個事件
        videoPlayer.loopPointReached += OnVideoFinished;

        // 2. 播放影片
        videoPlayer.Play();
        
        // 3. (可選) 增加一個跳過邏輯，例如按任意鍵跳過
        StartCoroutine(SkipIntro());
    }

    /// <summary>
    /// 當影片播放結束時，由 VideoPlayer 事件自動呼叫
    /// </summary>
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("開頭影片播放完畢，載入主場景...");
        // 載入下一個場景
        SceneManager.LoadScene(nextSceneName);
    }
    
    // 可選的跳過邏輯
    IEnumerator SkipIntro()
    {
        // 這裡可以設置一個簡單的無限循環來檢查輸入
        while (!videoPlayer.isPaused && videoPlayer.isPlaying)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("玩家跳過開頭影片。");
                videoPlayer.Stop(); // 停止播放
                SceneManager.LoadScene(nextSceneName);
                yield break; // 退出 Coroutine
            }
            yield return null;
        }
    }
    
    
    // 移除事件監聽器是一個好習慣，儘管在這裡不是絕對必須 (因為場景會被銷毀)
    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}