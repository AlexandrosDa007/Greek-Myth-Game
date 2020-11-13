using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
