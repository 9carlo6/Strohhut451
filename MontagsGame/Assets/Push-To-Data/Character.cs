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

	
	public void applyModifiers()
	{
		List<Modifier> expired = new List<Modifier>();
		foreach (Modifier modifier in modifiers)
		{
			modifier.duration = modifier.duration - Time.deltaTime;

			foreach (Feature f in features.Values)
			{

				if (modifier.m_type.Equals(f.featureName))
				{
					Debug.Log("MODIFICATORE SULLA FEATURE " + modifier.m_type);

					if (modifier.duration < 0)
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

						//da rinominare active che non si capisce  sarebbe " da attivare "

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
		}

		foreach (Modifier m in expired)

		{
			modifiers.Remove(m);
		}
	}
	
}
