using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

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
	public bool isMovementPressed;
	public bool isAttackButtonPressed;
	public bool isAttacking;

	//per l'animazione
	public Animator animator;
	//queste due variabili servono per modificare l'animazione in base alla direzione del personaggio
	float velocityX = 0.0f;
	float velocityZ = 0.0f;
	public float acceleration = 2.0f;
	public float deceleration = 2.0f;

	//Per accedere alla pistola
	public GameObject weapon;

	//Per accedere allo script RigBuilder
	public RigBuilder rigBuilder;

	//Per modificare i materiali dei figli runtime
	public Material[] material;
	private GameObject astroBody;
	private GameObject astroHead;
	public Renderer renderAstroBody;
	public Renderer renderAstroHead;

	void Awake()
	{
		playerInput = new PlayerInput();
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		rigBuilder = GetComponent<RigBuilder>();

		//Inizio - Componenti dei Figli
		astroBody = transform.Find("CorpoAstronauta").gameObject;
		astroHead = transform.Find("TestaAstronauta").gameObject;
		renderAstroBody = astroBody.GetComponent<Renderer>();
		renderAstroHead = astroHead.GetComponent<Renderer>();
		//Fine - Componenti dei Figli


		//ascolta quando il giocatore inizia a utilizzare l'azione Move
		playerInput.CharacterControls.Move.started += onMovementInput;
		//ascolta quando il giocatore rilascia i tasti
		playerInput.CharacterControls.Move.canceled += onMovementInput;
		//questa serve nel momento in cui si controlla il personaggio con il joystick
		//perchè il valore in questo caso non è solo 0 o 1 (ma modellato fra questi due)
		playerInput.CharacterControls.Move.performed += onMovementInput;


		//Callbacks per l'attacco corpo a corpo
		playerInput.CharacterControls.MeleeAttack.performed += _ => isAttackButtonPressed = true;
		playerInput.CharacterControls.MeleeAttack.canceled += _ => isAttackButtonPressed = false;
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
		//isRunning ancora non utilizzato
		bool isRunning = animator.GetBool("isRunning");

		//Gestione Camminata
		if (isMovementPressed && !isWalking)
		{
			animator.SetBool("isWalking", true);
		}
		else if (!isMovementPressed && isWalking)
		{
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
		//per potersi muovere il personaggio non deve star attaccando
		isAttacking = animator.GetBool("isAttacking");
		if(!isAttacking){
			characterController.Move(currentMovement * Time.deltaTime * moveSpeed);
			//Debug.Log("A = (" + currentMovement.x + ", " + currentMovement.z + ")");
		}

		//Da qui parte il codice per controllare la rotazione con il movimento del mouse
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		//Resulting plane has normal inNormal and goes through a point inPoint.
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
