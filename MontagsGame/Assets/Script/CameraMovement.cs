using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFeatures;

//questa classe serve per gestire il movimento della telecamera
public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    public PlayerController playerController;
    //public float x = 0f;
    //public float y = 0f;
    //public float z = 0f;

    //smoothSpeed serve per rendere più fluido il movimento della telecamera
    public float smoothSpeed = 2.5f;
    public Vector3 offset;

    //Per accedere a delle informazioni della camera
    private Camera mainCamera;

    //Per gestire il movimento della camera quando viene premuto il tasto shift
    public float maxCameraDistance = 5;

    void Awake(){
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            //la posizione della telecamera viene aggiornata in base al target passato (ad esempio quella del giocatore)
            //transform.position = new Vector3(target.transform.position.x + x, target.transform.position.y + y, target.transform.position.z + z);

            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            //transform.LookAt(target);
        }

        //Per spostare la telecamera quando si preme il tasto shift
        //Per poterla spostare il giocatore deve avere il booleano a true
        //Questo puo è essere ottenuto prendendo la TreasureChest (per ora)
        if (playerController.isFOVincreased && (bool)((playerController.features)[HumanFeature.FeatureType.FT_INCREASED_FOV]).currentValue)
        {
            handleCameraMovementOnShifPressed();
        }
    }

    //Funzione per gestire lo spostamentod della camera quando si preme il tasto shift
    public void handleCameraMovementOnShifPressed()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            //Bisogna evitare che la distanza dal giocatore superi un certo valore
            if (Vector3.Distance(pointToLook, player.transform.position) > maxCameraDistance)
            {
                Vector3 fromOriginToObject = pointToLook - player.transform.position;
                fromOriginToObject *= maxCameraDistance / Vector3.Distance(pointToLook, player.transform.position);
                pointToLook = player.transform.position + fromOriginToObject;
            }

            Vector3 desiredPosition = pointToLook + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}