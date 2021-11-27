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



    public bool toactive;
    
    public bool infinite;

    public override string ToString()
    {
        return "ID: "+ID+" - "+m_type+" - "+m_fFactor.ToString()+" - "+duration+" - "+toactive+" _ "+ infinite;
    }

    public Modifier(String CID, String m_type,String m_fFactor, float duration)
    {
        this.ID = Guid.NewGuid().ToString("N");

        this.CID = CID;
        this.m_type = m_type;
        this.m_fFactor = m_fFactor;
        this.duration = duration;
        this.toactive = true;

        if (duration == -1)
        {
            this.infinite = true;
        }
        else
        {
            this.infinite = false;
        }
    }
}
