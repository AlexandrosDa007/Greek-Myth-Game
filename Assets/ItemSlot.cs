using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject playerSet;

    public void OnDrop(PointerEventData eventData)
    {
        // Login here
        if (eventData.pointerDrag != null)
        {
            GameObject draggedItem = eventData.pointerDrag;
            draggedItem.GetComponent<DragDrop>().inSpot = true;
            draggedItem.transform.SetParent(this.gameObject.transform.parent);
            draggedItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            playerSet.GetComponent<PlayerSet>().playerHero = draggedItem.GetComponent<DragDrop>().hero;
            playerSet.GetComponent<PlayerSet>().draggedItem = draggedItem;
        }
    }

}
