using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayButton : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public Image background;
    private int day;
    private string Today;
    private CalendarManager calendarManager;

    public void Setup(int day, CalendarManager manager, bool isToday)
    {
        this.day = day;
        this.calendarManager = manager;
        dayText.text = day.ToString();

        // 檢查是不是為今天
        if (isToday){
            background.color = Color.cyan;
            Today =  day.ToString();
        }
    }
    public void OnClick()
    {
        calendarManager.OnDaySelected(day);
    }

    public void SetupEmpty()
    {
        dayText.text = "";
        Button btn = GetComponent<Button>();
        Destroy(btn);
    }
    public void SetText(string text){
        dayText.text = text;
    }
    public void MarkHasNote()
    {
        background.color = Color.yellow; // ���ƶ� �� ����
    }
    public void MarkHasNote(bool Mark)
    {
        
        if (dayText.text == Today && Mark)
            background.color = Color.red;
        else if(dayText.text == Today && !Mark)
            background.color = Color.cyan;
        else
            background.color = Mark ? Color.yellow : Color.white;
    }
}