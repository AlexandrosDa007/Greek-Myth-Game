using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    public Player[] players;
    public Player player;
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();

    public static int diceResult;
    public static bool isRolling = false;
    void Start()
    {
        definedButton = this.gameObject;
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
        Board.SetUpBoard();
        diceResult = Mathf.FloorToInt(Random.Range(1,7));
        // Disallow player to roll dice
        isRolling = true;
        // Find which player should move this is SERVER SIDE 

        player.MovePlayer(0);
        
    }
}
