using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HumanFeatures;

public abstract  class Character : MonoBehaviour
{
	public System.Object features;
	public List<Modifier> modifiers;
	public List<Component> components;

	public virtual void Awake()
    {
		modifiers = new List<Modifier>();
		components = new List<Component>();
	}

	public abstract void  applyModifiers();
	/*
	public void applyModifier()
	{
		foreach (Modifier modifier in modifiers)
		{
			modifier.duration = modifier.duration - Time.deltaTime;


			System.Type[] arguments = features.GetType().GetGenericArguments();
			System.Type keyType = arguments[0];
			System.Type valueType = arguments[1];

			/*Debug.Log("VEDIAMO FEATURE CHE COSA  é : " + features.GetType());

			Debug.Log("RISOLVIAMO " + keyType.ToString()+"-------"+valueType.ToString());

			Dictionary<String, tipeof(valueType)>.ValueCollection valori = ((Dictionary<String, valueType>)features).Values;
			

			foreach (Feature f in features.Values)
			{
				Debug.Log("VEDIAMO ora che succede : " + f.GetType());

				if (modifier.m_type.Equals(f.featureName))
				{
					if (modifier.duration < 0)
					{
						modifiers.Remove(modifier);
						f.removeModifier(modifier);
					}
					else
					{
						//da rinominare active che non si capisce  sarebbe " da attivare "
						if (modifier.active)
						{
							f.performeModifier(modifier);

						}

					}

				}

			}
		}
	}
	*/
}
