using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    public GameObject definedButton;
    public UnityEvent OnClick = new UnityEvent();
    public GameObject realDice;

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
        isRolling = true;

        realDice.SetActive(true);
        RealDice rd = realDice.GetComponent<RealDice>();
        rd.roll = true;
        
    }
}
