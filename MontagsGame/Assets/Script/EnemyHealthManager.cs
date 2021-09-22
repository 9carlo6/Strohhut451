using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Questa classe serve per gestire la vita del nemico
public class EnemyHealthManager : MonoBehaviour
{

    public int health;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    //Tramite l'Update si valuta costantemente la salute del nemico.
    //Nel caso essa fosse uguale o minore di zero allo si distrugge il nemico.
    void Update()
    {
        if(currentHealth <= 0)
        {
          Destroy(gameObject);
        }
    }

    //Questa funzione serve per diminuire la vita del nemico ogni volta che esso viene colpito.
    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }
}
