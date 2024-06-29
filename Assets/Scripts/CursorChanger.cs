using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D newCursor;
    public Texture2D defaultCursor;
    public Vector2 hotspot = Vector2.zero;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (newCursor != null) Cursor.SetCursor(newCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (defaultCursor != null) Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
    }
}
