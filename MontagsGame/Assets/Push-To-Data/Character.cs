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

	public virtual void Awake()
    {
		features = new Dictionary<System.Object, Feature>();
		modifiers = new List<Modifier>();
		components = new List<Component>();
	}

	public void addModifier(Modifier m)
    {
		this.modifiers.Add(m);

		foreach(Component c in components)
        {
			c.addModifier(m);
        }
    }


	public void applyModifiers()
	{
		List<Modifier> expired = new List<Modifier>();
		foreach (Modifier modifier in modifiers)
		{			bool corrispondenza = false;
			modifier.duration = modifier.duration - Time.deltaTime;

			foreach (Feature f in features.Values)
			{

				if (f.corrispondance(modifier))
				{					corrispondenza = true;
					Debug.Log("MODIFICATORE SULLA FEATURE " + modifier.m_type);

					if (modifier.duration < 0 && !modifier.infinite)
					{
						Debug.Log("RIMUOVO " + modifier.m_type +"ID: "+modifier.ID);

						//modifiers.Remove(modifier);

						expired.Add(modifier);

						Debug.Log("VALORE ATTUALE " + f.currentValue);

						f.removeModifier(modifier);

						Debug.Log("VALORE AGGIORNATO " + f.currentValue);
					}
					else
					{


						if (modifier.toactive)
						{
							Debug.Log("ATTIVO MODIFICATORE SULLA FEATURE :" + modifier.m_type + "ID: " + modifier.ID);
							Debug.Log("VALORE ATTUALE " + f.currentValue);
							f.performeModifier(modifier);
							Debug.Log("VALORE AGGIORNATO " + f.currentValue);

						}

					}

                }
               

			}

            if (!corrispondenza)
            {
				// se c'è un modificatore che non agisce su nessuna delle mie featuer lo elimino 
				expired.Add(modifier);
            }

		}

		foreach (Modifier m in expired)

		{
			modifiers.Remove(m);
		}
	}
	
}
