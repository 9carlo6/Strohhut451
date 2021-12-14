using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SessionController : MonoBehaviour
{
    public TextAsset textJSON;
    public Sessions sessions;
    public int lastSessionId;
    public string new_scene_name;

    //Per gestire le monete
    public TextAsset coinTextJSON;
    public Coins coins;

    //Per gestire gli item
    public TextAsset gameItemsTextJSON;
    //Teschio - Skull - Muore un nemico random
    //Telescopio - Telescope - Amplia campo visivo
    //Pirate - Helm - Stunna nemici
    public GameItems gameItems;
    public float skulls_amount;
    public float helms_amount;
    public float telescopes_amount;

    //Singleton
    public static SessionController scstatic;
   
    void Awake()
    {
        //Singleton
        if (scstatic == null)
        {
            scstatic = this;
            DontDestroyOnLoad(gameObject);

            sessions = new Sessions();
            //Leggiamo il json contenente le info di sessione
            sessions = JsonUtility.FromJson<Sessions>(textJSON.text);

            //Ricaviamo l'ultimo session id
            Debug.Log("SessionController: last session id-" + GetLastSessionId());

            //Ricaviamo il nome della scena corrente
            new_scene_name = SceneManager.GetActiveScene().name.ToString();

            //Per ricavare il numero di oggetti posseduti
            gameItems = JsonUtility.FromJson<GameItems>(gameItemsTextJSON.text);

            //Per ricavare il numero di coins
            coins = JsonUtility.FromJson<Coins>(coinTextJSON.text);
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    //Funzione per ricavare l'ultimo id di sessione
    public int GetLastSessionId()
    {
        lastSessionId = sessions.sessions_list.LastOrDefault().session_id;
        return lastSessionId;
    }

    //Funzione per ricavare i dati dell'ultima sessione
    public Dictionary<string, int> GetLastDataSession()
    {
        Dictionary<string, int> data = new Dictionary<string, int>();

        Session session = new Session();
        session = sessions.sessions_list.LastOrDefault();

        Scene scene = new Scene();
        scene = sessions.sessions_list.LastOrDefault().scenes.LastOrDefault();

        data.Add("chapter", session.chapter);
        data.Add("time", (int) scene.time);
        //data.Add("attempts", scene.restart_numbers);
        //data.Add("points", scene.score);
        if(scene.coins > 0)
        {
            data.Add("coins", scene.coins);
        }
        else
        {
            data.Add("coins", 1);
        }

        int attempts = 0;
        int points = 0;
        foreach (Scene s in sessions.sessions_list.LastOrDefault().scenes)
        {
            attempts += s.restart_numbers;
            points += s.score;
        }
        data.Add("attempts", attempts);
        data.Add("points", points);

        return data;
    }

    //Funzione per aggiornare il punteggio finale e il nome del giocatore della sessione quando essa termina
    public void EndSession(float final_score, string player_name)
    {
        sessions.sessions_list.LastOrDefault().final_score = final_score;
        sessions.sessions_list.LastOrDefault().is_completed = true;
        sessions.sessions_list.LastOrDefault().player_name = player_name;

        UpdateJson();

        //Gestione aggiornamento monete
        UpdateCoinsJson();

        //Per la gestione degli oggetti
        UpdateGameItemsJson();
    }

    //Funzione per aggiungere una nuova sessione
    public void AddNewSession(int chapter)
    {
        //Creiamo una nuova sessione e gli diamo come id il successivo dell'ultimo registrato nelle sessioni
        Session new_session = new Session();
        new_session.session_id = GetLastSessionId() + 1;
        new_session.chapter = chapter;
        new_session.final_score = 0;
        new_session.is_completed = false;
        new_session.player_name = "Samantha";

        Debug.Log("NEW SESSION ID: " + new_session.session_id);

        //Aggiungiamo la sessione alla lista delle sessioni e aggiornamo il json
        sessions.sessions_list.Add(new_session);
        UpdateJson();

        //Per la gestione degli oggetti
        if (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "skull").amount > 0)
        {
            skulls_amount = 1;
        }
        if (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "helm").amount > 0)
        {
            helms_amount = 1;
        }
        if (gameItems.gameItems_list.FirstOrDefault(gi => gi.name == "telescope").amount > 0)
        {
            telescopes_amount = 1;
        }
    }

    //Funzione per aggiungere una nuova scena
    //Questa viene richiamata ogni volta che il livello viene INIZIATO
    public void AddScene()
    {
        //Se la scena corrente non è gia presente nell'ultima sessione allora bisogna aggiungerla 
        if (!CheckSceneAlreadyExists())
        {
            //Creiamo una nuova scena e gli assegnamo il nome della scena corrente
            Scene new_scene = new Scene();
            new_scene.name = SceneManager.GetActiveScene().name.ToString();

            if(sessions.sessions_list.LastOrDefault().scenes == null)
            {
                sessions.sessions_list.LastOrDefault().scenes = new List<Scene>();
            }

            //Aggiungiamo la scena alla lista delle scene della sessione corrente e aggiornamo il json
            sessions.sessions_list.LastOrDefault().scenes.Add(new_scene);
            UpdateJson();

            Debug.Log("LA SCENA CORRENTE NON E' GIA STATA AGGIUNTA IN QUESTA SESSIONE - S_ID:" + lastSessionId);
        }
        else
        {
            Debug.Log("LA SCENA CORRENTE E' GIA STATA AGGIUNTA IN QUESTA SESSIONE - S_ID:" + lastSessionId);
        }
    }

    //Funzione per aggiornare i dati della scena corrente
    //Questa viene richiamata ogni volta che il livello corrente o viene RICOMINCIATO o viene COMPLETATO o quando si ESCE per ritornare al menu principale.
    public void UpdateCurrentSceneData(string scene_name, int scene_restart_numbers, int scene_coins, int scene_score, float scene_time, bool scene_is_completed)
    {
        AddScene();

        int current_scene_index = GetCurrentSceneIndex();

        //Bisogna controllare se la scena corrente è gia presente nell'ultima sessione alrtimenti c'è un errore
        if (CheckSceneAlreadyExists() && current_scene_index != -1)
        {
            //Aggiorniamo i valori della scena corrente e aggiornamo il json
            sessions.sessions_list.LastOrDefault().scenes[current_scene_index].restart_numbers += scene_restart_numbers;
            sessions.sessions_list.LastOrDefault().scenes[current_scene_index].coins = scene_coins;
            sessions.sessions_list.LastOrDefault().scenes[current_scene_index].score = scene_score;
            sessions.sessions_list.LastOrDefault().scenes[current_scene_index].time += scene_time;
            sessions.sessions_list.LastOrDefault().scenes[current_scene_index].is_completed = scene_is_completed;
            UpdateJson();
        }
        else
        {
            Debug.Log("ERRORE: UPDATE DEI DATI DELLA SCENA CORRENTE IMPOSSIBILE IN QUANTO NON ESISTE IN QUESTA SESSIONE - S_ID:" + lastSessionId);
        }
    }

    //Funzione necessaria per sovrascrivere il file json contenente le sessioni
    public void UpdateJson()
    {
        string json = JsonUtility.ToJson(sessions);
        File.WriteAllText("Assets/Push-To-Data/Sessions.txt", json);
        //await Task.Yield();
        Debug.Log("OK: UPDATE DEI DATI DELLA SCENA CORRENTE IN QUESTA SESSIONE - S_ID:" + lastSessionId);
    }

    //Funzione necessaria per sovrascrivere il file json contenente i dati sulle monete
    public void UpdateCoinsJson()
    {
        float normal_coins = sessions.sessions_list.LastOrDefault().scenes.LastOrDefault().coins;
        //Per il momento non possono essere presi i roger coins durante i livelli
        float roger_coins = 0;

        coins.normal_coins = coins.normal_coins + normal_coins;
        coins.roger_coins = coins.roger_coins + roger_coins;

        string coins_json = JsonUtility.ToJson(coins);
        File.WriteAllText("Assets/Push-To-Data/Coins.txt", coins_json);
    }

    //Funzione necessaria per sovrascrivere il file json contenente i dati sugli oggetti
    public void UpdateGameItemsJson()
    {
        //In questa maniera recupera gli oggetti non sprecati
        //Meno uno in quanto prima era stato preso e non sottratto
        foreach(GameItem gi in gameItems.gameItems_list)
        {
            if (gi.name == "skull") if(gi.amount + skulls_amount > 0) gi.amount += skulls_amount - 1;
            if (gi.name == "helm") if (gi.amount + helms_amount > 0) gi.amount += helms_amount - 1;
            if (gi.name == "telescope") if (gi.amount + telescopes_amount > 0) gi.amount += telescopes_amount - 1;
        }

        string gameItems_json = JsonUtility.ToJson(gameItems);
        File.WriteAllText("Assets/Push-To-Data/GameItems.txt", gameItems_json);
    }

    //Funzione per controllare se la scena è gia presente nella sessione corrente
    public bool CheckSceneAlreadyExists()
    {
        if (sessions.sessions_list.LastOrDefault().scenes == null) return false;

        foreach(Scene scene in sessions.sessions_list.LastOrDefault().scenes)
        {
            if(scene.name == SceneManager.GetActiveScene().name.ToString())
            {
                return true;
            }
        }
        return false;
    }

    //Funzione che restituisce l'indice della scena corrente se la scena è gia presente nell'ultima sessione
    public int GetCurrentSceneIndex()
    {
        int index = 0;

        foreach (Scene scene in sessions.sessions_list.LastOrDefault().scenes)
        {
            if (scene.name == SceneManager.GetActiveScene().name.ToString())
            {
                return index;
            }
            else
            {
                index++;
            }

        }
        //se la scena non è presente nella lista restituisce -1
        return -1;
    }

    //Funzione che restituisce il numero di tentativi dell'ultima sessione
    public int GetCurrentRestartNumber()
    {
        if (sessions.sessions_list.LastOrDefault().scenes != null)
            return sessions.sessions_list.LastOrDefault().scenes.LastOrDefault().restart_numbers;
        return 0;
    }


    //Funzione per la creazione del json iniziale (DA CANCELLARE)
    public void CreateInitialSessionLog()
    {
        Scene scene_1 = new Scene();
        scene_1.name = "level_1_1";
        scene_1.restart_numbers = 15;
        scene_1.coins = 5;
        scene_1.score = 5000;
        scene_1.time = 10.0f;

        Scene scene_2 = new Scene();
        scene_2.name = "level_1_2";
        scene_2.restart_numbers = 15;
        scene_2.coins = 5;
        scene_2.score = 5000;
        scene_2.time = 10.0f;

        List<Scene> scenes = new List<Scene>();
        scenes.Add(scene_1);
        scenes.Add(scene_2);

        Session session_1 = new Session();
        session_1.session_id = 1;
        session_1.scenes = scenes;

        List<Session> sessions_list = new List<Session>();
        sessions_list.Add(session_1);

        Sessions sessions = new Sessions();
        sessions.sessions_list = sessions_list;

        string json = JsonUtility.ToJson(sessions);

        Debug.Log("json: " + json);

        File.WriteAllText("Assets/Push-To-Data/Sessions.txt", json);
    }
}
