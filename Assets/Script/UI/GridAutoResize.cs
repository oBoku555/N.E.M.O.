using UnityEngine;
using UnityEngine.UI;

public class GridAutoResize : MonoBehaviour
{
    public RectTransform CalendarSize;
    public GridLayoutGroup grid;
    public int rows = 7;
    public int columns = 7; // 如果要一週七天

    void Start()
    {
        Resize();
    }

    void Resize()
    {
        RectTransform rt = CalendarSize;

        float width = rt.rect.width;
        float height = rt.rect.height;

        float cellWidth = width / columns;
        float cellHeight = height / rows;

        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
