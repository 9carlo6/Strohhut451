using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;


[Serializable]
public class ModifierJsonMap  
{
    List<Modifier> modifiers;    

    public Modifier getModifierbyID(String ID)
    {
        Debug.Log("CEgggggRCO gg gggg :" + ID);

        foreach (Modifier modifier in modifiers)
        {

            Debug.Log("CERCO CERCO CERCO :" + modifier.ID);

            if (modifier.ID.ToString().Equals(ID))
            {
                return modifier;
            }
        }
        return null;
    }
}
