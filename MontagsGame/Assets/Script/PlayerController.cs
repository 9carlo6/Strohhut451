using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //per controllare il valore della velocità
    public float moveSpeed;
    private Vector3 moveVelocity;
    private Rigidbody myRigidbody;

    //utulizzato nel vecchio sistema di input
    //private Vector3 moveInput;

    //per accedere a delle informazioni della camera
    private Camera mainCamera;

    //serve per muovere il personaggio con il new input System
    PlayerInput playerInput;
    CharacterController characterController;

    //variabili per salvare i valori di input del giocatore
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    //per l'animazione
    Animator animator;
    //queste due variabili servono per modificare l'animazione in base alla direzione del personaggio
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;


    void Awake()
    {
      playerInput = new PlayerInput();
      characterController = GetComponent<CharacterController>();
      animator = GetComponent<Animator>();

      playerInput.CharacterControls.Move.started += onMovementInput;
      playerInput.CharacterControls.Move.canceled += onMovementInput;
      playerInput.CharacterControls.Move.performed += onMovementInput;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
      currentMovementInput = context.ReadValue<Vector2>();
      currentMovement.x = currentMovementInput.x;
      currentMovement.z = currentMovementInput.y;
      isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    //Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    //Per gestire le animazioni
    void handleAnimation()
    {
      //Prende i parametri dall'animator
      bool isWalking = animator.GetBool("isWalking");
      bool isRunning = animator.GetBool("isRunning");

      if(isMovementPressed && !isWalking) {
        animator.SetBool("isWalking", true);
      }
      else if(!isMovementPressed && isWalking) {
        animator.SetBool("isWalking", false);
      }
    }

    // Update is called once per frame
    void Update()
    {
        //vecchio sistema di input
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput *moveSpeed;

        //nuovo sistema di input
        //per poter far muovere il personaggio
        characterController.Move(currentMovement * Time.deltaTime * moveSpeed);
        //Debug.Log("A = (" + currentMovement.x + ", " + currentMovement.z + ")");

        //Da qui parte il codice per controllare la rotazione con il movimento del mouse
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
          Vector3 pointToLook = cameraRay.GetPoint(rayLength);
          Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
          //Debug.Log("B = (" + pointToLook.x + ", " + pointToLook.z + ")");

          //questo pezzo serve per far ruotare il personaggio in base alla posizione del mouse
          transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        //velocityX e velocityZ servono per gestire l'animazione
        //il loro valore cambia in base al movimento del personaggio.
        //Vector3.Dot serve per fare il prodotto tra due vettori, moltiplicandolo poi per il coseno dell'angolo fra i due.
        //In particolare per velocityZ abbiamo currentMovement (ovvero la direzione del personaggio) e transform.forward (che restituisce un valore positivo
        //se il personaggio si muove in avanti, negativo altrimenti)
        //Per velocityX è la stessa cosa ma per il movimento laterale.
        handleAnimation();
        velocityZ = Vector3.Dot(currentMovement.normalized, transform.forward);
        velocityX = Vector3.Dot(currentMovement.normalized, transform.right);

        animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);

    }

    void OnEnable()
    {
      //serve per abilitare la character controls action map
      playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
      //serve per disabilitare la character controls action map
      playerInput.CharacterControls.Disable();
    }


    void FixedUpdate(){
      myRigidbody.velocity = moveVelocity;
    }

}
