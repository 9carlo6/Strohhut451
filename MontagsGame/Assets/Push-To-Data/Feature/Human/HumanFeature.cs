using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HumanFeatures
{

    public abstract class HumanFeature : Feature
    {
        
        public enum FeatureType
        {
            FT_SPEED, // va settata
            FT_HEALTH,// va inizializzata
            FT_MAX_HEALTH,
            FT_ATTACK_RANGE,
            FT_MELEE_DAMAGE,
            FT_INCREASED_FOV,
            FT_WEIGHT
        }
        

        public HumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

    }

    public class WeightHumanFeature : HumanFeature
    {
        public WeightHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }


        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (float)factor + (float)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return  0f;
        }

        public override void performeModifier(Modifier m)
        {

            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);


        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

        }
    }

    public class SpeedHumanFeature : HumanFeature
    {
        public SpeedHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (float)this.baseValue * (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (float)factor * (float)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return 1f;
        }
        public override void performeModifier(Modifier m)
        {

            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);



        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

        }
    }
    public class MaxHealthHumanFeature : HumanFeature
    {
        public MaxHealthHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (float)factor + (float)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return 0f;
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

        }
    }
    public class HealthHumanFeature : HumanFeature
    {
        public HealthHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void applyFactor(System.Object factor)
        {
            //this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            //return (float)factor + (float)currentValue;
            return 0f;
        }

        public override System.Object initializeFactor()
        {
            return 0f;
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

        }
    }
    public class AttackRangeHumanFeature : HumanFeature
    {
        public AttackRangeHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (float)factor + (float)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return 0f;
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

        }
    }
    public class MeleeDamageHumanFeature : HumanFeature
    {
        public MeleeDamageHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (float)factor + (float)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return 0f;
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

        }
    }
    public class IncreasedFovHumanFeature : HumanFeature
    {
        public IncreasedFovHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = bool.Parse(m.m_fFactor);
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = !(bool)this.currentValue;

        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue =  (bool)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return currentValue;
        }

        public override System.Object initializeFactor()
        {
            return false;
        }
    }


}