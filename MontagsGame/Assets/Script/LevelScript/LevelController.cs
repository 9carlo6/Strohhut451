using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private GameObject levelInfoCanvas;

    //Per controllare il tempo impiegato per superare il livello
    [HideInInspector]
    public float levelTimeCounter;

    //Per gestire i punti accumulati nel livello
    [HideInInspector]
    public GameObject pointsText;
    [HideInInspector]
    public int levelPoints;

    //Per gestire la combo
    [HideInInspector]
    public GameObject comboText;
    [HideInInspector]
    public float comboTimeCounter;
    [HideInInspector]
    public int comboMultiplier;

    //Per gestire i vari tipi di punteggio
    [HideInInspector]
    public int enemyKillScore;

    //Per controllare il numero corrente di nemici presenti nel livello
    [HideInInspector]
    public int currentNumberOfEnemies;
    [HideInInspector]
    public GameObject enemiesNumberText;

    //Per gestire il numero di monete raccolte
    [HideInInspector]
    public GameObject coinsText;
    [HideInInspector]
    public int currentCoins;

    //Per gestire il numero di munizioni
    [HideInInspector] 
    public GameObject ammoText;
    [HideInInspector] 
    public WeaponController weapon;

    //Singleton
    public static LevelController lcstatic;

    // Start is called before the first frame update
    void Awake()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;

        //Singleton
        //Diversa da 0 perchè la scena 0 è il menu
        if (lcstatic == null && currentSceneId != 0)
        {
            lcstatic = this;
            DontDestroyOnLoad(gameObject);

            levelTimeCounter = 0;
            levelPoints = 0;
            comboTimeCounter = 0;
            comboMultiplier = 0;
            enemyKillScore = 250;
            currentCoins = 0;

            pointsText = GameObject.FindWithTag("PointsText");
            comboText = GameObject.FindWithTag("ComboText");
            enemiesNumberText = GameObject.FindWithTag("EnemiesNumberText");
            coinsText = GameObject.FindWithTag("CoinsText");
            ammoText = GameObject.FindWithTag("AmmoText");

            //Per accedere al canvas relativo alle info del livello
            levelInfoCanvas = transform.Find("LevelInfoCanvas").gameObject;

            //Per prendere il numero corrente di nemici presenti nel livello
            currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

            //Aggiorna il numero di nemici sulla UI
            enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', currentNumberOfEnemies);
        }
        else
        {
            Destroy(gameObject);
        }
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

        //Per modificare il valore del testo relativo al punteggio
        pointsText.GetComponent<TMP_Text>().text = levelPoints.ToString() + " " + "PT";


        //Per modificare il valore del testo relativo alle monete raccolte
        coinsText.GetComponent<TMP_Text>().text = currentCoins.ToString() + " " + "$";

        if(GameObject.FindWithTag("PlayerWeapon") != null)
        {
            weapon = GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponPlayerController>();
            //Per modificare il valore delle munizioni
            ammoText.GetComponent<TMP_Text>().text = weapon.ammoCount.ToString() + "/" + weapon.maxAmmoCount.ToString();
        }
    }
}
