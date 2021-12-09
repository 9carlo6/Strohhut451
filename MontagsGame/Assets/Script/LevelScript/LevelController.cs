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

    //Singleton
    public static LevelController lcstatic;

    //Per gestire i modificatori e gli eventi
    [HideInInspector] public ModifierJsonMap modifiersjson;
    [HideInInspector] public EventJsonMap events;
    [HideInInspector] public int currentFailure;

    //Per gesitre gli aiuti
    [HideInInspector] public int current_restart_number;

    // Start is called before the first frame update
    void Awake()
    {
        int currentSceneId = SceneManager.GetActiveScene().buildIndex;

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

            //Per gestire i modificatori e gli eventi
            string fileStringevents = new StreamReader("Assets/Push-To-Data/Modifiers/Events.txt").ReadToEnd();
            events = JsonUtility.FromJson<EventJsonMap>(fileStringevents);
            string fileStringmodifiers = new StreamReader("Assets/Push-To-Data/Modifiers/Modifiers.txt").ReadToEnd();
            modifiersjson = JsonUtility.FromJson<ModifierJsonMap>(fileStringmodifiers);
            currentFailure = 0;

            //Per gesitre gli aiuti
            current_restart_number = sc.GetCurrentRestartNumber();
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

    void Update()
    {
        //Continua costantemente la tipologia della scena corrente
        //Se non è un livello distrugge l'oggetto LevelController
        CheckSceneType();

        /*
        //Per gestire gli aiuti
        if(current_restart_number < sc.GetCurrentRestartNumber())
        {
            current_restart_number = sc.GetCurrentRestartNumber();
            LevelHelper();
        }
        */

        //Per incrementare il valore del cronometro del tempo impiegato per superare il livello
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
        //Per gestire il caricamento
        else if(currentNumberOfEnemies < NumberOfEnemiesCheck)
        {
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
 
        //Per modificare il valore delle munizioni
        if (GameObject.FindWithTag("PlayerWeapon") != null)
        {
            weapon = GameObject.FindWithTag("PlayerWeapon").GetComponent<WeaponPlayerController>();
            ammoText.GetComponent<TMP_Text>().text = weapon.features[WeaponFeatures.WeaponFeature.FeatureType.FT_AMMO_COUNT].currentValue.ToString() + "/"
                + weapon.features[WeaponFeatures.WeaponFeature.FeatureType.FT_MAX_AMMO_COUNT].currentValue.ToString();
        }
    }

    //Per gesitre gli aiuti in caso di troppe morti
    public void LevelHelper()
    {
        //Se il numero di tentativi è uguale a 5 bisogna eliminare le trappole e informare il giocatore con un messaggio
        if (current_restart_number == 5)
        {
            radioController.SetRadioText(events.getEventbyName("HelpBypassTrappole").message);
        }
        //Se il numero di tentativi è maggiore di 5 bisogna eliminare le trappole a ogni morte
        if (current_restart_number >= 1)
        {
            foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap"))
            {
                trap.SetActive(false);
            }
        }

        //Se il numero di tentativi è uguale a 7 bisogna consigliare al giocatore di utilizzare al meglio i gameItems
        if (current_restart_number == 7)
        {
            radioController.SetRadioText(events.getEventbyName("HelpUseItems").message);
        }
        //Se il numero di tentativi è maggiore di 7 tutti i gameItems vengono incrementati di uno a ogni morte
        if (current_restart_number >= 7)
        {
            sc.skulls_amount++;
            sc.helms_amount++;
            sc.telescopes_amount++;
            UpdateGameItemsAmountText();
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
        levelTimeCounter = 0;
        comboTimeCounter = 0;
        comboMultiplier = 0;
        currentFailure = 0;

        //Per il reset dei gameItems
        sc.skulls_amount = valid_skulls_amount;
        sc.helms_amount = valid_helms_amount;
        sc.telescopes_amount = valid_telescopes_amount;
        UpdateGameItemsAmountText();

        //Per il reset dei canvas delle avarie
        firstBreakdownImage.enabled=false;
        secondBreakdownImage.enabled=false;
        thirdBreakdownImage.enabled= false;
        breakdownCanvas.SetActive(false);
    }

    //Per il reset dei parametri quando si completa il livello
    public void LevelCompletedParametersReset()
    {
        comboTimeCounter = 0;
        comboMultiplier = 0;
        valid_levelPoints = levelPoints;
        valid_currentCoins = currentCoins;
        valid_levelTimeCounter += levelTimeCounter;
        levelTimeCounter = 0;

        //Per il reset dei canvas delle avarie
        firstBreakdownImage.enabled = false;
        secondBreakdownImage.enabled = false;
        thirdBreakdownImage.enabled = false;
        breakdownCanvas.SetActive(false);

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

    //Per la gestione delle avarie
    public void handleBreakdown(PlayerController pc, bool isLevelCompleted)
    {
        //Applico la prima avaria
        //Se:
        //1) sono passati 30 secondi
        //2) non è completato il Livello
        //3) non contiene già un modicatore con questa chiave
        if (currentFailure == 0 && ((levelTimeCounter) < 20) && !isLevelCompleted)
        {
            breakdownCanvas.SetActive(false);
        }

        if (currentFailure == 0 && ((levelTimeCounter) >= 20) && !isLevelCompleted)
        {
            if (pc.getModifierbyID("001") == null)
            {
                currentFailure = 1;
                breakdownCanvas.SetActive(true);
                firstBreakdownImage.enabled = true;
                pc.addMoreModifiers(modifiersjson.getMoreModifiersbyCIDs(events.getEventbyName("primaAvaria").modificatoriCIDS));

                radioController.SetRadioText(events.getEventbyName("primaAvaria").message);
            }
        }
      
        else if (currentFailure == 1 && ((levelTimeCounter) >= 40) && !isLevelCompleted)
        {
            // seconda 
            if (pc.getModifierbyID("secondafailure") == null)
            {
                currentFailure = 2;
                secondBreakdownImage.enabled = true;

                pc.weaponController.addMoreModifiers(modifiersjson.getMoreModifiersbyCIDs(events.getEventbyName("secondaAvaria").modificatoriCIDS));
                radioController.SetRadioText(events.getEventbyName("secondaAvaria").message);
            }
        }

         else if (currentFailure == 2 && ((levelTimeCounter + valid_levelTimeCounter) >= 60) && !isLevelCompleted)
         {
            if (pc.getModifierbyID("terzafailure") == null)
            {
                currentFailure = 3;
                thirdBreakdownImage.enabled = true;

                pc.weaponController.addMoreModifiers(modifiersjson.getMoreModifiersbyCIDs(events.getEventbyName("terzaAvaria").modificatoriCIDS));
                radioController.SetRadioText(events.getEventbyName("terzaAvaria").message);
            }
        }
    }
}