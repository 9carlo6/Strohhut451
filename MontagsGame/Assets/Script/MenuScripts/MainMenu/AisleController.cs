using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AisleController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.back * turnSpeed * Time.deltaTime);
    }
}
