using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvasController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 2f;

    [HideInInspector] public float timeCounterMovement;
    [HideInInspector] public float timeCounterRotation;
    public float defaultTimeCounterMovement = 0.5f;
    public float defaultTimeCounterRotation = 0.1f;
    [HideInInspector] public bool leftDirection;
    [HideInInspector] public bool forwardDirection;

    void Start()
    {
        timeCounterMovement = defaultTimeCounterMovement;
        timeCounterRotation = defaultTimeCounterRotation;
        leftDirection = true;
    }

    void Update()
    {
        handleLateralMovement();
        handleOndulatoryMovement();
    }

    void handleLateralMovement()
    {
        if (timeCounterMovement > 0)
        {
            timeCounterMovement -= Time.deltaTime;

            if (leftDirection)
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            leftDirection = !leftDirection;

            //Deve essere il doppio in quanto partendo dal centro impieghera un tempo paria defaultTimeCounter per arrivare a sinistra
            //e successivamente un tempo pari a defaultTimeCounter * 2 per arrivare a destra (poi sar� sempre cosi)
            timeCounterMovement = defaultTimeCounterMovement * 2;
        }
    }

    void handleOndulatoryMovement()
    {
        if (timeCounterRotation > 0)
        {
            timeCounterRotation -= Time.deltaTime;

            if (forwardDirection)
            {
                transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.back * turnSpeed * Time.deltaTime);
            }
        }
        else
        {
            forwardDirection = !forwardDirection;
            timeCounterRotation = defaultTimeCounterRotation * 2;
        }
    }

}
