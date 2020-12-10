using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSet : MonoBehaviour
{

    public TextMeshProUGUI playerNameField;

    private string playerName;
    public string playerHero;

    public GameObject draggedItem;

    // Start is called before the first frame update
    void Start()
    {
        playerName = playerNameField.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (draggedItem != null && !draggedItem.GetComponent<DragDrop>().inSpot) {
            playerHero = null;
            draggedItem = null;
        }
    }
}
