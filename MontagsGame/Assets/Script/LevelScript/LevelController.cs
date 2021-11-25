using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using HumanFeatures;


public class LevelController : MonoBehaviour
{
    [HideInInspector] public GameObject sessionController;
    [HideInInspector] public SessionController sc;

    public GameObject levelInfoCanvas;

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
    public int currentNumberOfEnemies,NumberOfEnemiesCheck;
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

    //Questi paramentri servono per salvare i valori validi relativi allo score del livello
    //Quando il giocatore va al livello successivo devono essere aggiornati
    [HideInInspector] public int valid_levelPoints;
    [HideInInspector] public int valid_currentCoins;
    [HideInInspector] public int valid_currentNumberOfEnemies;
    [HideInInspector] public float valid_levelTimeCounter;
    //Parametri legati ai gameItems
    [HideInInspector] public float valid_skulls_amount;
    [HideInInspector] public float valid_helms_amount;
    [HideInInspector] public float valid_telescopes_amount;

    //Per la gestione delle avarie
    //public float BreakdownTimeCounter;
    public GameObject breakdownCanvas;
    public Image firstBreakdownImage;
    public Image secondBreakdownImage;
    public Image thirdBreakdownImage;

    //Per gestire il testo della radio
    public RadioController radioController;

    //Per gestire i gameItems
    public TMP_Text skulls_amount_text;
    public TMP_Text helms_amount_text;
    public TMP_Text telescopes_amount_text;

    public int currentFailure;

    //Singleton
    public static LevelController lcstatic;


    ModifierJsonMap modifiersjson;
    EventJsonMap events;

    // Start is called before the first frame update
    void Awake()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;



        string fileStringevents = new StreamReader("Assets/Push-To-Data/Modifiers/Events.txt").ReadToEnd();
        events = JsonUtility.FromJson<EventJsonMap>(fileStringevents);


        string fileStringmodifiers = new StreamReader("Assets/Push-To-Data/Modifiers/Modifiers.txt").ReadToEnd();
        modifiersjson = JsonUtility.FromJson<ModifierJsonMap>(fileStringmodifiers);

        currentFailure = 0;

