using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Question
{
    public string question;
    public string[] answers;
    public string correct;

    public Question(string question, string[] answers, string correct)
    {
        this.question = question;
        this.answers = answers;
        this.correct = correct;
    }
}
