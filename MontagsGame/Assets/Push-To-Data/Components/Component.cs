using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Component : MonoBehaviour
{
	public Dictionary<System.Object, Feature> features;
	public List<Modifier> modifiers;
	public List<Component> components;
	public Dictionable mapper;
	public String ID;
	public String category;

	public Component getComponentbyCategory(String category)
	{
		foreach (Component c in this.components)
		{
			if (c.category == category)
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

		Debug.Log("AAA IO SONO questo COMPONENT game object " + this.gameObject.ToString() + " ID = " + ID +
			"  e aggiungo questi SOTTOcomponenti " + this.gameObject.GetComponentsInChildren<Component>().Length);


		/*foreach (System.Object o in this.gameObject.GetComponentsInChildren<Component>()){
			Debug.Log("CHE SUCCEDE " + o.ToString()+" "+(((WeaponController)o).ID));
        }*/



		foreach (Component c in this.gameObject.GetComponentsInChildren<Component>())
		{
            if (ID != c.ID)
            {
				components.Add(c);
				Debug.Log("AAA IO SONO " + ID + "  E AGGIUNGO QUESTO SOTTOCOMPONENTE DENTRO " + c.ID);

			}

		}


		//Debug.Log("CIAO MONDO SONO  " + ID + this.GetType().FullName);



	}
	public virtual void Start()
	{
		UpdateFeatures();
		setFeatures();
		applyModifiers();
		initializeFeatures();
	}
	public virtual void Update()
	{
		UpdateFeatures();
		applyModifiers();
		setFeatures();
	}

	public void addMoreModifiers(List<Modifier> list)
	{

		foreach(Modifier m in list)
			this.modifiers.Add(m);

		
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

	public abstract void setFeatures();
	public abstract void initializeFeatures();
	public void UpdateFeatures()
	{

		foreach (Feature feature in this.features.Values)
		{

			System.Object factor = feature.initializeFactor();

			foreach (Component c in this.components)
			{
				foreach (Feature f in c.features.Values)
				{
					if (feature.featureName.ToString().Equals(f.featureName.ToString()))
					{
						Debug.Log("FEATURE CON CORRISPONDENZA :" + feature.featureName.ToString());
						factor = f.updateFactor(factor);

					}


				}


			}

			feature.applyFactor(factor);

		}


	}
	/*public void applyModifiers(List<Modifier> modifiers)
	{
		foreach (Component c in this.components)
		{
			c.applyModifiers(modifiers);
		}

		foreach (Modifier modifier in modifiers)
		{


			foreach (Feature f in features.Values)
			{

				//Debug.Log("VEDIAMO ora che succede : " + f.GetType());


				if (f.corrispondance(modifier))
				{

					Debug.Log("MODIFICATORE SULLA FEATURE " + modifier.m_type);

					if (modifier.duration < 0 && !modifier.infinite)
					{
						Debug.Log("RIMUOVO " + modifier.m_type);

						Debug.Log("VALORE ATTUALE " + f.currentValue);

						f.removeModifier(modifier);

						Debug.Log("VALORE AGGIORNATO " + f.currentValue);

					}
					else
					{


						if (modifier.toactive)
						{
							Debug.Log("ATTIVO MODIFICATORE SULLA FEATURE :" + modifier.m_type);
							Debug.Log("VALORE ATTUALE " + f.currentValue);
							f.performeModifier(modifier);
							Debug.Log("VALORE AGGIORNATO " + f.currentValue);

						}

					}

				}

			}
		}

		
	}*/

	/*
	public void applyModifiers()
	{
		List<Modifier> expired = new List<Modifier>();
		foreach (Modifier modifier in modifiers)
		{
			modifier.duration = modifier.duration - Time.deltaTime;

			foreach (Feature f in features.Values)
			{

				//Debug.Log("VEDIAMO ora che succede : " + f.GetType());


				if (f.corrispondance(modifier))
				{

					Debug.Log("MODIFICATORE SULLA FEATURE " + modifier.m_type);

					if (modifier.duration < 0 && !modifier.infinite)
					{
						Debug.Log("RIMUOVO " + modifier.m_type);

						//modifiers.Remove(modifier);

						expired.Add(modifier);
						Debug.Log("VALORE ATTUALE " + f.currentValue);

						f.removeModifier(modifier);

						Debug.Log("VALORE AGGIORNATO " + f.currentValue);

					}
					else
					{

						//da rinominare active che non si capisce  sarebbe " da attivare "

						if (modifier.toactive)
						{
							Debug.Log("ATTIVO MODIFICATORE SULLA FEATURE :" + modifier.m_type);
							Debug.Log("VALORE ATTUALE " + f.currentValue);
							f.performeModifier(modifier);
							Debug.Log("VALORE AGGIORNATO " + f.currentValue);

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
	*/

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
						//Debug.Log("RIMUOVO " + modifier.m_type + "ID: " + modifier.ID);
						Debug.Log("MODIFICATORE SULLA FEATURE " + modifier.m_type+" NON è VALIDO");

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
