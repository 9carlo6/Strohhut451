using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    GameObject playerGameObjects;
    float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerGameObjects = GameObject.FindGameObjectWithTag("Player");

       
    }

    // Update is called once per frame
   /* void Update()
    {
        distance = Vector3.Distance(playerGameObjects.transform.position, transform.position);

        if (distance<=4.5 && distance >=4.2)
        {
            animator.SetBool("Open", true);

            Sound();



        }
        else
        {
            animator.SetBool("Open", false);

        }
    }
   */
    void Sound()
    {
        Debug.Log("sooooooooooooooooooooundddddddddddddddddddddddd");
        FindObjectOfType<AudioManager>().Play("OpenDoor");

        //animator.SetBool("Open", false);
    }

}
