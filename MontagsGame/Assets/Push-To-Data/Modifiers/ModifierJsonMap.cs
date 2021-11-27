using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;


[Serializable]
public class ModifierJsonMap  
{
    public List<ModifierItem> modifiers;    


    public Modifier getModifierbyCID(String CID)
    {

        foreach (ModifierItem modifieritem in modifiers)
        {


            if (modifieritem.CID.ToString().Equals(CID))
            {
                Debug.Log("trovato MODIFIER :" + modifieritem.ToString());


                return new Modifier(modifieritem.CID, modifieritem.m_type, modifieritem.m_fFactor,modifieritem.duration);
            }
        }
        return null;
    }

    [Serializable]
    public class ModifierItem
    {
        public String CID;
        public String m_type;
        public String m_fFactor;
        public float duration;

        public override string ToString()
        {
            return "CID "+CID+" TIPO "+m_type + "FATTORE "+m_fFactor+" DURATA "+duration;
        }
    }
}
