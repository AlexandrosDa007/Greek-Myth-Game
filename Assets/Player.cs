using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Player : MonoBehaviour
{

    public GameObject questionWindow;
    public GameObject eventWindow;
    public Enemy enemy;
    private int Startingstep;
    private string playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {


        Startingstep = 1;
        switch (gameObject.name)
        {
            case "herculesHead":
                {
                    playerPosition = "topleft";
                    break;
                }
            case "perseusHead":
                {
                    playerPosition = "botleft";
                    break;
                }
            case "achillesHead":
                {
                    playerPosition = "botright";
                    break;
                }
            case "ippoHead":
                {
                    playerPosition = "topright";
                    break;
                }
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MovePlayer(int number)
    {
        int newStep = -999;
        switch (number)
        {
            case 0:
                {
                    newStep = Startingstep + Dice.diceResult;
                    break;
                }
            case 1:
                {
                    newStep = Startingstep + 1;
                    break;
                }
            case -1:
                {
                    newStep = Startingstep - 1;
                    break;
                }
            default:
                {
                    newStep = Startingstep + number;
                    break;
                }
        }

        StartCoroutine(Move(Startingstep, newStep));

    }


    private IEnumerator Move(int step, int newStep)
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        // When the player goes back
        if (step > newStep)
        {
            for (int i = step; i > newStep-1; i--)
            {
                MyStep st = Board.GetStepFromIndex(i);
                newPosition = st.Position;
                switch (playerPosition)
                {
                    case "topright":
                        {
                            newPosition.x += 6f;
                            break;
                        }
                    case "botleft":
                        {
                            newPosition.y -= 5f;
                            break;
                        }
                    case "botright":
                        {
                            newPosition.x += 6f;
                            newPosition.y -= 5f;
                            break;
                        }

                }
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
                switch (playerPosition)
                {
                    case "topright":
                        {
                            newPosition.x += 6f;
                            break;
                        }
                    case "botleft":
                        {
                            newPosition.y -= 5f;
                            break;
                        }
                    case "botright":
                        {
                            newPosition.x += 6f;
                            newPosition.y -= 5f;
                            break;
                        }

                }
                transform.position = newPosition;

                yield return new WaitForSeconds(1f);
            }
        }
        // Let player roll a dice again
        Startingstep = newStep;
        checkIfQuestion();
        checkIfEvent();
        if (!checkIfQuestion() && !checkIfEvent())
        {
            enemy.RollDiceForEnemy();
        }
    }


    public bool checkIfEvent()
    {
        GameEvent e = Board.GetStepFromIndex(Startingstep).GameEvent;
        if (e != null)
        {
            EventManger em = eventWindow.GetComponent<EventManger>();
            eventWindow.SetActive(true);
            em.SetGameEvent(e);
            Dice.isRolling = true;
            return true;
        }
        return false;

    }

    public bool checkIfQuestion()
    {
        Question q = Board.GetStepFromIndex(Startingstep).Question;
        if (q != null)
        {
            // TODO: get question window ready
            QuestionManager qm = questionWindow.GetComponent<QuestionManager>();
            qm.SetQuestion(q);
            qm.questionText.GetComponent<TextMeshProUGUI>().text = qm.Question.question;
            questionWindow.SetActive(true);
            Dice.isRolling = true;
            return true;
        }

        return false;
    }

}
