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
            if ((!modifieritem.infinite && !modifieritem.oneshot && (modifieritem.duration <= 0))||(modifieritem.oneshot && modifieritem.infinite))
                throw new DataException("NON HA SENSO QUESTO MODIFICATORE CON TEMPO NEGATIVO "+modifieritem.CID);

            if (modifieritem.CID.ToString().Equals(CID))
            {
                Debug.Log("trovato MODIFIER :" + modifieritem.ToString());


                return new Modifier(modifieritem.CID, modifieritem.m_type, modifieritem.m_fFactor,modifieritem.duration,modifieritem.infinite,modifieritem.oneshot);
            }
        }
        throw new DataException("NON HO TROVATO NESSUN MODIFICATORE " + CID);
    }
    public List<Modifier> getMoreModifiersbyCIDs(List<String> cids)
    {
        List<Modifier> newmodifiers = new List<Modifier>();

        foreach (String cid in cids)
        {
            foreach (ModifierItem modifieritem in modifiers)
            {
                if (((!modifieritem.infinite && !modifieritem.oneshot && (modifieritem.duration <= 0))) || (modifieritem.oneshot && modifieritem.infinite))
                    throw new DataException("NON HA SENSO QUESTO MODIFICATORE CON TEMPO NEGATIVO " + modifieritem.CID);

                if (modifieritem.CID.ToString().Equals(cid))
                {
                    Debug.Log("trovato MODIFIER :" + modifieritem.ToString());


                    newmodifiers.Add( new Modifier(modifieritem.CID, modifieritem.m_type, modifieritem.m_fFactor, modifieritem.duration, modifieritem.infinite, modifieritem.oneshot));
                }
            }
        }
        if(modifiers.Count>=0)
            return newmodifiers;
        throw new DataException("NON HO TROVATO NESSUN MODIFICATORE " + cids);
    }

    [Serializable]
    public class ModifierItem
    {

        public String CID;
        public String m_type;
        public String m_fFactor;
        public float duration;
        public bool infinite;
        public bool oneshot;


        public override string ToString()
        {
            return "CID "+CID+" TIPO "+m_type + "FATTORE "+m_fFactor+" DURATA "+duration;
        }
    }
}
