// 將這段掛在 UI 按鈕上
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToDoListButton : MonoBehaviour
{   //暫停開始 開關
    public TMP_Text buttonText;
    public TMP_Text textField;
    void Start()
    {

    }
    public void OnDeleteButtonClick(){
        Destroy(this.gameObject);
    }
    public void OnCheckButtonClick()
    {
        bool fontBool = buttonText.fontStyle == FontStyles.Strikethrough;
        buttonText.fontStyle = fontBool ? FontStyles.Normal : FontStyles.Strikethrough;
        
        
    }
}
