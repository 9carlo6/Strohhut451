using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//questa classe serve per gestire il movimento della telecamera
public class CameraMovement : MonoBehaviour
{
    public Transform target;
    //public float x = 0f;
    //public float y = 0f;
    //public float z = 0f;

    //smoothSpeed serve per rendere più fluido il movimento della telecamera (NON UTILIZZATO)
    public float smoothSpeed = 2.5f;
    public Vector3 offset;

    void FixedUpdate()
    {
      //la posizione della telecamera viene aggiornata in base al target passato (ad esempio quella del giocatore)
      //transform.position = new Vector3(target.transform.position.x + x, target.transform.position.y + y, target.transform.position.z + z);

      Vector3 desiredPosition = target.position + offset;
      Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
      transform.position = smoothedPosition;

      //transform.LookAt(target);
    }
}
