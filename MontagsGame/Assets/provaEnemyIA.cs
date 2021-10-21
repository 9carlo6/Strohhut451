using UnityEngine;
using UnityEngine.AI;

public class provaEnemyIA : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player;

    public Transform target;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public Transform[] wayPoints;   //array di points cui il nemico dovrà effettuare il patroling. Sarebbe il vettore di waypoint da percorrere
    private int wayPointIndex;   //indice per tenere conto dei points verso cui muoversi 

    //raggio di vista
    public float viewRadius;
    [Range(0, 360)]



    public float viewAngle;   //angolo di vista

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    public LayerMask targetMask;    //bersagli, cio� il player
    public LayerMask obstructionMask; //ostacoli, ad esempio le pareti

    //States
    // public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FieldOfViewCheck();   //Il fov dovr� essere sempre attivo, ritorna il valore di playerInSightRange

        if (!playerInSightRange)
            //se il player NON � nel campo visivo del nemico, esso continuer� il patroling
            Patroling();
        else
            //se il player E' nel campo visivo del nemico, esso inseguir� il player
            ChasePlayer();
    }

    private void FieldOfViewCheck()
    {
        //Inizializziamo un'array con tutti i collider che toccano o sono dentro la sfera con i parametri passati
        //Centro della sfera, raggio, layer di collider da includere nella query
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        //Se qualcosa collide andiamo in questo if, quindi la lunghezza dell'array sar� diversa da zero
        //Qui basta un semplice if perch� sul layer targetMask c'� solo il player, altrimenti avremmo dovuto fare un for per scorrere l'array
        if (rangeChecks.Length != 0)
        {
            //target sar� pari alla prima istanza di rangeChecks, cio� la trasform del player
            Transform target = rangeChecks[0].transform;

            //Definiamo la direzione verso cui il nostro nemico sta guardando
            //Differenza tra la posizione del player (prima istanza del Collider[]) e il nemico
            Vector3 directionToTarget = (target.position - transform.position).normalized; //normalizzato tra 0 e 1

            //Transform.forward ritorna un vettore normalizzato rappresentante l'asse z
            //Quindi verifichiamo se l'angolo tra questi due vettori � minore dell'angolo di visuale fratto 2
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                //Distanza tra la posizione del nemico e quella del player
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //Col RayCast � come se dotassimo il nemico di un occhio, avviene il lancio di un raggio
                //Parametri: origine del raggio, direzione, distanza massima che il raggio deve controllare per le collisioni
                //Maschera di livello utilizzata per ignorare selettivamente i Collider durante la proiezione di un raggio
                //Il RayCast termina nel momento in cui colpisce qualcosa nell'obstrunctionMask (tipo i muri)
                //Facciamo prima il controllo positivo col !, quindi se non stiamo colpendo qualcosa nell'obstructionMask
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
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

    private void Patroling()
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
    /* private void SearchWalkPoint()
     {
         //Calculate random point in range
         float randomZ = Random.Range(-walkPointRange, walkPointRange);
         float randomX = Random.Range(-walkPointRange, walkPointRange);

         walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

         if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
             walkPointSet = true;
     }*/

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
        transform.LookAt(target);
    }

    /*private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }*/
}
