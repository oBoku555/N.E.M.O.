using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class CalendarManager : MonoBehaviour
{
    public GameObject dayButtonPrefab;
    public Transform gridParent;
    public TextMeshProUGUI selectedDateLabel;
    public TMP_InputField noteInput;
    public Button saveButton;
    private DateTime currentDate;
    private Dictionary<string, string> notes = new Dictionary<string, string>();
    private int selectedDay;
    private List<DayButton> dayButtons = new List<DayButton>();
    private string[] WeekdayLabels =    { "日", "一", "二", "三", "四", "五", "六" };
    void Start()
    {
        currentDate = DateTime.Now;
        GenerateCalendar(currentDate);
        saveButton.onClick.AddListener(SaveNote);
    }

    void GenerateCalendar(DateTime date)
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        dayButtons.Clear();
        selectedDateLabel.text = date.ToString("yyyy MMMM");

        DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
        int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

        int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;
        int gridcnt = 0;
        //設定前面的日~一
        for (int i = 0; i < 7; i++)
        {
            GameObject emptyObj = Instantiate(dayButtonPrefab, gridParent);
            DayButton emptyBtn = emptyObj.GetComponent<DayButton>();
            emptyBtn.SetupEmpty();
            emptyBtn.SetText(WeekdayLabels[i]);
            gridcnt++;
        }
        // 設定前面的空日期
        for (int i = 0; i < startDayOfWeek; i++)
        {
            GameObject emptyObj = Instantiate(dayButtonPrefab, gridParent);
            DayButton emptyBtn = emptyObj.GetComponent<DayButton>();
            emptyBtn.SetupEmpty(); // �A�i�H�b DayButton �g�Ӥ�k�ӳ]�m���u�ťաv
            gridcnt++;
        }

        // �A�ͦ������
        for (int i = 0; i < daysInMonth; i++)
        {
            DateTime currentDate = firstDayOfMonth.AddDays(i);
            string key = currentDate.ToString("yyyyMMdd");

            GameObject obj = Instantiate(dayButtonPrefab, gridParent);
            DayButton btn = obj.GetComponent<DayButton>();

            bool isToday = (currentDate.Date == DateTime.Now.Date);
            btn.Setup(currentDate.Day, this, isToday);

            if (notes.ContainsKey(key))
            {
                btn.MarkHasNote(true);
            }
            dayButtons.Add(btn);
            gridcnt++;
        }

        for (int i = gridcnt; i < 7*6; i++)
        {
            GameObject emptyObj = Instantiate(dayButtonPrefab, gridParent);
            DayButton emptyBtn = emptyObj.GetComponent<DayButton>();
            emptyBtn.SetupEmpty(); // �A�i�H�b DayButton �g�Ӥ�k�ӳ]�m���u�ťաv
        }
    }

    public void OnDaySelected(int day)
    {
        selectedDay = day;
        selectedDateLabel.text = $"{currentDate.Year} {currentDate.Month}月{day}日";
        DateTime date = new DateTime(currentDate.Year, currentDate.Month, selectedDay);
        string key = date.ToString("yyyyMMdd");
        // �p�G���e���g�ƶ� �� ���
        if (notes.ContainsKey(key))
            noteInput.text = notes[key];
        else
            noteInput.text = "";
    }

    void SaveNote()
    {
        if (selectedDay == 0) return;

        string text = noteInput.text;
        DateTime date = new DateTime(currentDate.Year, currentDate.Month, selectedDay);
        string key = date.ToString("yyyyMMdd");

        if (!string.IsNullOrEmpty(text))
        {
            notes[key] = text;
            dayButtons[selectedDay - 1].MarkHasNote(true);
        }
        else
        {
            if (notes.ContainsKey(key))
                notes.Remove(key);

            dayButtons[selectedDay - 1].MarkHasNote(false);
        }
    }
    public void NextMonth()
    {
        currentDate = currentDate.AddMonths(1);
        GenerateCalendar(currentDate);
    }

    // ���s�ƥ�G�W�@�Ӥ�
    public void PrevMonth()
    {
        currentDate = currentDate.AddMonths(-1);
        GenerateCalendar(currentDate);
    }
}
