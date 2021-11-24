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
            FT_VELOCITY,
            FT_ACCELERATION,
            FT_DECELERATION,
            FT_HEALTH,
            FT_MELEE_RANGE,
            FT_MELEE_DAMAGE,
            FT_IS_WEAPONED,
            FT_FIRE_DISTANCE,
            FT_VIEW_ANGLE_CHASING,
            FT_VIEW_ANGLE_PATROLLING,
            FT_VIEW_RADIUS
        }


        public EnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

    }

    public class SpeedEnemyFeature : EnemyFeature
    {
        public SpeedEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue * (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / (float)m.m_fFactor;

        }
    }

    public class AccelerationEnemyFeature : EnemyFeature
    {
        public AccelerationEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue * (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / (float)m.m_fFactor;

        }
    }
    public class DecelerationEnemyFeature : EnemyFeature
    {
        public DecelerationEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue * (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue / (float)m.m_fFactor;

        }
    }

    public class HealthEnemyFeature : EnemyFeature
    {
        public HealthEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }

    public class MeleeRangeEnemyFeature : EnemyFeature
    {
        public MeleeRangeEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }

    public class MeleeDamageEnemyFeature : EnemyFeature
    {
        public MeleeDamageEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }

    public class IsWeaponedEnemyFeature : EnemyFeature
    {
        public IsWeaponedEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }
        public override void performeModifier(Modifier m)
        {
            this.currentValue = (bool)m.m_fFactor;
            m.toactive = false;
        }
        public override void removeModifier(Modifier m)
        {
            this.currentValue = !(bool)this.currentValue;

        }
    }

    public class FireDistanceEnemyFeature : EnemyFeature
    {
        public FireDistanceEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }

    public class ViewAngleChasingEnemyFeature : EnemyFeature
    {
        public ViewAngleChasingEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }

    public class ViewAnglePatrollingEnemyFeature : EnemyFeature
    {
        public ViewAnglePatrollingEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }

    public class ViewRadiusEnemyFeature : EnemyFeature
    {
        public ViewRadiusEnemyFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue + (float)m.m_fFactor;
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
    }
}