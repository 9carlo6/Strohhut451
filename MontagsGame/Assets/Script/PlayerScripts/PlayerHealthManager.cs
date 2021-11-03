using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerHealthManager : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;

    //Per gestire le feature;
  	public Dictionary<string, HumanFeature> features;
  	private HumanFeaturesJsonMap humanMapper;


    // Start is called before the first frame update
    void Start()
    {
        //Inizio - Inizializzazione delle feature
    		string fileString = new StreamReader("Assets/Push-To-Data/Feature/Human/player_features.json").ReadToEnd();
    		humanMapper = JsonUtility.FromJson<HumanFeaturesJsonMap>(fileString);

    		features = new Dictionary<string, HumanFeature>();
    		features.Add("startingHealth", new HumanFeature(humanMapper.FT_HEALTH, HumanFeature.FeatureType.FT_HEALTH));

    		//Da eliminare???
    		startingHealth = (float) features["startingHealth"].currentValue;
    		//Fine - Inizializzazione delle feature

        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //abbassare barra vita grafica se ci sar�
    }

    public void HurtPlayer(float damageAmount)
    {
        currentHealth -= damageAmount;
    }
}
