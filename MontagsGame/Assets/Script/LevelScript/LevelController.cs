using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    private GameObject levelInfoCanvas;

    //Per controllare il tempo impiegato per superare il livello
    public float levelTimeCounter = 0;

    //Per gestire i punti accumulati nel livello
    public GameObject pointsText;
    public int levelPoints = 0;

    //Per gestire la combo
    public GameObject comboText;
    public float comboTimeCounter = 0;
    public int comboMultiplier = 0;

    //Per gestire i vari tipi di punteggio
    public int enemyKillScore = 250;

    //Per controllare il numero corrente di nemici presenti nel livello
    private int currentNumberOfEnemies;
    public GameObject enemiesNumberText;


    // Start is called before the first frame update
    void Start()
    {
        //Per accedere al canvas relativo alle info del livello
        levelInfoCanvas = transform.Find("LevelInfoCanvas").gameObject;

        //Per prendere il numero corrente di nemici presenti nel livello
        currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Aggiorna il numero di nemici sulla UI
        enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', currentNumberOfEnemies);
    }

    // Update is called once per frame
    void Update()
    {
        //Per modificare incrementare il valore del cronometro del tempo impiegato per superare il livello
        if(currentNumberOfEnemies > 0)
        {
            levelTimeCounter += Time.deltaTime;
        }

        //Per gestire i punti da assegnare ogni volta che si uccide un nemico
        if (currentNumberOfEnemies != GameObject.FindGameObjectsWithTag("Enemy").Length)
        {
            //Per prendere il numero corrente di nemici presenti nel livello
            currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            //Il moltiplicatore viene incrementato a ogni nemico ucciso
            comboMultiplier++;

            //Per aggiornare il valore del moltiplicatore sulla UI
            comboText.GetComponent<TMP_Text>().text = (comboMultiplier + 1).ToString() + " " + "X";

            //Il punti vengono aggiornato 
            levelPoints += enemyKillScore * comboMultiplier;

            //Quando si uccide un nemico il contatore viene riportato a 5 secondi
            comboTimeCounter = 5;

            //Aggiorna il numero di nemici sulla UI
            enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', currentNumberOfEnemies);
        }

        //Per modificare il valore del testo relativo ai punti accumulati nel livello (DA RIMUOVERE CONTATORE)
        if (comboTimeCounter > 0)
        {
            comboTimeCounter -= Time.deltaTime;
        }
        else
        {
            //Se il valore del contatore arriva a 0 il moltiplicatore combo viene messo a 0
            comboMultiplier = 0;

            //Per aggiornare il valore del moltiplicatore sulla UI
            comboText.GetComponent<TMP_Text>().text = " ";
        }

        pointsText.GetComponent<TMP_Text>().text = levelPoints.ToString() + " " + "PT";
    }
}
