using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 2f;


    public AudioMixer audioMixer;


    [HideInInspector] public float timeCounterMovement;
    [HideInInspector] public float timeCounterRotation;
    [HideInInspector] public float defaultTimeCounterMovement = 2;
    [HideInInspector] public float defaultTimeCounterRotation = 1;
    [HideInInspector] public bool leftDirection;
    [HideInInspector] public bool forwardDirection;

    //Per gestire l'animazione della transizione tra un livello e un altro
    public Animator transition;
    public float transitionTime = 1f;

    public GameObject sessionController;
    public SessionController sc;


    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }


    //Funzione per avviare una nuova sessione
    //Questa viene richiamata quando viene premuto il pulsante "start game" o il pulsante dedicato al capitolo
    //In futuro quando viene premuto il pulsante "start game" si deve fare un controllo
    //sull'ultimo livello completato in maniera tale da partire dal successivo
    public void AddNewSession(int chapter)
    {
        sessionController = GameObject.FindWithTag("SessionController");
        sc = sessionController.GetComponent<SessionController>();
        sc.AddNewSession(1);
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

}
