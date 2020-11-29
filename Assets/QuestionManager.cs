﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scripts.GameModels;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Question question;

    public Player player;


    //Online specific
    public PlayerOnline playerOnline;

    public Enemy enemy;
    public GameObject questionText;

    public GameObject[] answerButtons;

    public Question Question { get => question; set => question = value; }

    public string answer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuestion(Question question)
    {
        this.question = question;

        for (int i =  0;i< answerButtons.Length;i++)
        {
            answerButtons[i].GetComponent<Button>().interactable = true;
        }

        int index = 0;
        foreach (GameObject g in answerButtons)
        {
            g.GetComponentInChildren<TextMeshProUGUI>().text = this.question.answers[index];
            index++;
        }

    }

    public void OnClickFu(int index)
    {
        answer = this.question.answers[index];

        if (Board.isMultiplayer)
        {
            FirebaseDatabase.PostJSON("game/question/" + playerOnline.User.uid + "/answer", answer, gameObject.name, "OnSuccess", "OnError");
            return;
        }

        if (this.question.correct == answer)
        {
            this.gameObject.SetActive(false);
            player.MovePlayer(1);
        }
        else
        {
            // TODO: Display better wrong message!!
            Debug.Log("Lathosss!!");
            player.questionWindow.SetActive(false);
            player.MovePlayer(-1);
        }
    }

    public void OnSuccess(string data)
    {
        // Everything went ok
        if (this.question.correct == answer)
        {
            this.gameObject.SetActive(false);
            playerOnline.Move(1);
            return;
        }
        else
        {
            // TODO: Display better wrong message!!
            Debug.Log("Lathosss!!");
            player.questionWindow.SetActive(false);

            playerOnline.Move(-1);
            return;

        }
    }

    public void OnError(string errorMessage)
    {
        Debug.LogError("Error when writing answer " + errorMessage);
    }

}
