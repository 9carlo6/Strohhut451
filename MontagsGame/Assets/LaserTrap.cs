using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{

    public LineRenderer lineRenderer;
    float durataAnimazione = 1.0f;
    float valoreMinimo = -0.01f;
    float valoreMassimo = 3f;
    public BoxCollider boxCollider;
    EnemyHuman enemyHuman;
    Renderer renderEnemyBody;
    private GameObject enemyBody;




    //per lo shader 
    public Shader trapShader;
    public Shader baseShader;


    void Start()
    {
        //tra i figli
        lineRenderer = GetComponentInChildren<LineRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        enemyHuman = GetComponent<EnemyHuman>();
    }

    private void OnEnable()//QUANDO VIENE ATTIVATO L'OGGETTO
    {
        StartCoroutine(AnimateLaser());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Trigger(other);
        }
        else
        {
            if (renderEnemyBody == null)
            {
                enemyBody = other.transform.Find("EnemyPirateSkin").gameObject;
                renderEnemyBody = enemyBody.GetComponent<Renderer>();
                renderEnemyBody.material.shader = trapShader;//facendo questo è stata commentata una linea di codice nell'enemycontroller relativa al chase
            }
            else
            {
                return;
            }
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (renderEnemyBody != null)
        {
            renderEnemyBody.material.shader = baseShader;
        }
       
    }

    void Trigger(Collider other)
    {
        PlayerHealthManager healthManager = other.GetComponent<PlayerHealthManager>();
        healthManager.currentHealth = 0;
    }
 
    //creiamo una corutine per far muovere il laser
    //la coroutine consente di suddividere un'attività in più frame
    IEnumerator AnimateLaser()
    {
        //vado a prendere il punto iniziale del linerenderer
        Vector3 laserStartingPoint = lineRenderer.GetPosition(0);//
        Vector3 laserEndingPoint = lineRenderer.GetPosition(1);
        Vector3 colliderCenter = boxCollider.center;//
       

        float currentY = valoreMinimo;//partiamo dal valore minimo
        float elapsedTime = 0;//quanto tempo è trascorso

        //variamo la y in modo da far spostare il laser
        while (gameObject.activeSelf)//finchè questo laser è attivo, calcoliamo la y all'interno del range
        {                              //cioè dal valore minimo e massimo e il valore che prendiamo equivale al tempo passato diviso la 
                                       //durata totale
           

            if(Mathf.Abs(currentY) == Mathf.Abs(valoreMassimo))//una volta arrivato al valore massimo, scambio 
            {                              //per tornare giù (Abs è il valore assoluto)
                float temp = valoreMinimo;
                valoreMinimo = valoreMassimo;
                valoreMassimo = temp;
                elapsedTime = Time.deltaTime;//in questo modo creo l'effetto ping pong
            }


            //prendiamo currenty e la trsformiamo mediante la funzione lerp che è l'interpolazione, cioè andare da
            //una valore all'altro in un determinato arco di tempo
            currentY = Mathf.Lerp(valoreMinimo, valoreMassimo, elapsedTime / durataAnimazione);//valore iniziale,valore finale, quanto tempo è passato
            elapsedTime += Time.deltaTime;

            laserStartingPoint.y = currentY;
            laserEndingPoint.y = currentY;

            lineRenderer.SetPosition(0, laserStartingPoint);
            lineRenderer.SetPosition(1, laserEndingPoint);

            //per muovere anche il collider
            colliderCenter.y = currentY;
            boxCollider.center = colliderCenter;

            //a questo punto, bisogna aspettare la fine del frame(poichè il deltatime viene ricalcolato in base all'untimo frame,
            //se aspettiamo 1 frame, stiamo aspettando esattamente il tempo che è passato dall'ultimo frame)
            yield return new WaitForEndOfFrame();
        }

       

        //una volta spostati

    }
}
