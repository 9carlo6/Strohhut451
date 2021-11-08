using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Modifier
{
    public Feature.FeatureType m_type;
    public string m_feature_id; //che corrisponde alla stringa con la quale la feature viene salvata nel dizionario
    public System.Object m_fFactor; //valore da applicare alla feautre

    public Modifier(Feature.FeatureType m_type, string m_feature_id, System.Object m_fFactor)
    {
        this.m_type = m_type;
        this.m_feature_id = m_feature_id;
        this.m_fFactor = m_fFactor;
    }
}
