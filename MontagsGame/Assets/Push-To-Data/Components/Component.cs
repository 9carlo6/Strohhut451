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
		foreach (Component my_component in this.components)
		{
			if (my_component.category == category)
			{
				return my_component;
			}
		}
		return null;
	}

	public virtual void Awake()
	{
		//Per generare un Id casuale
		ID = Guid.NewGuid().ToString("N");

		//Per inizializzazione delle feature, dei modificatori e delle componenti
		features = new Dictionary<System.Object, Feature>();
		modifiers = new List<Modifier>();
		components = new List<Component>();

		//Da cambiare!
		foreach (Component com in this.gameObject.GetComponentsInChildren<Component>())
		{
            if (ID != com.ID)
            {
				components.Add(com);
			}
		}

	}

	//Per richiamare le funzioni del Push-To-Data (all'inizio)
	public virtual void Start()
	{
		UpdateFeatures();
		setFeatures();
		applyModifiers();
		initializeFeatures();
	}

	//Per richiamare le funzioni del Push-To-Data (a ogni frame)
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
	}

	public abstract void setFeatures();
	public abstract void initializeFeatures();

	public void UpdateFeatures()
	{
		foreach (Feature my_feature in this.features.Values)
		{
			//Il fattore puo' essere di vari tipi
			System.Object factor = my_feature.initializeFactor();

			foreach (Component my_component in components)
			{
				foreach (Feature feature_component in my_component.features.Values)
				{
					if (my_feature.featureName.ToString().Equals(feature_component.featureName.ToString()))
					{
						//Il fattore e' interpretato dalla feature
						factor = feature_component.updateFactor(factor);
					}
				}
			}
			//Il fattore e' applicato tramite la feature
			my_feature.applyFactor(factor);
		}
	}

	public void applyModifiers()
	{
		//Lista di tutti i modificatori "scaduti" (in quel frame)
		List<Modifier> expired = new List<Modifier>();

		foreach (Modifier my_modifier in modifiers)		{			my_modifier.duration = my_modifier.duration - Time.deltaTime;
			foreach (Feature my_feature in features.Values)
			{
				//Controlla se il modifcatore si puo' applicare a quella feature
				if (my_feature.corrispondance(my_modifier))
				{
					//Si valuta la validita' del modificatore, se non valida si mette nella lita exiperd, altrimenti viene applicato
					if (!my_modifier.isValid())
					{
						expired.Add(my_modifier);
					}
					else
					{
						my_feature.performeModifier(my_modifier);

						//Se e' oneshot si aggiunge alla lista expiered
						if (my_modifier.oneshot)
						{
							expired.Add(my_modifier);
						}
					}
				}
			}
		}

		//Rimozione di tutti i modificatori nella lista expired
		foreach (Modifier m in expired)
		{			modifiers.Remove(m);		}
	}
}