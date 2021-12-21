using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
       
    }

    private void OnTriggerEnter(Collider other)
    {
         animator.SetBool("Open", true);
         audioSource.Play();        
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Open", false);
        audioSource.Play();
    }

   


}
