using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyChasePlayerState : EnemyBaseState
{

    public float viewRadius;  //raggio di vista
    [Range(0, 360)]           //limitiamo l'angolo di visuale a 360�
    public float viewAngle;   //angolo di vista
//private float offsetPosition = 2.0f;
    Vector3 destinationVector = new Vector3();

    //Dobbiamo fare in modo da inseguire il personaggio e tenere conto degli ostacoli
    //LayerMask permette di specificare i layer da utilizzare in Physics.Raycast
    public LayerMask targetMask;    //bersagli, cio� il player
    public LayerMask obstructionMask; //ostacoli, ad esempio le pareti
    GameObject playerRef;

    PlayerController playerController;
    Transform target;
    NavMeshAgent agent;
    Transform enemyTransform;
    float distanceToTarget;

    public bool playerInSightRange;  //quando vedo il bersaglio = true
    public bool playerInAttackRange = false;
    bool isArmed = true;

    public bool ingaged = false;

    public override void EnterState(EnemyStateManager enemy)
    {
        viewRadius = 10;
        viewAngle = 110;
        playerInSightRange = true;
        Debug.Log("Stato Nemico = Chasing");
        playerRef = GameObject.FindGameObjectWithTag("Player");
        target = playerRef.transform;
        agent = enemy.GetComponent<NavMeshAgent>();
        enemyTransform = enemy.gameObject.transform;
        targetMask = enemy.GetComponent<WayPoints>().targetMask;
        obstructionMask = enemy.GetComponent<WayPoints>().obstructionMask;


        //enemyHealthManager = enemy.GetComponent<EnemyHealthManager>();

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        distanceToTarget = Vector3.Distance(enemyTransform.position, target.position);
        //enemyTransform = enemy.gameObject.transform;
        //target = playerRef.transform;
       
        FieldOfViewCheck();

        if (!playerInSightRange && !ingaged)
        {
            Debug.Log("Patrollllllllll");

            enemy.SwitchState(enemy.PatrollingState);
        }
        else
        {
            if (distanceToTarget <= 1.5f)
            {
                enemy.SwitchState(enemy.AttackMeleeState);

            }
            else if ((distanceToTarget >= 1.5 && distanceToTarget <= 4f) && isArmed == true)
            {
                enemy.SwitchState(enemy.StopAndFireState);

            }
            else
            {
                ChasePlayer();
            }

            
            //ChasePlayer();

        }






    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {

    }


    //FOV
    private void FieldOfViewCheck()
    {
     
        //Inizializziamo un'array con tutti i collider che toccano o sono dentro la sfera con i parametri passati
        //Centro della sfera, raggio, layer di collider da includere nella query
        Collider[] rangeChecks = Physics.OverlapSphere(enemyTransform.position, viewRadius, targetMask);
        //Se qualcosa collide andiamo in questo if, quindi la lunghezza dell'array sarà diversa da zero
        //Qui basta un semplice if perchè sul layer targetMask c'è solo il player, altrimenti avremmo dovuto fare un for per scorrere l'array
        if (rangeChecks.Length != 0)
        {
            //target sarà pari alla prima istanza di rangeChecks, cioè la trasform del player
            Transform target = rangeChecks[0].transform;

            //Definiamo la direzione verso cui il nostro nemico sta guardando
            //Differenza tra la posizione del player (prima istanza del Collider[]) e il nemico
            Vector3 directionToTarget = (target.position - enemyTransform.position).normalized; //normalizzato tra 0 e 1

            //Transform.forward ritorna un vettore normalizzato rappresentante l'asse z
            //Quindi verifichiamo se l'angolo tra questi due vettori è minore dell'angolo di visuale fratto 2
            if (Vector3.Angle(enemyTransform.forward, directionToTarget) < viewAngle / 2)
            {
                //Distanza tra la posizione del nemico e quella del player
                float distanceToTarget = Vector3.Distance(enemyTransform.position, target.position);

                //Col RayCast è come se dotassimo il nemico di un occhio, avviene il lancio di un raggio
                //Parametri: origine del raggio, direzione, distanza massima che il raggio deve controllare per le collisioni
                //Maschera di livello utilizzata per ignorare selettivamente i Collider durante la proiezione di un raggio
                //Il RayCast termina nel momento in cui colpisce qualcosa nell'obstrunctionMask (tipo i muri)   
                //Facciamo prima il controllo positivo col !, quindi se non stiamo colpendo qualcosa nell'obstructionMask
                if (!Physics.Raycast(enemyTransform.position, directionToTarget, distanceToTarget, obstructionMask))
                    playerInSightRange = true;
                else
                    playerInSightRange = false;
            }
            else
                playerInSightRange = false;
        }
        //fallisce il controllo se il giocatore non è alla portata, non si trova nemmeno nel raggio
        else
            playerInSightRange = false;
    }


    private void ChasePlayer()
    {

        //si ferma poco prima del player
        destinationVector.x = target.position.x;
        destinationVector.y = target.position.y;
        destinationVector.z = target.position.z + 0.5f;

        

        //Raggiunge la posizione del player, target � il transform del player
        agent.SetDestination(destinationVector);

        // Debug.Log("vado qui : "+ destinationVector.x.ToString() + "-"+destinationVector.y.ToString() + "-"+destinationVector.z.ToString() + "-"); 

        //Il nemico si gira verso il player 
        enemyTransform.LookAt(target);
    }
}
