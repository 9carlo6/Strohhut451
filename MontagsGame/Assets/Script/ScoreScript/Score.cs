using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Score
{
    public string player_name { get; set; }
    public float vote { get; set; }

    public Score(string player_name, float vote)
    {
        this.player_name = player_name;
        this.vote = vote;
    }
}


