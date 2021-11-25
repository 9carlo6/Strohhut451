using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Modifier
{
    //ID
    public String ID;
    public System.Object m_type;
    //public string m_feature_id; //che corrisponde alla stringa con la quale la feature viene salvata nel dizionario
    public System.Object m_fFactor; //valore da applicare alla feautre
    public float duration;



    [NonSerialized]
    public bool toactive;
    [NonSerialized]
    public bool infinite;

  

    public Modifier(String ID, System.Object m_type, System.Object m_fFactor, float duration)
    {
        this.ID = ID;
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
    public Modifier(System.Object m_type, System.Object m_fFactor,float duration)
    {
        this.ID = Guid.NewGuid().ToString("N");
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
