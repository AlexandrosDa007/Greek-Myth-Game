﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Scripts.GameModels;

public class EventManger : MonoBehaviour
{

    public GameEvent gameEvent;
    public GameObject eventTitle;
    public GameObject eventDescription;
    public GameObject okButton;
    public Sprite badSprite;
    public Sprite goodSprite;


    public Player player;

    //Online specific
    public PlayerOnline playerOnline;

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
        okButton.GetComponent<Button>().enabled = true;
        gameEvent = e;
        eventTitle.GetComponent<TextMeshProUGUI>().text = this.gameEvent.title;
        eventDescription.GetComponent<TextMeshProUGUI>().text = this.gameEvent.description;
        okButton.GetComponentInChildren<TextMeshProUGUI>().text = "ok";

        gameObject.GetComponent<Image>().sprite = this.gameEvent.isGood ? goodSprite : badSprite;
        
        // TODO: Also play sound effects accordingly
     
        if (!Board.isMultiplayer && Dice.turn == "enemy")
        {
            okButton.GetComponent<Button>().enabled = false;
            StartCoroutine(CloseWindow());
        }
        
    }

    public void OnOkClick()
    {
        gameObject.SetActive(false);

        if (Board.isMultiplayer)
        {
            playerOnline.Move(gameEvent.steps);
            return;
        }
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
