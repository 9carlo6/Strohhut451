using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature
{
    public virtual void performeModifier(Modifier m)
    {
        Debug.Log("NON SO CHE FARE");
    }
    public virtual void removeModifier(Modifier m)
    {
        Debug.Log("NON SO CHE CANCELLARE");
    }

    //public System.Object FeatureType;

    public System.Object baseValue;
    public System.Object currentValue;
    public System.Object featureName;

    public Feature(System.Object baseValue, System.Object featureName)
    {
        this.baseValue = baseValue;
        this.currentValue = baseValue;
        this.featureName = featureName;
    }


    //public FeatureType featureName;
}
