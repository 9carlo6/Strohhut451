using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardsMenu : MonoBehaviour
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

    //Per gestire i primi 4 punteggi
    public TMP_Text first_place_front;
    public TMP_Text first_place_back;
    public TMP_Text second_place_front;
    public TMP_Text second_place_back;
    public TMP_Text third_place_front;
    public TMP_Text third_place_back;
    public TMP_Text fourth_place_front;
    public TMP_Text fourth_place_back;
    public List<Score> scores = new List<Score>();

    void Awake()
    {
        sessionController = GameObject.FindWithTag("SessionController");
        sc = sessionController.GetComponent<SessionController>();
    }

    void Start()
    {
        timeCounterMovement = defaultTimeCounterMovement;
        timeCounterRotation = defaultTimeCounterRotation;
        leftDirection = true;

        LoadScores();
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

    public void LoadScores()
    {
        foreach(Session s in sc.sessions.sessions_list)
        {
            if(s.final_score != null)
            {
                scores.Add(new Score(s.player_name, s.final_score));
            }
        }

        int i = 4;


        List<Score> orderedScores = scores.OrderByDescending(item => item.vote).ToList();


        //first_place_front.text = scores["CARLO"].ToString();
        //first_place_back.text = scores["CARLO"].ToString();

        
        if (orderedScores[0] != null)
        {
            first_place_front.text = "1 : " + orderedScores[0].player_name + " - " + orderedScores[0].vote.ToString();
            first_place_back.text = "1 : " + orderedScores[0].player_name + " - " + orderedScores[0].vote.ToString();
        }
        else
        {
            first_place_front.text = "1 - NO SCORE";
            first_place_back.text = "1 - NO SCORE";
        }

        if (orderedScores[1] != null)
        {
            second_place_front.text = "2 : " + orderedScores[1].player_name + " - " + orderedScores[1].vote.ToString();
            second_place_back.text = "2 : " + orderedScores[1].player_name + " - " + orderedScores[1].vote.ToString();
        }
        else
        {
            second_place_front.text = "2 - NO SCORE";
            second_place_back.text = "2 - NO SCORE";
        }

        if (orderedScores[2] != null)
        {
            third_place_front.text = "3 : " + orderedScores[2].player_name + " - " + orderedScores[2].vote.ToString();
            third_place_back.text = "3 : " + orderedScores[2].player_name + " - " + orderedScores[2].vote.ToString();
        }
        else
        {
            third_place_front.text = "3 - NO SCORE";
            third_place_back.text = "3 - NO SCORE";
        }

        if (orderedScores[3] != null)
        {
            fourth_place_front.text = "4 : " + orderedScores[3].player_name + " - " + orderedScores[3].vote.ToString();
            fourth_place_back.text = "4 : " + orderedScores[3].player_name + " - " + orderedScores[3].vote.ToString();
        }
        else
        {
            fourth_place_front.text = "4 - NO SCORE";
            fourth_place_back.text = "4 - NO SCORE";
        }


    }

}