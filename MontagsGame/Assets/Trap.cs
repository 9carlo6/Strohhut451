
using UnityEngine;

public class Trap : MonoBehaviour
{
    GameObject player;
    public BoxCollider boxCollider;
    Renderer renderEnemyBody;
    private GameObject enemyBody;

    //per lo shader 
    public Shader trapShader;
    public Shader baseShader;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxCollider = GetComponent<BoxCollider>();


    }

    
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            player.GetComponent<PlayerHealthManager>().currentHealth = 0;

        }
        else
        {
            if (renderEnemyBody == null)
            {
                Debug.Log("elseeeeeeeeeeee");
                enemyBody = other.transform.Find("EnemyPirateSkin").gameObject;
                renderEnemyBody = enemyBody.GetComponent<Renderer>();
                Debug.Log("setto lo shader");
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
}
