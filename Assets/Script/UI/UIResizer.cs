using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIResizer : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform target;

    public void OnDrag(PointerEventData eventData)
    {
        if (target != null)
        {
            Vector2 sizeDelta = target.sizeDelta;
            sizeDelta += new Vector2(eventData.delta.x, eventData.delta.y);
            sizeDelta.x = Mathf.Max(sizeDelta.x, 100); // 最小寬度
            sizeDelta.y = Mathf.Max(sizeDelta.y, 100); // 最小高度
            target.sizeDelta = sizeDelta;
        }
    }
}
