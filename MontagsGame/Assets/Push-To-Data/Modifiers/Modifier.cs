using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Modifier
{
    //ID
    public String ID;
    public String CID;
    public String  m_type;
    public String m_fFactor; 
    public float duration;
    public bool infinite;
    public bool oneshot;

    public override string ToString()
    {
        return "ID: "+ID+" - "+m_type+" - "+m_fFactor.ToString()+" - "+duration+" - "+" _ "+ infinite+"_"+oneshot;
    }

    public bool isValid()
    {
        if (this.infinite || oneshot) return true;
        else
            return duration > 0;
    }

    public Modifier(String CID, String m_type,String m_fFactor, float duration,bool infinite,bool oneshot)
    {
        this.ID = Guid.NewGuid().ToString("N");

        this.CID = CID;
        this.m_type = m_type;
        this.m_fFactor = m_fFactor;
        this.duration = duration;
        this.infinite = infinite;
        this.oneshot = oneshot;
    }
}