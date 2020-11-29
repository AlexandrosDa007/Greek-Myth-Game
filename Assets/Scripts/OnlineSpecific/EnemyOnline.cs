using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Objects;
using Scripts.GameModels;
using TMPro;
using UnityEngine.UI;


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
        //FirebaseDatabase.ListenForValueChanged("game/question/" + user.uid + "/answer", gameObject.name, "OnSuccess", "OnQuestionError");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnValueChanged(string positionIndex)
    {
        Debug.Log(positionIndex);
        // int newStep = int.Parse(positionIndex);

        // // move to this step
        // MoveToStep(newStep);
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

        CheckIfQuestion();
        CheckIfEvent();
    }


    public void CheckIfQuestion()
    {
        // Show question window
        Question q = Board.GetStepFromIndex(step).Question;
        if (q != null)
        {
            // TODO: get question window ready
            QuestionManager qm = GameObject.Find("QuestionWindow").GetComponent<QuestionManager>();
            qm.SetQuestion(q);
            for (int i = 0; i < qm.answerButtons.Length; i++)
            {
                qm.answerButtons[i].GetComponent<Button>().interactable = false;
            }
            qm.questionText.GetComponent<TextMeshProUGUI>().text = qm.Question.question;
            GameObject.Find("QuestionWindow").SetActive(true);
            DiceOnline.isRolling = true;
        }
    }

    public void CheckIfEvent()
    {
        GameEvent e = Board.GetStepFromIndex(step).GameEvent;
        if (e != null)
        {
            EventManger em = GameObject.Find("EventWindow").GetComponent<EventManger>();
            GameObject.Find("EventWindow").SetActive(true);
            em.SetGameEvent(e);
            em.okButton.GetComponent<Button>().interactable = false;
            // Start cooroutine
            StartCoroutine("CloseEvent");
            DiceOnline.isRolling = true;
        }
    }

    private IEnumerator CloseEvent()
    {
        yield return new WaitForSeconds(5f);
        GameObject.Find("EventWindow").GetComponent<EventManger>().okButton.GetComponent<Button>().interactable = true;
        GameObject.Find("EventWindow").SetActive(false);
    }

    public void OnSuccess(string json)
    {
        Debug.Log(json);
        string answer = JsonUtility.FromJson<string>(json);

        Debug.Log("User : " + user.uid + "answer : " + answer);
        GameObject.Find("QuestionWindow").SetActive(false);

    }

    public void OnQuestionError(string errorMessage)
    {
        Debug.LogError("Error when listening for question " + errorMessage);
    }



}
