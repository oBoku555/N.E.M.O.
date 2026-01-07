using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ClockDisplay : MonoBehaviour
{
    public  TMP_Text clockText;

    void Update()
    {
        clockText.text = DateTime.Now.ToString("HH:mm");
    }
}