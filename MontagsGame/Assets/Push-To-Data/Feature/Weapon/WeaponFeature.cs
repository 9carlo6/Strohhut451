using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WeaponFeatures
{

    public abstract class WeaponFeature : Feature
    {
        public enum FeatureType
        {
            FT_FIRE_RATE,
            FT_MAX_AMMO_COUNT,
            FT_AMMO_COUNT,
            FT_DAMAGE,
            FT_BURST,
            //FT_TRACER_EFFECT,
            FT_WEIGHT,
            FT_NOISE_RANGE,
            FT_CHANCE_OF_SHOOTING,
            FT_IS_AMMO_INFINITE
        }

        public WeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

    }

    public class FT_IS_AMMO_INFINITE : WeaponFeature
    {
        public FT_IS_AMMO_INFINITE(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = bool.Parse(m.m_fFactor);
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = !(bool)currentValue ;

        }
    }

    public class Chanche_Of_Shooting : WeaponFeature
    {
        public Chanche_Of_Shooting(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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

    public class FireRateWeaponFeature : WeaponFeature
    {
        public FireRateWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    public class MaxAmmoCountWeaponFeature : WeaponFeature
    {
        public MaxAmmoCountWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    public class AmmoCountWeaponFeature : WeaponFeature
    {
        public AmmoCountWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (int)((int)currentValue * float.Parse(m.m_fFactor));
            m.toactive = false;

        }

        public override void removeModifier(Modifier m)
        {
           // this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }

    }
    public class DamageWeaponFeature : WeaponFeature
    {
        public DamageWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    public class BurstWeaponFeature : WeaponFeature
    {
        public BurstWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    /*
    public class TracerEffectWeaponFeature : WeaponFeature
    {
        public TracerEffectWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            throw new NotImplementedException();
        }

        public override void removeModifier(Modifier m)
        {
            throw new NotImplementedException();

        }

    }
    */
    public class WeightWeaponFeature : WeaponFeature
    {
        public WeightWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    public class NoiseRangeWeaponFeature : WeaponFeature
    {
        public NoiseRangeWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
}