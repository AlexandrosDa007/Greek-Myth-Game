using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public static int diceResult;
    public static bool isRolling = false;
    public static string turn;

    //public GameObject definedButton;
    //public UnityEvent OnClick = new UnityEvent();
    public GameObject diceSpawner;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public Button button;


    void Start()
    {
        //definedButton = this.gameObject;
        turn = "player";
        isRolling = false;
        button = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit Hit;

        // if (Input.GetMouseButtonDown(0) && !isRolling)
        // {
        //     if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
        //     {
        //         RollDice();
        //         OnClick.Invoke();
        //     }
        // }

        // if(!isRolling && gameObject.GetComponent<SpriteRenderer>().sprite == disabledSprite)
        // {
        //     gameObject.GetComponent<SpriteRenderer>().sprite = enabledSprite;
        // }
        if (Dice.turn == "player" && !button.interactable && !isRolling)
        {
            button.interactable = true;
            gameObject.GetComponent<Image>().sprite = enabledSprite;
        }

    }

    public void RollDice()
    {
        // This is called only when player rolls dice

        Debug.Log("not");
        Dice.turn = "player";
        button.interactable = false;
        Board.SetUpBoard();
        isRolling = true;
        gameObject.GetComponent<Image>().sprite = disabledSprite;

        diceSpawner.GetComponent<DiceSpawner>().SpawnDice();
    }

}
