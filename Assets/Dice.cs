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

    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public GameObject diceSpawner;
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    


    void Start()
    {
        definedButton = this.gameObject;
        turn = "player";
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;

        if (Input.GetMouseButtonDown(0) && !isRolling)
        {
            if (Physics.Raycast(ray, out Hit) && Hit.collider.gameObject == gameObject)
            {
                RollDice();
                OnClick.Invoke();
            }
        }

        if(!isRolling && gameObject.GetComponent<SpriteRenderer>().sprite == disabledSprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = enabledSprite;
        }
        
        
    }

    public void RollDice()
    {
        Debug.Log("Ri3e zaria");

        Board.SetUpBoard();
        isRolling = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = disabledSprite;

        diceSpawner.GetComponent<DiceSpawner>().SpawnDice();
    }

}
