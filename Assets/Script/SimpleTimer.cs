using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SimpleTimer : MonoBehaviour
{
    public TMP_Text workTimerText;
    public TMP_Text breakTimerText;
    public TMP_Text cycleTimerText;
    public float countdownWorkTime = 1500f; // 25分鐘
    public float countdownBreakTime = 0f;
    public int cycleTime = 0;
    public Image StartButtonImage;
    public Sprite Start_icon;
    private float currentWorkTime;
    private float currentBreakTime;
    private bool isRunning = false;

    void Start()
    {
        currentWorkTime = countdownWorkTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (isRunning && cycleTime >= 0)
        {
            if (currentWorkTime >= 0f && currentBreakTime >= 0f) {
                currentWorkTime -= Time.deltaTime;
            }
            if (currentWorkTime <= 0f && currentBreakTime >=0f)
            {
                currentBreakTime -= Time.deltaTime;
                currentWorkTime = 0f;
                // 這裡你可以加入提醒音效或動畫
            }
            if (currentWorkTime <= 0f && currentBreakTime <= 0f) {
                cycleTime--;
                cycleTimerText.text = cycleTime.ToString();
                currentWorkTime = countdownWorkTime;
                currentBreakTime = countdownBreakTime;
            }
                UpdateTimerText();
        }
    }

    public bool PauseTimer()
    {
        isRunning = !isRunning;
        return isRunning;
    }
    public void ResetTimer()
    {
        currentWorkTime = countdownWorkTime;
        currentBreakTime = countdownBreakTime;
        isRunning = false;
        StartButtonImage.sprite = Start_icon;
        cycleTimerText.text = cycleTime.ToString();
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int workminutes = Mathf.FloorToInt(currentWorkTime / 60f);
        int workseconds = Mathf.FloorToInt(currentWorkTime % 60f);
        int breakminutes = Mathf.FloorToInt(currentBreakTime / 60f);
        int breakseconds = Mathf.FloorToInt(currentBreakTime % 60f);
        workTimerText.text = string.Format("{0:00}:{1:00}", workminutes, workseconds);
        breakTimerText.text = string.Format("{0:00}:{1:00}", breakminutes, breakseconds);
        
    }
}