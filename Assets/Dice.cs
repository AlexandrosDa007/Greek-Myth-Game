using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    public static int diceResult;
    public static bool isRolling = false;
    public static string turn;

    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public GameObject diceSpawner;

    


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
    }

    public void RollDice()
    {
        Debug.Log("Ri3e zaria");

        Board.SetUpBoard();
        isRolling = true;

        diceSpawner.GetComponent<DiceSpawner>().SpawnDice();
    }
}
