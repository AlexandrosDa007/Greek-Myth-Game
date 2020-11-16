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
    public GameObject okButton;
    public Sprite badSprite;
    public Sprite goodSprite;


    public Player player;
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {

    }
    /// <summary>
    /// Sets the game event for this window.
    ///</summary>
    public void SetGameEvent(GameEvent e)
    {
        okButton.SetActive(true);
        this.gameEvent = e;
        this.eventTitle.GetComponent<TextMeshProUGUI>().text = this.gameEvent.title;
        this.eventDescription.GetComponent<TextMeshProUGUI>().text = this.gameEvent.description;


        gameObject.GetComponent<Image>().sprite = this.gameEvent.isGood ? goodSprite : badSprite;
        
        // TODO: Also play sound effects accordingly

        if (Dice.turn == "player")
        {
            okButton.GetComponent<Button>().enabled = false;
            StartCoroutine(CloseWindow());
        }
        
    }

    public void OnOkClick()
    {
        gameObject.SetActive(false);
        player.MovePlayer(gameEvent.steps);
    }

    private IEnumerator CloseWindow()
    {
        for(int i =0;i<5;i++)
        {

            okButton.GetComponentInChildren<TextMeshProUGUI>().text = "ok ("+(5-i)+")";
            yield return new WaitForSeconds(1f);
        }

        gameObject.SetActive(false);
        enemy.MoveEnemy(gameEvent.steps);
    }
}
