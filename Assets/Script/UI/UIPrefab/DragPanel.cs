using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private float _initialX;
    private float _initialY;
    private RectTransform _targetRectTransform;
    private Vector2 _pointerOffset;

    private void Awake()
    {
        _targetRectTransform = transform.parent.GetComponent<RectTransform>();
        _initialX = _targetRectTransform.anchoredPosition.x; 
        _initialY = _targetRectTransform.anchoredPosition.y;
    }

   public void OnPointerDown(PointerEventData eventData)
    {
        // 計算滑鼠點擊點與 Panel 錨點之間的偏移量
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _targetRectTransform, eventData.position, eventData.pressEventCamera, out _pointerOffset);
    }
     public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition;
        
        // 每次拖曳時，將新的滑鼠螢幕座標轉換為 Panel 所在的局部座標
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _targetRectTransform.parent as RectTransform, // 這裡要用 Panel 的父物件 (Canvas) 進行座標轉換
            eventData.position, 
            eventData.pressEventCamera, 
            out localPointerPosition))
        {
            // 設定 Panel 的新錨點位置 = 新的局部座標 - 初始偏移量
            _targetRectTransform.anchoredPosition = localPointerPosition - _pointerOffset;
        }
    }
    
    // 還原功能
    public void RestorePosition()
    {
        _targetRectTransform.anchoredPosition = new Vector2(_initialX, _initialY);
    }
}
