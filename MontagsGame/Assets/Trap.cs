
using UnityEngine;

public class Trap : MonoBehaviour
{
    GameObject player;


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyHealthManager>().currentHealth = 0;

        }

        if (other.tag.Equals("Player"))
        {
            player.GetComponent<PlayerHealthManager>().currentHealth = 0;

        }


    }

}
