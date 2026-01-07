using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public RectTransform panelTransform;   // 父物件 (面板)
    public CanvasGroup canvasGroup;        // 控制透明度

    public float scaleMin = 0.5f, scaleMax = 2f,opacityMin = 0.2f,opacityMax = 1f;

    public Slider scaleSlider;
    public Slider opacitySlider;
   
    private void Start()
    {
        // 設定初始值
        scaleSlider.onValueChanged.AddListener(SetScale);
        opacitySlider.onValueChanged.AddListener(SetOpacity);
    }

    void SetScale(float value)
    {
        float scale;

        if (value >= 0.5f)
        {
            // 左半段：1 → 2
            scale = Mathf.Lerp(1f, 2f,(value - 0.5f) / 0.5f);
        }
        else
        {
            // 右半段：0.5 → 1
            scale = Mathf.Lerp(0.5f, 1f, value / 0.5f);
        }

        panelTransform.localScale = Vector3.one * scale;
    }

    void SetOpacity(float value)
    {
        float alpha = Mathf.Lerp(opacityMin, opacityMax, value);
        canvasGroup.alpha = 1.2f - alpha;
    }
}
