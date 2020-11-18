using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject gameOverWindow;
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
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {

            for (int i = step; i < newStep + 1; i++)
            {
                if (i == 49)
                {
                    Debug.Log("telos");
                    // Finished game Enemy is the winner
                    // Show something
                    EndGame();
                    yield break;
                }

                MyStep st = Board.GetStepFromIndex(i);
                newPosition = st.Position;
                newPosition.x += 6f;
                transform.position = newPosition;

                yield return new WaitForSeconds(0.2f);
            }
        }
        startingStep = newStep;
        Dice.isRolling = CheckIfEvent();
        if (!Dice.isRolling) {
            // Enemy finished moving
            Dice.turn = "player";
        }

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

    public void EndGame()
    {
        StartCoroutine(EndGameTimer());
        
    }

    private IEnumerator EndGameTimer()
    {
        for (int i = 0; i < 5; i++)
        {
            gameOverWindow.SetActive(true);
            gameOverWindow.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy wins!\nQuiting in "+(5-i)+" sec...";
            yield return new WaitForSeconds(1f);

        }
        //UnityEditor.EditorApplication.isPlaying = false;
        // Go to menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
