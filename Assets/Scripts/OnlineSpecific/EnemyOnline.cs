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
        FirebaseDatabase.ListenForValueChanged("game/positions/" + user.uid, gameObject.name, "OnValueChanged", "OnError");
        FirebaseDatabase.ListenForValueChanged("room/room1/question/" + user.uid, gameObject.name, "OnQuestionChange", "OnQuestionError");
        FirebaseDatabase.ListenForValueChanged("room/room1/" + user.uid+"/userAnswer", gameObject.name, "OnAnswerSuccess", "OnQuestionError");
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

            Debug.LogError("Error parsing! :" + positionIndex);
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
        Debug.Log("ola kala");
        JObject quuu = JsonUtility.FromJson<JObject>(json);
        Debug.Log("ola kala 2?");
        Debug.Log(quuu.GetValue("text"));
        // if (json == "" || json == null || json == "null") {
        //     Debug.Log("To json htan adeio! QuestionChange");
        //     return;
        // }
        // // TODO: get question from json
        // Question question = null;
        // try
        // {
        //     JObject questionObj = JsonConvert.DeserializeObject<JObject>(json);
        //     question.question = questionObj.GetValue("text").ToObject<string>();
        //     question.correct = questionObj.GetValue("correct").ToObject<string>();
        //     for (int i = 0; i < 4; i++)
        //     {
        //         question.answers[i] = questionObj.GetValue("answer" + (i + 1)).ToObject<string>();
        //     }
        // }
        // catch (System.Exception e)
        // {
        //     Debug.LogError("Error when deserialize object!");
        //     throw new System.Exception(e.Message);
        // }

        // Debug.Log(question.answers[0]);
        // // Open question window
        // GameObject.Find("QuestionWindow").SetActive(true);
        // GameObject.Find("QuestionWindow").GetComponent<QuestionManager>().SetQuestion(question);
        // for (int i = 0; i < 4; i++)
        // {
        //     GameObject.Find("QuestionWindow").GetComponent<QuestionManager>().answerButtons[i].GetComponent<Button>().interactable = false;
        // }
    }


    public void OnAnswerSuccess(string answerJson)
    {
        Debug.Log(answerJson);
        if (answerJson == "" || answerJson == null || answerJson == "null") {
            Debug.Log("To json htan adeio! OnAnswerSuccess");
            return;
        }
        Debug.Log("pige edw prin spasei? 1");
        string answer = "";
        try
        {
            answer = JsonUtility.FromJson<string>(answerJson);
        }
        catch (System.Exception e)
        {
            Debug.LogError("error when getting answer json!" + e.Message);
            throw new System.Exception(e.Message);
        }
        
        // Show stuff...
        Debug.Log("I apantisi einai: " + answer);


        // and close stuff..
        GameObject.Find("QuestionWindow").SetActive(false);
        Debug.Log("pige edw prin spasei? 2");

    }

    public void OnQuestionError(string errorMessage)
    {
        Debug.LogError("Error when listening for question " + errorMessage);
    }



}
