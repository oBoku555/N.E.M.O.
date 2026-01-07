using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestButton : MonoBehaviour
{
    
    // --- UI 控制項 (用於還原大小和透明度) ---
    public Slider scaleSlider;
    public Slider opacitySlider;

    // --- 外部腳本引用 (用於還原位置) ---
    // 請將您的 DragPanel 腳本所掛載的 Header/Panel 物件拖入這裡
    public DragPanel dragPanelScript; 

    // --- 預設值 ---
    private const float DEFAULT_SCALE_VALUE = 0.5f;
    private const float DEFAULT_OPACITY_VALUE = 0f;
    private const float DEFAULT_COLOR_R = 1f; // 假設預設顏色是白色 (1, 1, 1, 1)

    // 這個方法綁定到您的「自訂還原」按鈕的 OnClick() 事件
    public void RestoreAllCustomSettings()
    {
        // 1. 還原位置 (呼叫 DragPanel 腳本的方法)
        if (dragPanelScript != null)
        {
            dragPanelScript.RestorePosition();
        }
        else
        {
            Debug.LogError("CustomRestorer: 尚未指定 DragPanel 腳本!");
        }

        // 2. 還原大小/比例 (透過 Slider)
        if (scaleSlider != null)
        {
            scaleSlider.value = DEFAULT_SCALE_VALUE;
        }

        // 3. 還原透明度 (透過 Slider)
        if (opacitySlider != null)
        {
            opacitySlider.value = DEFAULT_OPACITY_VALUE;
        }
        
        // 4. 還原顏色 (如果您的 Slider 只是控制透明度，您可能需要單獨處理顏色組件)
        // 假設您有一個 RectTransform 叫 targetPanel 來控制顏色
        /*
        Image panelImage = targetPanel.GetComponent<Image>();
        if (panelImage != null)
        {
            Color defaultColor = new Color(DEFAULT_COLOR_R, DEFAULT_COLOR_R, DEFAULT_COLOR_R, DEFAULT_OPACITY_VALUE);
            panelImage.color = defaultColor;
        }
        */

        // 提示：還原完成後，建議清除 PlayerPrefs 中儲存的客製化值，
        // 確保下次遊戲載入時會回到預設值，而不是讀取被還原前的客製化值。
        // PlayerPrefs.DeleteKey("CustomScale");
        // PlayerPrefs.DeleteKey("CustomOpacity");
        // PlayerPrefs.Save();
        
        Debug.Log("所有自訂設定已還原至預設值。");
    }
}
