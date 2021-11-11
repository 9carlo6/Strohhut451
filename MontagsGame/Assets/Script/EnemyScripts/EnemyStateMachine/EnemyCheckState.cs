using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckState : EnemyBaseState
{

    //FOV, Chase player
    public float viewRadius;  //raggio di vista
    [Range(0, 360)]           //limitiamo l'angolo di visuale a 360�
    public float viewAngle;   //angolo di vista

    //Dobbiamo fare in modo da inseguire il personaggio e tenere conto degli ostacoli
    //LayerMask permette di specificare i layer da utilizzare in Physics.Raycast
    public LayerMask targetMask;    //bersaglio, cioè il player
    public LayerMask obstructionMask; //ostacoli, ad esempio le pareti

    
    GameObject enemyGameObject;

    public Transform[] wayPoints;   //Array di points verso cui il nemico dovrà effettuare il patroling



    //per l'animazione
    public Animator enemyAnimator;


    public bool playerInSightRange;  //quando vedo il bersaglio = true

    EnemyHealthManager enemyHealthManager;


    public float checkTime = 0;

    //ALERT
    public GameObject playerGameObject;
    public bool playerFire;  //Viene utilizzata per l'alert
    public bool fireInHearRange;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Stato nemico = CHECK");

        checkTime = 0;

        viewRadius = 10;
        viewAngle = 110;

        enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();
        enemyGameObject = enemy.GetComponent<EnemyController>().gameObject;
        targetMask = enemy.GetComponent<EnemyController>().targetMask;
        obstructionMask = enemy.GetComponent<EnemyController>().obstructionMask;
        wayPoints = enemy.GetComponent<EnemyController>().wayPoints;


        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
        }
    }


    public override void UpdateState(EnemyStateManager enemy)
    {

        if (playerGameObject != null)
        {
            playerFire = playerGameObject.GetComponent<PlayerController>().getBoolToAlert();

            if (playerFire)
            {
                if (Vector3.Distance(enemyGameObject.transform.position, playerGameObject.transform.position) <= 12f)

                    fireInHearRange = true;
                else
                {
                    fireInHearRange = false;
                }
            }

        }
        if (enemyHealthManager.currentHealth <= 0)
        {
            enemy.SwitchState(enemy.DeathState);
        }

        FieldOfViewCheck();

        if (playerInSightRange || fireInHearRange)
        {
            enemy.SwitchState(enemy.ChasePlayerState);
        }

        else if (checkTime > 3.5f)
        {
            if (wayPoints.Length > 1)
            {
                enemy.SwitchState(enemy.PatrollingState);
            }
        }
        else 
        {
            checkTime += Time.deltaTime;
        }
    }

    
    //FOV
    private void FieldOfViewCheck()
    {
        //Inizializziamo un'array con tutti i collider che toccano o sono dentro la sfera con i parametri passati
        //Centro della sfera, raggio, layer di collider da includere nella query
        Collider[] rangeChecks = Physics.OverlapSphere(enemyGameObject.transform.position, viewRadius, targetMask);

        //Se qualcosa collide andiamo in questo if, quindi la lunghezza dell'array sar� diversa da zero
        //Qui basta un semplice if perch� sul layer targetMask c'� solo il player, altrimenti avremmo dovuto fare un for per scorrere l'array
        if (rangeChecks.Length != 0)
        {
            //targetTransform sarà pari alla prima istanza di rangeChecks, cioè la trasform del player
            Transform targetTransform = rangeChecks[0].transform;

            //Definiamo la direzione verso cui il nostro nemico sta guardando
            //Differenza tra la posizione del player (prima istanza del Collider[]) e il nemico
            Vector3 directionToTarget = (targetTransform.position - enemyGameObject.transform.position).normalized; //normalizzato tra 0 e 1

            //Transform.forward ritorna un vettore normalizzato rappresentante l'asse z
            //Quindi verifichiamo se l'angolo tra questi due vettori � minore dell'angolo di visuale fratto 2
            if (Vector3.Angle(enemyGameObject.transform.forward, directionToTarget) < viewAngle / 2)
            {
                //Distanza tra la posizione del nemico e quella del player
                float distanceToTarget = Vector3.Distance(enemyGameObject.transform.position, targetTransform.position);

                //Col RayCast è come se dotassimo il nemico di un occhio, avviene il lancio di un raggio
                //Parametri: origine del raggio, direzione, distanza massima che il raggio deve controllare per le collisioni
                //Maschera di livello utilizzata per ignorare selettivamente i Collider durante la proiezione di un raggio
                //Il RayCast termina nel momento in cui colpisce qualcosa nell'obstrunctionMask (tipo i muri)
                //Facciamo prima il controllo positivo col !, quindi se non stiamo colpendo qualcosa nell'obstructionMask
                if (!Physics.Raycast(enemyGameObject.transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    playerInSightRange = true;
                else
                    playerInSightRange = false;
            }
            else
                playerInSightRange = false;
        }
        //Fallisce il controllo se il player non è alla portata, non si trova nemmeno nel raggio
        else
            playerInSightRange = false;
    }



    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        
    }

}
