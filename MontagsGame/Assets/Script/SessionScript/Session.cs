using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Session
{
    public int session_id;
    public int chapter;
    public float final_score;
    public bool is_completed;
    public string player_name;
    public List<Scene> scenes;
}