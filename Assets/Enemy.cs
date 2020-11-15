using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject eventWindow;
    public GameObject diceSpawner;

    public int startingStep;

    // Start is called before the first frame update
    void Start()
    {
        startingStep = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RollDiceForEnemy()
    {
        diceSpawner.GetComponent<DiceSpawner>().SpawnDice();
    }

    public void MoveEnemy(int number)
    {

        int newStep = -999;
        switch (number)
        {
            case 0:
                {
                    newStep = startingStep + Dice.diceResult;
                    break;
                }
            case 1:
                {
                    newStep = startingStep + 1;
                    break;
                }
            case -1:
                {
                    newStep = startingStep - 1;
                    break;
                }
            default:
                {
                    newStep = startingStep + number;
                    break;
                }
        }

        StartCoroutine(Move(startingStep, newStep));

    }

    private IEnumerator Move(int step, int newStep)
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        // When the enemy goes back
        if (step > newStep)
        {
            for (int i = step; i > newStep - 1; i--)
            {
                MyStep st = Board.GetStepFromIndex(i);
                newPosition = st.Position;
                newPosition.x += 6f;
                transform.position = newPosition;
                yield return new WaitForSeconds(1f);
            }
        }
        else
        {

            for (int i = step; i < newStep + 1; i++)
            {

                MyStep st = Board.GetStepFromIndex(i);
                newPosition = st.Position;
                newPosition.x += 6f;
                transform.position = newPosition;

                yield return new WaitForSeconds(1f);
            }
        }
        startingStep = newStep;
        Dice.isRolling = CheckIfEvent();

    }

    public bool CheckIfEvent()
    {
        GameEvent e = Board.GetStepFromIndex(startingStep).GameEvent;
        if (e != null)
        {
            EventManger em = eventWindow.GetComponent<EventManger>();
            eventWindow.SetActive(true);
            em.SetGameEvent(e);
            return true;
        }
        return false;

    }
}
