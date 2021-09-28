using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Questa classe serve per la gestione del'animazione del nemico
public class EnemyController : MonoBehaviour
{

    private Rigidbody enemyRigidbody;

    //per l'animazione
    Animator animator;

    //variabili utilizzare per salvare informazioni relative al movimento del nemico
    Vector3 currentPosition;
    bool isMoving;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        currentPosition = transform.position;
    }

    //Per gestire le animazioni
    void handleAnimation()
    {
        //Prende i parametri dall'animator
        bool isWalkingEnemy = animator.GetBool("isWalkingEnemy");

        if (isMoving && !isWalkingEnemy)
        {
            animator.SetBool("isWalkingEnemy", true);
        }
        else if (!isMoving && isWalkingEnemy)
        {
            animator.SetBool("isWalkingEnemy", false);
        }


    }


    // Update is called once per frame
    void Update()
    {
        if(transform.position != currentPosition)
        {
            isMoving = true;
            currentPosition = transform.position;
        }
        else
        {
            isMoving = false;
        }

        handleAnimation();

    }
}
