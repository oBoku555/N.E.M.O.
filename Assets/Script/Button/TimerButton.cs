using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerButton : MonoBehaviour
{
    public SimpleTimer spTimer;
    public Sprite  Start_icon;
    public Sprite  Pause_icon;
    public GameObject timerInput;
    public TMP_InputField  workTimeInput;
    public TMP_InputField  breakTimeInput;
    public TMP_InputField  cycleTimeInput;
    private TMP_Text buttonText;
    private Image buttonImage;
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }
    public void OnAddCycleClick()
    {
        int.TryParse(cycleTimeInput.text, out int cycletimevalue);
        cycletimevalue++;
        cycleTimeInput.text = cycletimevalue.ToString();
    }
    public void OnReduceCycleTimeClick()
    {
        int.TryParse(cycleTimeInput.text, out int cycletimevalue);
        cycletimevalue--;
        cycleTimeInput.text = cycletimevalue.ToString();
    }
    public void OnTimerClick()
    { 
        timerInput.SetActive(!timerInput.activeSelf);
    }
    public void OnPauseClick(){
        bool isRunning = spTimer.PauseTimer();
        buttonImage.sprite = isRunning ? Pause_icon : Start_icon;
    }
    public void OnCycleTimeSettingClick()
    {
        string cleanWorkText = Regex.Replace(workTimeInput.text, @"[^\d]", "");
        string cleanBreakText = Regex.Replace(breakTimeInput.text, @"[^\d]", "");
        if (int.TryParse(cleanWorkText, out int worktimevalue))
        {
            spTimer.countdownWorkTime = worktimevalue * 60;
            Debug.Log("已設定工作時間: " + worktimevalue);
            spTimer.ResetTimer();
        }
        else
        {
            Debug.LogWarning("發生錯誤: " + workTimeInput.text);
        }

        if (int.TryParse(cleanBreakText, out int breaktimevalue))
        {
            spTimer.countdownBreakTime = breaktimevalue * 60;
            Debug.Log("已設定休息時間: " + breaktimevalue);
            spTimer.ResetTimer();
        }
        else
        {
            Debug.LogWarning("發生錯誤: " +breakTimeInput.text);
        }
        if (int.TryParse(cycleTimeInput.text, out int cycletimevalue))
        {
            spTimer.cycleTime = cycletimevalue;
            Debug.Log("循環時間: " + cycletimevalue);
            spTimer.ResetTimer();
        }
    }
}
