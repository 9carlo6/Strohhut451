using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;


[Serializable]
public class EventJsonMap 
{
    public List<Event> events;

    

    public Event getEventbyName(String name)
    {
        foreach (Event  e in events)
        {
            if (e.nomeEvento == name)
            {
                return e;
            }
        }
        return null;
    }


}
[Serializable]
public class Event
{
    public String nomeEvento;
    public List<String> modificatoriIDS;
    public String message;


}
