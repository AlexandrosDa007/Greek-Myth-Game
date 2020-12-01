using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.GameModels;
using Scripts.Objects;
using TMPro;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class PlayerOnline : MonoBehaviour
{


    public AudioClip moveSound;
    public GameObject gameOverWindow;
    public GameObject questionWindow;
    public GameObject eventWindow;

    private int Startingstep;
    private string playerPosition;

    private FirebaseUser user;

    public FirebaseUser User { get => user; set => user = value; }

    public EnemyOnline[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        // load up user?
        //FirebaseAuth.CurrentUser(gameObject.name, "GetCurrentUser", "ErrorCurrentUser");
        // TODO: find a way to select position for players?
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

    public void GetCurrentUser(string json)
    {
        try
        {
            User = JsonUtility.FromJson<FirebaseUser>(json);
        }
        catch (System.Exception)
        {
            Debug.LogError("Error  when getting user!!");
            throw new System.Exception("Error when getting current user");
        }
    }

    public void Move(int number)
    {
        int newStep = -999;
        switch (number)
        {
            case 0:
                {
                    // Shouldn't be used?
                    Debug.Log("WTF");
                    newStep = Startingstep + DiceOnline.diceResult;
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

        StartCoroutine(MoveSteps(Startingstep, newStep));
    }


    private IEnumerator MoveSteps(int step, int newStep)
    {
        Vector3 newPosition = new Vector3(0, 0, 0);
        // When the player goes back
        if (step > newStep)
        {
            for (int i = step; i > newStep - 1; i--)
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
                            newPosition.z -= 6f;
                            break;
                        }
                    case "botright":
                        {
                            newPosition.x += 6f;
                            newPosition.z -= 6f;
                            break;
                        }

                }
                transform.position = newPosition;
                // Play sound
                GameObject.FindGameObjectWithTag("soundEffects").GetComponent<SoundEffects>().PlaySoundEffect(moveSound);
                FirebaseDatabase.PostJSON("game/positions/"+User.uid, i+"", gameObject.name, "OnPostSuccess", "OnPostFail");
                yield return new WaitForSeconds(0.2f);
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
                            newPosition.z -= 6f;
                            break;
                        }
                    case "botright":
                        {
                            newPosition.x += 6f;
                            newPosition.z -= 6f;
                            break;
                        }

                }
                transform.position = newPosition;
                // Play sound
                GameObject.FindGameObjectWithTag("soundEffects").GetComponent<SoundEffects>().PlaySoundEffect(moveSound);

                FirebaseDatabase.WriteToPosition("game/positions/"+User.uid, i, gameObject.name, "OnPostSuccess", "OnPostFail");
                if (i == 49)
                {
                    Debug.Log("telos");
                    // Finished game Player is the winner
                    // Show something
                    EndGame();
                    yield break;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        // Let player roll a dice again
        Startingstep = newStep;
        bool isQuestion = CheckIfQuestion();
        bool isEvent = CheckIfEvent();
        if (!isQuestion && !isEvent)
        {
            // change turns..
            // Go write 
            DiceOnline.isRolling = false;
            ServerManager.NextTurn();
        }
    }

    public void EndGame()
    {
        Debug.Log("User with uid: " + User.uid + "...**WON**");
    }

    public bool CheckIfQuestion()
    {
        Question q = Board.GetStepFromIndex(Startingstep).Question;
        if (q != null)
        {
            // TODO: get question window ready
            QuestionManager qm = questionWindow.GetComponent<QuestionManager>();
            qm.playerOnline = this;
            qm.SetQuestion(q);
            qm.questionText.GetComponent<TextMeshProUGUI>().text = qm.Question.question;
            questionWindow.SetActive(true);
            DiceOnline.isRolling = true;
            return true;
        }

        return false;
    }

    public bool CheckIfEvent()
    {
        GameEvent e = Board.GetStepFromIndex(Startingstep).GameEvent;
        if (e != null)
        {
            EventManger em = eventWindow.GetComponent<EventManger>();
            eventWindow.SetActive(true);
            em.playerOnline = this;
            em.SetGameEvent(e);
            DiceOnline.isRolling = true;
            return true;
        }
        return false;

    }

    public void OnPostSuccess(string successMessage)
    {
        Debug.Log("Success! moved player" + successMessage);
    }

    public void OnPostFail(string errorMessage)
    {
        Debug.LogError("Error on post!" + errorMessage);
    }
}

