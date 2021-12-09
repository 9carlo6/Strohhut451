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
            FT_AMMO_COUNT,// va inizializzata
            FT_DAMAGE,
            FT_BURST,
            FT_WEIGHT, // il peso dalla potenza?
            FT_NOISE_RANGE, // il rumore dipende dalla potenza? non credo
            FT_CHANCE_OF_SHOOTING,
            FT_IS_AMMO_INFINITE
        }

        public WeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

    }

    

    public class AmmoInfiniteWeaponFeature : WeaponFeature
    {
        public AmmoInfiniteWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = bool.Parse(m.m_fFactor);

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = !(bool)currentValue ;

        }
        public override void applyFactor(System.Object factor)
        {
            //this.currentValue = (bool)factor;
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

    public class ChancheOfShootingWeaponFeature : WeaponFeature
    {
        public ChancheOfShootingWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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

    public class FireRateWeaponFeature : WeaponFeature
    {
        public FireRateWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (int)currentValue * int.Parse(m.m_fFactor);

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (int)currentValue / int.Parse(m.m_fFactor);

        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (int)this.baseValue * (int)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (int)factor * (int)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return 1;
        }
    }
    public class MaxAmmoCountWeaponFeature : WeaponFeature
    {
        public MaxAmmoCountWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
        {
        }

        public override void performeModifier(Modifier m)
        {
            this.currentValue = (int)currentValue + int.Parse(m.m_fFactor);

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = (int)currentValue - int.Parse(m.m_fFactor);

        }
        public override void applyFactor(System.Object factor)
        {
            this.currentValue = (int)this.baseValue + (int)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            return (int)factor + (int)currentValue;
        }

        public override System.Object initializeFactor()
        {
            return 0;
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

        }

        public override void removeModifier(Modifier m)
        {
           // this.currentValue = (float)currentValue - (float)m.m_fFactor;

        }
        public override void applyFactor(System.Object factor)
        {
            //this.currentValue = (float)this.baseValue + (float)factor;
        }

        public override System.Object updateFactor(System.Object factor)
        {
            //return (float)factor + (float)currentValue;
            return 0;
        }

        public override System.Object initializeFactor()
        {
            return 0;
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
            return 0.0f;
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

        }

        public override void removeModifier(Modifier m)
        {
            this.currentValue = !(bool)this.currentValue;

        }
       
        public override void applyFactor(System.Object factor)
        {
            //this.currentValue = (bool)factor;
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
  
    public class WeightWeaponFeature : WeaponFeature
    {
        public WeightWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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
    public class NoiseRangeWeaponFeature : WeaponFeature
    {
        public NoiseRangeWeaponFeature(System.Object baseValue, FeatureType featureName) : base(baseValue, featureName)
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