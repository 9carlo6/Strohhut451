using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;

public abstract  class Character : MonoBehaviour
{
	public Dictionary<System.Object,Feature> features;
	public List<Modifier> modifiers;
	public List<Component> components;
	public Dictionable mapper;
	public String ID;
	public WeaponController weaponController;


	public Component getComponentbyCategory(String category)
    {
		foreach(Component c in this.components)
        {
			if(c.category == category)
            {
				return c;
            }
        }
		return null;
    }

	public virtual void Awake()
    {
		ID = Guid.NewGuid().ToString("N");

		features = new Dictionary<System.Object, Feature>();
		modifiers = new List<Modifier>();
		components = new List<Component>();

		Debug.Log("AAA IO SONO questo Character game object " + this.gameObject.ToString()+" ID = "+ID+
			"  e aggiungo questi componenti " + this.gameObject.GetComponentsInChildren<Component>().Length);


		/*foreach (System.Object o in this.gameObject.GetComponentsInChildren<Component>()){
			Debug.Log("CHE SUCCEDE " + o.ToString()+" "+(((WeaponController)o).ID));
        }*/

		components.AddRange(this.gameObject.GetComponentsInChildren<Component>());

		weaponController = null;

		foreach (Component c in components)
        {
			Debug.Log("AAA IO SONO "+ID+ "  E AGGIUNGO QUESTO COMPONENTE " + c.ID);

			if( c is WeaponController)
            {
				weaponController = (WeaponController)c;

			}
        }



		
	}
	public virtual void Start()
    {
		UpdateFeatures();
		applyModifiers();
		setFeatures();
		initializeFeatures();
	}

	public virtual void Update()
    {
		UpdateFeatures();
		applyModifiers();
		setFeatures();
	}
	
	public void addModifier(Modifier m)
    {
		this.modifiers.Add(m);

		/*
		foreach(Component c in components)
        {
			c.addModifier(m);
        }
		*/
    }

	public void addMoreModifiers(List<Modifier> list)
	{

		foreach (Modifier m in list)
			this.modifiers.Add(m);


	}

	public abstract void setFeatures(); 
	public abstract void initializeFeatures();


	public void UpdateFeatures()
    {

		foreach(Feature feature in this.features.Values)
        {

			System.Object factor = feature.initializeFactor();

			foreach(Component  c in components)
            {
				foreach(Feature f in c.features.Values)
                {
					if (feature.featureName.ToString().Equals(f.featureName.ToString()))
					{
						//Debug.Log("FEATURE CON CORRISPONDENZA :" + feature.featureName.ToString());
						factor = f.updateFactor(factor);

                    }


				}


			}

			feature.applyFactor(factor);

        }

		
	}

	public void applyModifiers()
	{
		List<Modifier> expired = new List<Modifier>();

		foreach (Modifier modifier in modifiers)
		{
			modifier.duration = modifier.duration - Time.deltaTime;
			foreach (Feature f in features.Values)
			{

				if (f.corrispondance(modifier))
				{
					Debug.Log("MODIFICATORE SULLA FEATURE " + modifier.m_type);

					if (!modifier.isValid())
					{
						Debug.Log("RIMUOVO " + modifier.m_type +"ID: "+modifier.ID);

						//modifiers.Remove(modifier);

						//Debug.Log("VALORE ATTUALE " + f.currentValue);

						expired.Add(modifier);

						//f.removeModifier(modifier);

						//Debug.Log("VALORE AGGIORNATO " + f.currentValue);
					}
					else
					{
							Debug.Log("ATTIVO MODIFICATORE SULLA FEATURE :" + modifier.m_type + "ID: " + modifier.ID);
							Debug.Log("VALORE ATTUALE " + f.currentValue);
							f.performeModifier(modifier);
							Debug.Log("VALORE AGGIORNATO " + f.currentValue);

                        if (modifier.oneshot)
                        {
							expired.Add(modifier);
                        }

					}

                }
               

			}


		}

		foreach (Modifier m in expired)

		{
			modifiers.Remove(m);
		}
	}
	
}
