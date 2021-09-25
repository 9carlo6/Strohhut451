using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    //GameObject è la cla verso sse per gestire le entità nella scena
    //playerRef è il nostro player
    public GameObject playerRef;

    //Transform del target (posizione, rotazione, scale)
    //Il target è il nostro player
    Transform target;
    //Singleton class to access the baked NavMesh
    //agent sono i nemici
    NavMeshAgent agent;

    //Patroling
    public Transform[] wayPoints;   //array di pointscui il nemico dovrà effettuare il patroling
    private int wayPointIndex;   //indice per tenere conto dei points verso cui muoversi 

    //FOV, Chase player
    public float viewRadius;  //raggio di vista
    [Range(0, 360)]           //limitiamo l'angolo di visuale a 360°, verrà impostato manualmente nell'inspector
    public float viewAngle;   //angolo di vista


    //dobbiamo fare in modo da inseguire il personaggio e tenere conto degli ostacoli
    //LayerMask permette di specificare i layer da utilizzare in Physics.Raycast
    public LayerMask targetMask;    //bersagli 
    public LayerMask obstructionMask; //ostacoli

    public bool playerInSightRange;  //quando vedo il bersaglio = true
    //da aggiungere poi player in attackRange ed interlacciarlo con il faceTarget

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");        
        target = playerRef.transform;
        agent = GetComponent<NavMeshAgent>();

        wayPointIndex = 0;   //primo waypoint

    }


    private void Update()
    {
        FieldOfViewCheck();   //ritorna playerInSightRange


        if (playerInSightRange == false)
            Patroling();
        else
            ChasePlayer();
    }

    //CHASING

    private void ChasePlayer()
    {
        viewRadius = 13;
        viewAngle = 360;
        //raggiunge la posizione del player, target è il transform del player
        agent.SetDestination(target.position);

        FieldOfViewCheck();

        //Vector3.Distance ritorna la distanza tra la posizione del player e quella del nemico
        if (Vector3.Distance(target.position, transform.position) <= agent.stoppingDistance)
        {
            FaceTarget(); //il nemico si gira di faccia verso il player
        }

    }

    //SI POTREBBE PROVARE CON UNO SWITCH https://www.youtube.com/watch?v=db0KWYaWfeM&t=49s

    /* private IEnumerator FOVRoutine()
     {
         WaitForSeconds wait = new WaitForSeconds(0.2f);

         while (true)
         {
             yield return wait; //aspettiamo questa quantità di tempo (0.2secondi) prima di iniziare la ricerca col fov

             FieldOfViewCheck();

             if (canSeePlayer == true)
             {
                 agent.SetDestination(target.position);    //raggiunge la posizione del player

                 if (Vector3.Distance(target.position, transform.position) <= agent.stoppingDistance)
                 {
                     FaceTarget(); //il nemico si gira di faccia verso il player
                 }
             }
             else //Qualora il nemico non rientri nel campo visivo continua il patroling
             {
                 Patroling();
             }
         }
     }*/

    //FOV
    void FaceTarget()       //questa funzione permette di ruotare al nemico e guardare in faccia il player nell'inseguimento
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //I quaternioni sono usati per rappresentare le rotazioni.
        //Sono compatti, non soffrono di blocco cardanico e possono essere facilmente interpolati
        //LookRotation crea una rotazione con le direzioni in avanti e verso l'alto specificate.
        //In questo caso solo in avanti (Forward), in particolare la x e la z della direzione verso cui voltarsi, cioè il player
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //Ruotiamo il nemico agendo sulla rotation 
        //Creiamo una rotazione che va ad interpolare  tra il primo quaternione (primo parametro)
        //ed il secondo quaternione(lookRotation) ina base al valore del paramentro t (Time.deltaTime * 5f)
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    private void FieldOfViewCheck()
    {
        //Inizializziamo un'array con tutti i collider che toccano o sono dentro la sfera
        //centro della sfera, raggio, layer di collider da includere nella query
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        //se qualcosa collide andiamo in questo if, quindi la lunghezza dell'array sarà diversa da zero
        if (rangeChecks.Length != 0)
        {
            //target sarà pari alla prima istanza di rangeChecks
            Transform target = rangeChecks[0].transform;

            //Definiamo la direzione verso cui il nostro nemico sta guardando
            //Differenza tra la posizione del player (prima istanza del Collider[]) e il nemico
            Vector3 directionToTarget = (target.position - transform.position).normalized; //normalizzato tra 0 e 1

            //Transform.forward ritorna un vettore normalizzato rappresentante l'asse z
            //Quindi verifichiamo se l'angolo tra questi due vettori è minore dell'angolo di visuale fratto 2
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                //distanza tra la posizione del nemico e quella del player
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //RayCast: facciamo il lancio del raggio
                //Parametri: origine del raggio, direzione, distanza massima che il raggio dovrebbe controllare per le collisioni
                //Maschera di livello utilizzata per ignorare selettivamente i Collider durante la proiezione di un raggio
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    playerInSightRange = true;
                else
                    playerInSightRange = false;
            }
            else
                playerInSightRange = false;
        }
        //fallisce il controllo se il giocatore non è alla portata
        else
            playerInSightRange = false;

    }



    //PATROL

    private void Patroling()
    {

        viewRadius = 10;
        viewAngle = 110;
        if (Vector3.Distance(agent.transform.position, wayPoints[wayPointIndex].position) < 1f)  //se la distanza tra il nemico e il waypoint corrente è minore di 1f waypoint successivo
        {
            Debug.Log("Sto incrementando l'index");
            IncreaseIndex();
        }
        Debug.Log("Sto andando verso il walkpoint" + wayPointIndex);
        agent.SetDestination(wayPoints[wayPointIndex].position);   //ci muoviamo al waypoint i-esimo
    }

    private void IncreaseIndex()
    {
        wayPointIndex++;

        if (wayPointIndex >= wayPoints.Length)
        {
            Debug.Log("sto azzerando");
            wayPointIndex = 0;
        }
    }
}