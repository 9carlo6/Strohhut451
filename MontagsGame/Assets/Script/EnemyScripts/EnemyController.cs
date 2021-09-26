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
    Vector3 currentMovementDirection;
    bool isMoving;

    //queste due variabili servono per modificare l'animazione in base alla direzione in cui si muove il nemico
    float velocityXEnemy = 0.0f;
    float velocityZEnemy = 0.0f;

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

        velocityZEnemy = Vector3.Dot(currentMovementDirection, transform.forward);
        velocityXEnemy = Vector3.Dot(currentMovementDirection, transform.right);

        animator.SetFloat("VelocityZEnemy", velocityZEnemy, 5f, Time.deltaTime);
        animator.SetFloat("VelocityXEnemy", velocityXEnemy, 5f, Time.deltaTime);

    }


    // Update is called once per frame
    void Update()
    {
        if(transform.position != currentPosition)
        {
            isMoving = true;
            currentMovementDirection = (currentPosition - transform.position).normalized;
        }
        else
        {
            isMoving = false;
        }

        handleAnimation();

    }
}
