using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToDoListManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject toDoItemPrefab; // 一個有 Text 和 Delete 按鈕的 UI prefab
    public Transform toDoListContainer;

    public void AddItem()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) return;

        GameObject item = Instantiate(toDoItemPrefab, toDoListContainer);
        ToDoListButton toDoItem = item.GetComponent<ToDoListButton>();
        toDoItem.textField.text = inputField.text;

        inputField.text = "";
    }
}
