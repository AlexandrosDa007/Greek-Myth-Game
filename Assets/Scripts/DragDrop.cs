using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,
IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    public string hero;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public bool inSpot;
    public Vector3 originalPosition;

    private void Awake() {
        inSpot = true;
        originalPosition = transform.position;
        hero = GetComponent<UnityEngine.UI.Image>().sprite.name;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        inSpot = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!inSpot) {
            transform.position = originalPosition;
            transform.SetParent(GameObject.Find("HostRoom").transform);
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("on drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

}
