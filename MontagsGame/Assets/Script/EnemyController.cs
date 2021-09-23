using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Questa classe serve per la gestione del nemico
public class EnemyController : MonoBehaviour
{

    private Rigidbody enemyRigidbody;
    public float moveSpeed;

    public PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        enemyRigidbody.velocity = (transform.forward * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
      //per farlo guardare in direzione del personaggio
      transform.LookAt(thePlayer.transform.position);

    }
}
