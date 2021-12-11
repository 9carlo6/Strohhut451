using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using System.IO;
using EnemyFeatures;

//Questa classe serve per la gestione del'animazione del nemico
public class Enemy_type2_Controller : EnemyHuman
{

    public override void Awake()
    {
        base.Awake();

        //Inizio - Inizializzazione delle feature
        string fileString = new StreamReader("Assets/Push-To-Data/Feature/Enemy/enemy_type2_features.json").ReadToEnd();
        mapper = JsonUtility.FromJson<EnemyFeaturesJsonMap>(fileString);


        this.features = mapper.todict();
    }
}
