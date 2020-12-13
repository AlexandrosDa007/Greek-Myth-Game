using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.Objects;

namespace Scripts.GameModels
{


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

        public Question()
        {

        }
    }

    [Serializable]
    public class GameEvent
    {

        public string title;
        public string description;
        public bool isGood;
        public int steps;

        public GameEvent(string title, string description, bool isGood, int steps)
        {
            this.title = title;
            this.description = description;
            this.isGood = isGood;
            this.steps = steps;
        }
    }

    public class MyStep
    {

        private Vector3 position;
        private int index;
        private Question question;
        private GameEvent gameEvent;
        private string[] playersInside;

        public MyStep(Vector3 position, int index)
        {
            this.Position = position;
            this.Index = index;
        }



        public Vector3 Position { get => position; set => position = value; }
        public int Index { get => index; set => index = value; }
        public Question Question { get => question; set => question = value; }
        public GameEvent GameEvent { get => gameEvent; set => gameEvent = value; }
    }


    [Serializable]
    public class JRoom
    {
        public string roomHost { get; set; }
        public string roomName { get; set; }
        public int maxPlayers { get; set; }
        public int activePlayers { get; set; }
        public string difficulty { get; set; }
        public string roomId { get; set; }
        public JUser[] players { get; set; }
    }
    [Serializable]
    public class JUser
    {
        public string uid {get; set;}
        public string displayName {get;set;}
        public bool ready {set;get;}   
    }
}