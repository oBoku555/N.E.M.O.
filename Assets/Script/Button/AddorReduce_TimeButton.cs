using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddTimeButton : MonoBehaviour
{
    public TMP_InputField  TimeInput;
    public void OnAddTimeClick()
    {
        int.TryParse(TimeInput.text, out int timeValue);
        timeValue++;
        TimeInput.text = timeValue.ToString();
    }
    public void OnReduceTimeClick()
    {
        int.TryParse(TimeInput.text, out int timeValue);
        timeValue--;
        if(timeValue < 0)
            timeValue = 0;
        TimeInput.text = timeValue.ToString();
    }
}
