using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature
{
    public abstract void performeModifier(Modifier m);

    public abstract void removeModifier(Modifier m);

    public abstract void applyFactor(System.Object factor);

    public abstract System.Object updateFactor(System.Object factor);

    public abstract System.Object initializeFactor();

    public System.Object baseValue;
    public System.Object currentValue;
    public System.Object featureName;

    public Feature(System.Object baseValue, System.Object featureName)
    {
        this.baseValue = baseValue;
        this.currentValue = baseValue;
        this.featureName = featureName;
    }

    public bool corrispondance(Modifier m)
    {
        return this.featureName.ToString().Equals(m.m_type.ToString());
    }
}
