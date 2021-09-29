using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Questa classe serve per la gestione della Ragdoll collegata al nemico
public class Ragdoll : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    public void DeactivateRagdoll()
    {
        foreach(var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void ActivateRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
    }


    //Questa funzione serve per appilcare della forza nel momento in cui il nemico muore (per farlo saltare)
    public void ApplyForce(Vector3 force)
    {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
