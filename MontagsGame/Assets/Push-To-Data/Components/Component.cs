using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Component : MonoBehaviour
{
	public System.Object features;
	public List<Modifier> modifiers;
	public List<Component> components;

	public virtual void Awake()
	{
		modifiers = new List<Modifier>();
		components = new List<Component>();
	}

	public abstract void applyModifiers();

	/*
	public void applyModifier()
	{
		foreach (Modifier modifier in modifiers)
		{
			modifier.duration = modifier.duration - Time.deltaTime;


			foreach (Feature f in ((Dictionary<System.Object, Feature>)features).Values)
			{
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
