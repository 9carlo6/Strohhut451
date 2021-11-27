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
            FT_SPEED,
            FT_HEALTH,
            FT_ATTACK_RANGE,
            FT_MELEE_DAMAGE,
            FT_INCREASED_FOV
        }
        

        public HumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

    }



    public class SpeedHumanFeature : HumanFeature
    {
        public SpeedHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {

            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);


            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

        }
    }
    public class HealthHumanFeature : HumanFeature
    {
        public HealthHumanFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
            m.toactive = false;
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
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
            m.toactive = false;
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
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
            m.toactive = false;
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
            m.toactive = false;
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = !(bool)this.currentValue;

        }
    }


}