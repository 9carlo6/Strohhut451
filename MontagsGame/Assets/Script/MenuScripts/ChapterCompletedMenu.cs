using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Linq;
using System.Text;

public class ChapterCompletedMenu : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 2f;

    [HideInInspector] public float timeCounterMovement;
    [HideInInspector] public float timeCounterRotation;
    [HideInInspector] public float defaultTimeCounterMovement = 2;
    [HideInInspector] public float defaultTimeCounterRotation = 1;
    [HideInInspector] public bool leftDirection;
    [HideInInspector] public bool forwardDirection;

    //Per gestire l'animazione della transizione tra un livello e un altro
    public Animator transition;
    public float transitionTime = 1f;

    //Per gestire il sessionController
    public GameObject sessionController;
    public SessionController sc;

    //Per gestire il punteggio finale
    public Dictionary<string, int> finalScore;
    public TMP_Text time;
    public TMP_Text attempts;
    public TMP_Text points;
    public TMP_Text coins;
    [HideInInspector] public float scoreTimeCounter = 1.5f;

    //Per gestire il voto finale
    public ChapterScores chapterScores;
    public TextAsset scoreJSON;
    public List<string> votes = new List<string>();


    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    //Per gestire l'animazione della transizione tra un livello e un altro
    IEnumerator LoadLevel(int levelIndex)
    {
        this.transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Uscita dal Gioco");
        Application.Quit();
    }

    void Awake()
    {
        sessionController = GameObject.FindWithTag("SessionController");
        sc = sessionController.GetComponent<SessionController>();

        //Per recuperare il punteggio finale
        finalScore = sc.GetLastDataSession();

        //Per gestire il voto finale
        chapterScores = new ChapterScores();
        //Leggiamo il json contenente le info di sessione
        chapterScores = JsonUtility.FromJson<ChapterScores>(scoreJSON.text);


        handleVoteLoading();
    }


    void Start()
    {
        timeCounterMovement = defaultTimeCounterMovement;
        timeCounterRotation = defaultTimeCounterRotation;
        leftDirection = true;
    }

    void Update()
    {
        handleLateralMovement();
        handleOndulatoryMovement();
        handleScoreLoading();
    }

    void handleLateralMovement()
    {
        if (timeCounterMovement > 0)
        {
            timeCounterMovement -= Time.deltaTime;

            if (leftDirection)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            leftDirection = !leftDirection;

            //Deve essere il doppio in quanto partendo dal centro impieghera un tempo paria defaultTimeCounter per arrivare a sinistra
            //e successivamente un tempo pari a defaultTimeCounter * 2 per arrivare a destra (poi sarà sempre cosi)
            timeCounterMovement = defaultTimeCounterMovement * 2;
        }
    }

    void handleOndulatoryMovement()
    {
        if (timeCounterRotation > 0)
        {
            timeCounterRotation -= Time.deltaTime;

            if (forwardDirection)
            {
                transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.back * turnSpeed * Time.deltaTime);
            }
        }
        else
        {
            forwardDirection = !forwardDirection;
            timeCounterRotation = defaultTimeCounterRotation * 2;
        }
    }

    //Funzione per gestire il caricamento del punteggio
    public void handleScoreLoading()
    {
        if (scoreTimeCounter > 0)
        {
            scoreTimeCounter -= Time.deltaTime;

            //Cambia casualmente il valore dei numeri
            time.text = Random.Range(0, 100000).ToString();
            attempts.text = Random.Range(0, 100000).ToString();
            points.text = Random.Range(0, 100000).ToString();
            coins.text = Random.Range(0, 100000).ToString();
        }
        else
        {
            time.text = finalScore["time"].ToString();
            attempts.text = finalScore["attempts"].ToString();
            points.text = finalScore["points"].ToString();
            coins.text = finalScore["coins"].ToString();
        }
    }

    //Funzione per gestire il calcolo del voto finale e del suo caricamento
    public void handleVoteLoading()
    {
        Scores scores = new Scores();
        scores = chapterScores.chapter_scores_list[finalScore["chapter"]-1];


        //TIME SCORE
        if (finalScore["time"] < scores.scores_list[0].time)
        {
            Debug.Log("TIME SCORE = A");
            votes.Add("A");
        }
        else if (finalScore["time"] < scores.scores_list[1].time)
        {
            Debug.Log("TIME SCORE = B");
            votes.Add("B");
        }
        else if (finalScore["time"] < scores.scores_list[2].time)
        {
            Debug.Log("TIME SCORE = C");
            votes.Add("C");
        }
        else
        {
            Debug.Log("TIME SCORE = D");
            votes.Add("D");
        }

        //ATTEMPTS SCORE
        if (finalScore["attempts"] < scores.scores_list[0].attempts)
        {
            Debug.Log("ATTEMPTS SCORE = A");
            votes.Add("A");
        }
        else if (finalScore["attempts"] < scores.scores_list[1].attempts)
        {
            Debug.Log("ATTEMPTS SCORE = B");
            votes.Add("B");
        }
        else if (finalScore["attempts"] < scores.scores_list[2].attempts)
        {
            Debug.Log("ATTEMPTS SCORE = C");
            votes.Add("C");
        }
        else
        {
            Debug.Log("ATTEMPTS SCORE = D");
            votes.Add("D");
        }

        //POINTS SCORE
        if (finalScore["points"] > scores.scores_list[0].points)
        {
            Debug.Log("POINTS SCORE = A");
            votes.Add("A");
        }
        else if (finalScore["points"] > scores.scores_list[1].points)
        {
            Debug.Log("POINTS SCORE = B");
            votes.Add("B");
        }
        else if (finalScore["points"] > scores.scores_list[2].points)
        {
            Debug.Log("POINTS SCORE = C");
            votes.Add("C");
        }
        else
        {
            Debug.Log("POINTS SCORE = D");
            votes.Add("D");
        }

        //COINS SCORE
        if (finalScore["coins"] > scores.scores_list[0].coins)
        {
            Debug.Log("COINS SCORE = A");
            votes.Add("A");
        }
        else if (finalScore["coins"] > scores.scores_list[1].coins)
        {
            Debug.Log("COINS SCORE = B");
            votes.Add("B");
        }
        else if (finalScore["coins"] > scores.scores_list[2].coins)
        {
            Debug.Log("COINS SCORE = C");
            votes.Add("C");
        }
        else
        {
            Debug.Log("COINS SCORE = D");
            votes.Add("D");
        }
    }
}

