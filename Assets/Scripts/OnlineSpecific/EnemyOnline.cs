using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Objects;
using Scripts.GameModels;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


public class EnemyOnline : MonoBehaviour
{

    public GameObject questionWindow;

    public FirebaseUser user;

    public int step;

    public string enemyPosition;

    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.name)
        {
            case "herculesHead":
                {
                    enemyPosition = "topleft";
                    break;
                }
            case "perseusHead":
                {
                    enemyPosition = "botleft";
                    break;
                }
            case "achillesHead":
                {
                    enemyPosition = "botright";
                    break;
                }
            case "ippoHead":
                {
                    enemyPosition = "topright";
                    break;
                }
        }
        step = 1;
        Board.SetUpBoard();
        FirebaseDatabase.ListenForValueChanged("game/positions/" + user.uid, gameObject.name, "OnValueChanged", "OnError");
        FirebaseDatabase.ListenForValueChanged("rooms/room1/question/" + user.uid, gameObject.name, "OnQuestionChange", "OnQuestionError");
        FirebaseDatabase.ListenForAnswer("rooms/room1/answers/" + user.uid, gameObject.name, "OnAnswerSuccess", "OnQuestionError");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnValueChanged(string positionIndex)
    {
        int newStep = 0;
        try
        {
            Debug.Log(positionIndex);
            newStep = int.Parse(positionIndex);
            // move to this step
            if (newStep > 1)
                MoveToStep(newStep);
            Debug.Log("newStep" + newStep);
        }
        catch (System.Exception e)
        {

            Debug.LogError("Error parsing position! :" + positionIndex);
            throw new System.Exception(e.Message);
        }



    }

    public void MoveToStep(int newStep)
    {

        Vector3 newPosition = new Vector3();
        MyStep st = Board.GetStepFromIndex(newStep);
        newPosition = st.Position;
        switch (enemyPosition)
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
        step = newStep;

        // TODO :fix issue
        // CheckIfQuestion();
        // CheckIfEvent();
    }


    // public void CheckIfQuestion()
    // {
    //     // Show question window
    //     Question q = Board.GetStepFromIndex(step).Question;
    //     if (q != null)
    //     {
    //         // TODO: get question window ready
    //         QuestionManager qm = GameObject.Find("QuestionWindow").GetComponent<QuestionManager>();
    //         qm.SetQuestion(q);
    //         for (int i = 0; i < qm.answerButtons.Length; i++)
    //         {
    //             qm.answerButtons[i].GetComponent<Button>().interactable = false;
    //         }
    //         qm.questionText.GetComponent<TextMeshProUGUI>().text = qm.Question.question;
    //         GameObject.Find("QuestionWindow").SetActive(true);
    //         DiceOnline.isRolling = true;
    //     }
    // }

    // public void CheckIfEvent()
    // {
    //     GameEvent e = Board.GetStepFromIndex(step).GameEvent;
    //     if (e != null)
    //     {
    //         EventManger em = GameObject.Find("EventWindow").GetComponent<EventManger>();
    //         GameObject.Find("EventWindow").SetActive(true);
    //         em.SetGameEvent(e);
    //         em.okButton.GetComponent<Button>().interactable = false;
    //         // Start cooroutine
    //         StartCoroutine("CloseEvent");
    //         DiceOnline.isRolling = true;
    //     }
    // }

    private IEnumerator CloseEvent()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("EventWindow").GetComponent<EventManger>().okButton.GetComponent<Button>().interactable = true;
        GameObject.Find("EventWindow").SetActive(false);
    }

    public void OnQuestionChange(string json)
    {
        Debug.Log(json);
        if (json == "" || json == null || json == "null")
        {
            Debug.Log("Den yparxei question abort!!");
            return;
        }
        Debug.Log("Try deserialize");
        JObject quuu = JsonConvert.DeserializeObject<JObject>(json);
        Debug.Log("done deserialize");
        // TODO: get question from json
        string[] kati = new string[4];
        kati[0] = "ans0";
        kati[1] = "ans1";
        kati[2] = "ans2";
        kati[3] = "ans3";
        Question question = new Question("poios?", kati, "ans3");
        Debug.Log("done questioning hahaha");
        int ind = 0;
        foreach (JProperty prop in quuu.Properties())
        {
            switch (prop.Name)
            {
                case "text":
                    {
                        question.question = prop.Value.ToString();
                        continue;
                    }
                case "correct":
                    {
                        question.correct = prop.Value.ToString();
                        continue;
                    }

                default:
                    {
                        question.answers[ind] = prop.Value.ToString();
                        ind++;
                        continue;
                    }
            }
        }


        Debug.Log("done getting question");

        Debug.Log("Opening Question Window...");
        // Open question window
        questionWindow.SetActive(true);
        questionWindow.GetComponent<QuestionManager>().SetQuestion(question);
        for (int i = 0; i < 4; i++)
        {
            questionWindow.GetComponent<QuestionManager>().answerButtons[i].GetComponent<Button>().interactable = false;
        }
    }


    public void OnAnswerSuccess(string answerJson)
    {
        Debug.Log(answerJson);
        if (answerJson == "" || answerJson == null || answerJson == "null")
        {
            Debug.Log("To json htan adeio! OnAnswerSuccess");
            return;
        }
        Debug.Log("pige edw prin spasei? 1");
        string answer = answerJson;

        // Show stuff...
        Debug.Log("O antipalos apantise:  " + answer);

        // and close stuff..
        questionWindow.SetActive(false);
        Debug.Log("pige edw prin spasei? 2");

    }

    public void OnQuestionError(string errorMessage)
    {
        Debug.LogError("Error when listening for question " + errorMessage);
    }



}
