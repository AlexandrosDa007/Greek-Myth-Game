using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceOnline : MonoBehaviour
{

    public static bool isThisTheFirstTurn = true;

    public static int diceResult;
    public static bool isRolling = false;
    public static string turnId;
    
    // Maybe player online?
    //public static Player[] players;

    public PlayerOnline appPlayer;

    public GameObject diceSpawner;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public Button button;

    void Start()
    {
        // Find which player whould roll dice first? maybe random?
        isRolling = false;
        button = gameObject.GetComponent<Button>();
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
            
        if (DiceOnline.turnId == appPlayer.User.uid  && !button.interactable && !isRolling)
        {
            button.interactable = true;
            gameObject.GetComponent<Image>().sprite = enabledSprite;
        }

    }

    public void RollDice()
    {
        // This should be run once?
        if (DiceOnline.isThisTheFirstTurn)
        {
            Board.SetUpBoard();
            Debug.Log("Setting up board....");
        }

        // Theese 2 should change when player finished turn
        button.interactable = false;
        isRolling = true;

        gameObject.GetComponent<Image>().sprite = disabledSprite;
        diceSpawner.GetComponent<DiceSpawner>().playerOnline = appPlayer;
        diceSpawner.GetComponent<DiceSpawner>().SpawnDice();
    }

}