        //Singleton
        //Diversa da 0 perch� la scena 0 � il menu
        if (lcstatic == null && currentSceneId != 0)
        {
            lcstatic = this;
            DontDestroyOnLoad(gameObject);

            sessionController = GameObject.FindWithTag("SessionController");
            sc = sessionController.GetComponent<SessionController>();

            levelTimeCounter = 0;
            levelPoints = 0;
            comboTimeCounter = 0;
            comboMultiplier = 0;
            enemyKillScore = 250;
            currentCoins = 0;
            valid_levelPoints = 0;
            valid_currentCoins = 0;
            valid_levelTimeCounter = 0;
            

            pointsText = GameObject.FindWithTag("PointsText");
            comboText = GameObject.FindWithTag("ComboText");
            enemiesNumberText = GameObject.FindWithTag("EnemiesNumberText");
            coinsText = GameObject.FindWithTag("CoinsText");
            ammoText = GameObject.FindWithTag("AmmoText");

            //Per accedere al canvas relativo alle info del livello
            levelInfoCanvas = transform.Find("LevelInfoCanvas").gameObject;

            //Per prendere il numero corrente di nemici presenti nel livello
            valid_currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
            NumberOfEnemiesCheck = GameObject.FindGameObjectsWithTag("Enemy").Length;

            //Aggiorna il numero di nemici sulla UI
            enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', currentNumberOfEnemies);

            //Per gestire il testo della radio
            radioController = GameObject.FindWithTag("RadioController").GetComponent<RadioController>();

            //Per gestire i gameItems
            skulls_amount_text.text = sc.skulls_amount.ToString();
            helms_amount_text.text = sc.helms_amount.ToString();
            telescopes_amount_text.text = sc.telescopes_amount.ToString();
            valid_skulls_amount = sc.skulls_amount;
            valid_helms_amount = sc.helms_amount;
            valid_telescopes_amount = sc.telescopes_amount;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Funzione necessaria per controllare se la scena corrente è un livello
    //In caso contrario bisogna distruggere l'oggetto LevelController
    public void CheckSceneType()
    {
        //Se la scena corrente non contiene la parola level allora viene cancellata
        if (!SceneManager.GetActiveScene().name.ToString().Contains("Level"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Continua costantemente la tipologia della scena corrente
        //Se non è un livello distrugge l'oggetto LevelController
        CheckSceneType();

        //Per modificare incrementare il valore del cronometro del tempo impiegato per superare il livello
        if (currentNumberOfEnemies > 0)
        {
            levelTimeCounter += Time.deltaTime;
        }
        NumberOfEnemiesCheck = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //Per gestire i punti da assegnare ogni volta che si uccide un nemico
        if (currentNumberOfEnemies > NumberOfEnemiesCheck)
        {
            //Per prendere il numero corrente di nemici presenti nel livello
            currentNumberOfEnemies=NumberOfEnemiesCheck;

            //Il moltiplicatore viene incrementato a ogni nemico ucciso
            comboMultiplier++;

            //Per aggiornare il valore del moltiplicatore sulla UI
            comboText.GetComponent<TMP_Text>().text = (comboMultiplier + 1).ToString() + " " + "X";

            //Il punti vengono aggiornato
            levelPoints += enemyKillScore * comboMultiplier;

            //Quando si uccide un nemico il contatore viene riportato a 5 secondi
            comboTimeCounter = 5;
        }
        else if(currentNumberOfEnemies < NumberOfEnemiesCheck){
          currentNumberOfEnemies=NumberOfEnemiesCheck;
          levelPoints=valid_levelPoints;
          currentCoins=valid_currentCoins;
          comboMultiplier=0;
          comboText.GetComponent<TMP_Text>().text = " ";
        }

        //Per modificare il valore del testo relativo ai punti accumulati nel livello (DA RIMUOVERE CONTATORE)
        if (comboTimeCounter > 0 )
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

        //Aggiorna il numero di nemici sulla UI
        enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', currentNumberOfEnemies);

        //Per modificare il valore del testo relativo al punteggio
        pointsText.GetComponent<TMP_Text>().text = levelPoints.ToString() + " " + "PT";

        //Per modificare il valore del testo relativo alle monete raccolte
        coinsText.GetComponent<TMP_Text>().text = currentCoins.ToString() + " " + "$";

        

        if (GameObject.FindWithTag("PlayerWeapon") != null)
        {
            weapon = GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponPlayerController>();
            //Per modificare il valore delle munizioni
            ammoText.GetComponent<TMP_Text>().text = weapon.features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue.ToString() + "/"
                + weapon.features[WeaponFeatures.WeaponFeature.FeatureType.FT_MAX_AMMO_COUNT].currentValue.ToString();
        }
    }

    //Per il reset dei parametri quando si riavvia il livello
    public void ParametersReset()
    {
        currentNumberOfEnemies = valid_currentNumberOfEnemies;
        NumberOfEnemiesCheck = valid_currentNumberOfEnemies;
        enemiesNumberText.GetComponent<TMP_Text>().text = new string('*', valid_currentNumberOfEnemies);
        levelPoints = valid_levelPoints;
        currentCoins = valid_currentCoins;
        levelTimeCounter = valid_levelTimeCounter;
        comboTimeCounter = 0;
        comboMultiplier = 0;
        currentFailure = 0;


        //Per il reset dei gameItems
        sc.skulls_amount = valid_skulls_amount;
        sc.helms_amount = valid_helms_amount;
        sc.telescopes_amount = valid_telescopes_amount;
        UpdateGameItemsAmountText();
    }

    //Per il reset dei parametri quando si completa il livello
    public void LevelCompletedParametersReset()
    {
        comboTimeCounter = 0;
        comboMultiplier = 0;
        valid_levelPoints = levelPoints;
        valid_currentCoins = currentCoins;
        valid_levelTimeCounter = levelTimeCounter;

        //Per il reset dei valori validi relativi ai gameItems
        valid_skulls_amount = sc.skulls_amount;
        valid_helms_amount = sc.helms_amount;
        valid_telescopes_amount = sc.telescopes_amount;
        UpdateGameItemsAmountText();
    }

    //Per gestire l'update del totale dei gameItem posseduti
    public void UpdateGameItemsAmountText()
    {
        skulls_amount_text.text = sc.skulls_amount.ToString();
        helms_amount_text.text = sc.helms_amount.ToString();
        telescopes_amount_text.text = sc.telescopes_amount.ToString();
    }


    public void handleBreakdown(PlayerController pc, bool isLevelCompleted)

    {
        //Applico la prima avaria
        //Se:
        //1) sono passati 30 secondi
        //2) non è completato il Livello
        //3) non contiene già un modicatore con questa chiave

        Debug.Log("CURRENT AVARY NUMBER : " + currentFailure.ToString());

        if (currentFailure == 0 && ((levelTimeCounter + valid_levelTimeCounter) < 5) && !isLevelCompleted)
        {
            breakdownCanvas.SetActive(false);
        }


            //sara push to data
            if (currentFailure == 0 && ((levelTimeCounter + valid_levelTimeCounter) >= 5) && !isLevelCompleted)
        {

            
               if(pc.getModifierbyID("001") == null)
               {
                    Debug.Log("APPLICO LA PRIMA FAILURE");

                    currentFailure = 1;
                    breakdownCanvas.SetActive(true);
                    firstBreakdownImage.enabled = true;
                    pc.modifiers.Add(modifiersjson.getModifierbyID("001"));
                    radioController.SetRadioText(events.getEventbyName("primaAvaria").message);
                }
                else
                {
                
                Debug.Log("LA PRIMA FAILURE è GIA IN CORSO ASPETTIAMO LA SECONDA ");

                }


        }
      
        else if (currentFailure == 1 && ((levelTimeCounter + valid_levelTimeCounter) >= 35) && !isLevelCompleted)
        {
            // seconda 
            if (pc.getModifierbyID("secondafailure") == null)
            {
                currentFailure = 2;
                //firstBreakdownImage.enabled = false;
                secondBreakdownImage.enabled = true;

                pc.modifiers.Add(modifiersjson.getModifierbyID("002"));
                radioController.SetRadioText(events.getEventbyName("secondaAvaria").message);
            }

        }

         else if (currentFailure == 2 && ((levelTimeCounter + valid_levelTimeCounter) >= 60) && !isLevelCompleted)
         {
            if (pc.getModifierbyID("terzafailure") == null)
            {
                currentFailure = 3;

                //firstBreakdownImage.enabled = false;
                //secondBreakdownImage.enabled = false;
                thirdBreakdownImage.enabled = true;

                pc.modifiers.Add(modifiersjson.getModifierbyID("003"));
                radioController.SetRadioText(events.getEventbyName("terzaAvaria").message);
            }
        }
         







    }
}
