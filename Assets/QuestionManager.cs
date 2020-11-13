using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Question question;

    public Player player;
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

    public void SetQuestion(Question question){
        this.question = question;


        int index = 0;
        foreach (GameObject g in answerButtons)
        {   
            g.GetComponentInChildren<TextMeshProUGUI>().text = this.question.answers[index];
            index++;
        }

    }

    public void OnClickFu(int index) {
        answer = this.question.answers[index];
        if(this.question.correct == answer){
            this.gameObject.SetActive(false);
            player.MovePlayer(1);
        }else{
            Debug.Log("Lathosss!!");
            player.questionWindow.SetActive(false);
            player.MovePlayer(-1);
        }
    }

}
