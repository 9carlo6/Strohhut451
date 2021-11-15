using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Linq;
using System.Text;

public class InputPlayerNameMenu : MonoBehaviour
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
    public GameObject name;
    [HideInInspector] public TMP_Text total;
    [HideInInspector] public float scoreTimeCounter = 1.5f;

    //Per gestire il voto finale
    [HideInInspector] public float total_score;


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

        total_score = ((finalScore["points"] * finalScore["coins"]) / (finalScore["time"] * finalScore["attempts"])) * 100;
        total.text = total_score.ToString();
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


    //Funzione per caricare il punteggio e tornare al menu iniziale
    public void SaveScore()
    {
        sc.EndSession(total_score, name.GetComponent<TMP_InputField>().text);
        LoadMenu();
    }
}


