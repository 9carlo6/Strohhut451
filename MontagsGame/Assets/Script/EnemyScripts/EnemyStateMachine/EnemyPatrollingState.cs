using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyPatrollingState : EnemyBaseState
{

    //Patroling
    public Transform[] wayPoints;   //array di points verso cui il nemico dovr� effettuare il patroling
    private int wayPointIndex = 0;   //indice per tenere conto dei points verso cui muoversi,  primo waypoint

    //FOV, Chase player
    public float viewRadius;  //raggio di vista
    [Range(0, 360)]           //limitiamo l'angolo di visuale a 360�
    public float viewAngle;   //angolo di vista

    //Dobbiamo fare in modo da inseguire il personaggio e tenere conto degli ostacoli
    //LayerMask permette di specificare i layer da utilizzare in Physics.Raycast
    public LayerMask targetMask;    //bersagli, cio� il player
    public LayerMask obstructionMask; //ostacoli, ad esempio le pareti

    NavMeshAgent agent;
    Transform target;
    GameObject playerRef;
    Transform enemyTransform;



    public bool playerInSightRange;  //quando vedo il bersaglio = true





    public override void EnterState(EnemyStateManager enemy)
        {
        Debug.Log("Stato Nemico = Patrolling");
        agent = enemy.GetComponent<NavMeshAgent>();
        enemyTransform = enemy.gameObject.transform;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        target = playerRef.transform;
        wayPoints = enemy.GetComponent<WayPoints>().wayPoints;
      
    }

    public override void UpdateState(EnemyStateManager enemy)
        {

        FieldOfViewCheck();   //Il fov dovr� essere sempre attivo, ritorna il valore di playerInSightRange
        Debug.Log("Sto chekkando e oltre");

        //aggiungere una condizione che verifica se viene attaccato corpo a corpo stealth
        //if(enemy.isAttacked)
        //enemy.SwitchState(EnemyAttackedState);

        if (!playerInSightRange) {
            Debug.Log("Sto facendo il patrolling");
            //se il player NON � nel campo visivo del nemico, esso continuer� il patroling
            Patrolling();
                }
        else
            //se il player E' nel campo visivo del nemico, esso inseguir� il player
            enemy.SwitchState(enemy.ChasePlayerState);
        //quando implementeremo l'attacco del nemico dovremmo includerlo qui
    

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

        //Se qualcosa collide andiamo in questo if, quindi la lunghezza dell'array sar� diversa da zero
        //Qui basta un semplice if perch� sul layer targetMask c'� solo il player, altrimenti avremmo dovuto fare un for per scorrere l'array
        if (rangeChecks.Length != 0)
        {
            //target sar� pari alla prima istanza di rangeChecks, cio� la trasform del player
            Transform target = rangeChecks[0].transform;

            //Definiamo la direzione verso cui il nostro nemico sta guardando
            //Differenza tra la posizione del player (prima istanza del Collider[]) e il nemico
            Vector3 directionToTarget = (target.position - enemyTransform.position).normalized; //normalizzato tra 0 e 1

            //Transform.forward ritorna un vettore normalizzato rappresentante l'asse z
            //Quindi verifichiamo se l'angolo tra questi due vettori � minore dell'angolo di visuale fratto 2
            if (Vector3.Angle(enemyTransform.forward, directionToTarget) < viewAngle / 2)
            {
                //Distanza tra la posizione del nemico e quella del player
                float distanceToTarget = Vector3.Distance(enemyTransform.position, target.position);

                //Col RayCast � come se dotassimo il nemico di un occhio, avviene il lancio di un raggio
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
        //fallisce il controllo se il giocatore non � alla portata, non si trova nemmeno nel raggio
        else
            playerInSightRange = false;
    }


    private void Patrolling()
    {
        viewRadius = 10;
        viewAngle = 110;

        //a ogni frame vado a vedere se non gli ho ancora assegnato un path(!agent.pathPending) e
        //la distanza dal waypoint da raggiungere è0.5(cioè ha raggiunto il prossimo waypoint), si va al waypoint successivo 
        if (!agent.pathPending && agent.remainingDistance < 0.5f)  //se la distanza tra il nemico e il waypoint corrente è minore di 0.5f waypoint successivo
        {
            GotoNextPoint();
        }
    }

    private void GotoNextPoint()
    {
        //se l'array è vuoto non fa niente
        if (wayPoints.Length == 0)
        {
            return;
        }

        //altrimeti setto la destinazione dell'agente al waypoints
        agent.destination = wayPoints[wayPointIndex].position;

        //poi usando un modulo ciclo sui numeri e aumento l'index
        wayPointIndex = (wayPointIndex + 1) % wayPoints.Length;
    }

}
