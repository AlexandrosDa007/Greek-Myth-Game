﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using Scripts.GameModels;

[Serializable]
public class IQuestions
{
    public Question[] questions;
}

[Serializable]
public class IGameEvents
{
    public GameEvent[] gameevents;
}


public class Board : MonoBehaviour
{
    public static bool isMultiplayer = false;
    public static List<MyStep> stepList = new List<MyStep>(49);

    public static Question[] QUESTION_LIST;
    public static GameEvent[] GAME_EVENTS_LIST;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void SetUpBoard()
    {
        if (isMultiplayer)
            InitializeQuestions();

        InitializeGameEvents();
        GameObject[] steps = GameObject.FindGameObjectsWithTag("platform");
        foreach (GameObject st in steps)
        {
            int i = int.Parse(st.GetComponent<TextMeshPro>().text);
            MyStep newStep = new MyStep(st.transform.position, i);


            // TODO: Find a better way to implement questions
            if (isMultiplayer)
            {
                if (i == 3 || i == 5 || i == 25 || i == 27 || i == 29 || i == 23 || i == 42 || i == 39 || i == 10 || i == 21 || i == 46 || i == 11 || i == 37 || i == 33 || i == 17 || i == 15)
                {
                    int rnumber = Mathf.FloorToInt(UnityEngine.Random.Range(0, 2));
                    newStep.Question = QUESTION_LIST[rnumber];
                }

            }
            if (i == 4 || i == 8 || i == 40 || i == 41 || i == 31 || i == 9 || i == 47 || i == 34 || i == 13 || i == 19 || i == 16)
            {
                int rnumber = Mathf.FloorToInt(UnityEngine.Random.Range(0, 2));
                newStep.GameEvent = GAME_EVENTS_LIST[rnumber];
            }
            stepList.Add(newStep);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }

    public static MyStep GetStepFromIndex(int index)
    {
        foreach (MyStep step in stepList)
        {
            if (index == step.Index)
            {
                return step;
            }
        }
        return null;
    }


    public static void InitializeQuestions()
    {
        TextAsset kati = Resources.Load<TextAsset>("questions");
        string json = kati.text;
        //reader.Close();
        IQuestions qs = JsonUtility.FromJson<IQuestions>(json);
        // qs.questions this contains all the questions 
        QUESTION_LIST = qs.questions;


    }

    public static void InitializeGameEvents()
    {
        //StreamReader reader = new StreamReader("Assets/gameevents.json");
        TextAsset kati = Resources.Load<TextAsset>("gameevents");
        string json = kati.text;
        Debug.Log(json);
        //reader.Close();
        IGameEvents qs = JsonUtility.FromJson<IGameEvents>(json);
        // qs.questions this contains all the questions 
        GAME_EVENTS_LIST = qs.gameevents;
    }
}


