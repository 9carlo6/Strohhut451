using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EnemyFeatures
{

    public abstract class EnemyFeature : Feature
    {
        public enum FeatureType
        {
            FT_VELOCITY,// dipende dal peso
            FT_ACCELERATION,
            FT_DECELERATION,
            FT_HEALTH,// va inizializzata e mai piu gestita 
            FT_MAX_HEALTH,
            FT_MELEE_RANGE,
            FT_MELEE_DAMAGE,
            FT_IS_WEAPONED,
            FT_FIRE_DISTANCE,
            FT_VIEW_ANGLE_CHASING,
            FT_VIEW_ANGLE_PATROLLING,
            FT_VIEW_RADIUS,
            FT_WEIGHT
        }


        public EnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

    }

    public class WeightEnemyFeature : EnemyFeature
    {
        public WeightEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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

            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);


        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

        }
    }
    public class MaxHealthEnemyFeature : EnemyFeature
    {
        public MaxHealthEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    public class SpeedEnemyFeature : EnemyFeature
    {
        public SpeedEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

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
    }

    public class AccelerationEnemyFeature : EnemyFeature
    {
        public AccelerationEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

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
    }
    public class DecelerationEnemyFeature : EnemyFeature
    {
        public DecelerationEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue * float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / float.Parse(m.m_fFactor);

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
    }

    public class HealthEnemyFeature : EnemyFeature
    {
        public HealthEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
         }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

        }

        public override void applyFactor(System.Object factor)
        {
           // this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            // return (float)factor + (float)currentValue;
            return 0f;
        }

        public override System.Object initializeFactor()
        {
            return 0f;
        }

    }

    public class MeleeRangeEnemyFeature : EnemyFeature
    {
        public MeleeRangeEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

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
    }

    public class MeleeDamageEnemyFeature : EnemyFeature
    {
        public MeleeDamageEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

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
    }

    public class IsWeaponedEnemyFeature : EnemyFeature
    {
        public IsWeaponedEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
            this.currentValue = (bool)factor;
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

    public class FireDistanceEnemyFeature : EnemyFeature
    {
        public FireDistanceEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

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
    }

    public class ViewAngleChasingEnemyFeature : EnemyFeature
    {
        public ViewAngleChasingEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

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
    }

    public class ViewAnglePatrollingEnemyFeature : EnemyFeature
    {
        public ViewAnglePatrollingEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

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
    }

    public class ViewRadiusEnemyFeature : EnemyFeature
    {
        public ViewRadiusEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + float.Parse(m.m_fFactor);
 
        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - float.Parse(m.m_fFactor);

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
    }
}