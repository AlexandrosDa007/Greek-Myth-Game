using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EventManger : MonoBehaviour
{

    public GameEvent gameEvent;
    public GameObject eventTitle;
    public GameObject eventDescription;
    public Sprite badSprite;
    public Sprite goodSprite;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameEvent(GameEvent e)
    {
        this.gameEvent = e;
        this.eventTitle.GetComponent<TextMeshProUGUI>().text = this.gameEvent.title;
        this.eventDescription.GetComponent<TextMeshProUGUI>().text = this.gameEvent.description;


        if (!this.gameEvent.isGood) {
            // Set color
            gameObject.GetComponent<Image>().sprite = badSprite;
        }else {
            gameObject.GetComponent<Image>().sprite = goodSprite;
        }
        // TODO: if event is bad then maybe display a red banner around the object
        // Also play sound effects accordingly


    }

    public void OnOkClick()
    {
        gameObject.SetActive(false);
        
        player.MovePlayer(gameEvent.steps);
    }
}
