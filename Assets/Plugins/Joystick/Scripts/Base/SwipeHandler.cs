using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public Vector2 swipeDelta { get; private set; }
    private Vector2 lastFrameSwipeDelta;

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        swipeDelta = Vector2.zero;
        lastFrameSwipeDelta = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentTouchPosition = eventData.position;
        swipeDelta = currentTouchPosition - lastFrameSwipeDelta;
        lastFrameSwipeDelta = currentTouchPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        swipeDelta = Vector2.zero;
    }
}