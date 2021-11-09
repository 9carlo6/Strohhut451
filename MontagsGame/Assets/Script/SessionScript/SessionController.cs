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

    public static SessionController scstatic;
   
    void Awake()
    {
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

    //Funzione per aggiungere una nuova sessione
    public void AddNewSession()
    {
        //Creiamo una nuova sessione e gli diamo come id il successivo dell'ultimo registrato nelle sessioni
        Session new_session = new Session();
        new_session.session_id = GetLastSessionId() + 1;

        Debug.Log("NEW SESSION ID: " + new_session.session_id);

        //Aggiungiamo la sessione alla lista delle sessioni e aggiornamo il json
        sessions.sessions_list.Add(new_session);
        UpdateJson();
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
