using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
	//Per controllare il valore della velocità
	public float moveSpeed;
	private Vector3 moveVelocity;
	private Rigidbody myRigidbody;

	//Per accedere a delle informazioni della camera
	private Camera mainCamera;

	//Serve per muovere il personaggio con il new input System
	PlayerInput playerInput;
	CharacterController characterController;

	//Variabili per salvare i valori di input del giocatore
	Vector2 currentMovementInput;
	Vector3 currentMovement;
	[HideInInspector] public bool isMovementPressed;
	[HideInInspector] public bool isAttackButtonPressed;
	[HideInInspector] public bool isAttacking;
	[HideInInspector] public bool isDeath;

	//Per l'animazione
	public Animator animator;
	//Queste due variabili servono per modificare l'animazione in base alla direzione del personaggio
	float velocityX = 0.0f;
	float velocityZ = 0.0f;

	//Per la gestione dello sparo
	public GameObject weapon;
	//Per collegarsi alla classe che gestisce l'arma
	WeaponController weaponController;

	//Per accedere allo script RigBuilder
	public RigBuilder rigBuilder;

	//Per l'attacco corpo a corpo
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public float attackRange = 0.5f;
	public float meleeDamage = 1f;

	//Per modificare i materiali dei figli runtime
	public Material[] material;
	private GameObject astroBody;
	private GameObject astroHead;
	[HideInInspector] public Renderer renderAstroBody;
	[HideInInspector] public Renderer renderAstroHead;

	void Awake()
	{
		playerInput = new PlayerInput();
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		rigBuilder = GetComponent<RigBuilder>();
		weaponController = GetComponentInChildren<WeaponController>();

		//Inizio - Componenti dei Figli
		astroBody = transform.Find("CorpoAstronauta").gameObject;
		astroHead = transform.Find("TestaAstronauta").gameObject;
		renderAstroBody = astroBody.GetComponent<Renderer>();
		renderAstroHead = astroHead.GetComponent<Renderer>();
		//Fine - Componenti dei Figli

		//Callbacks per il movimento
		//ascolta quando il giocatore inizia a utilizzare l'azione Move
		playerInput.CharacterControls.Move.started += onMovementInput;
		//ascolta quando il giocatore rilascia i tasti
		playerInput.CharacterControls.Move.canceled += onMovementInput;
		//questa serve nel momento in cui si controlla il personaggio con il joystick
		//perchè il valore in questo caso non è solo 0 o 1 (ma modellato fra questi due)
		playerInput.CharacterControls.Move.performed += onMovementInput;

		//Callbacks per la gestione dello sparo
		//playerInput.CharacterControls.Fire.performed += _ => weaponController.StartFiring();
		playerInput.CharacterControls.Fire.performed += _ => weaponController.isFiring = true;
		//playerInput.CharacterControls.Fire.canceled += _ => weaponController.StopFiring();
		playerInput.CharacterControls.Fire.canceled += _ => weaponController.isFiring = false;

		//Callbacks per l'attacco corpo a corpo
		playerInput.CharacterControls.MeleeAttack.performed += _ => isAttackButtonPressed = true;
		playerInput.CharacterControls.MeleeAttack.canceled += _ => isAttackButtonPressed = false;
	}

	//Funzione per gestire la Callback del movimento
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

	// Update is called once per frame
	void Update()
	{
		//per poter far muovere il personaggio
		//per potersi muovere il personaggio non deve star attaccando e non deve essere morto 

		isAttacking = animator.GetBool("isAttacking");
		isDeath = animator.GetBool("isDeath");

		if(!isAttacking && !isDeath){
			characterController.Move(currentMovement * Time.deltaTime * moveSpeed);
			handlePlayerRotation();
			
			handleFiring();
		
		}
		handleAnimation();
	}

	//Per gestire la rotazione del player con il movimento del mouse
	void handlePlayerRotation()
    {
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		//Resulting plane has normal inNormal and goes through a point inPoint.
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;
		if (groundPlane.Raycast(cameraRay, out rayLength))
		{
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);

			//Per disengare una linea che parte dalla telecamera e arriva al punto in cui guarda il player
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

			//Questo pezzo serve per far ruotare il personaggio in base alla posizione del mouse
			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}
	}

	//Per gestire lo sparo dell'arma
	void handleFiring()
    {
		if (weaponController.isFiring)
		{
			weaponController.StartFiring();
			weaponController.UpdateFiring(Time.deltaTime);
			
		}
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

		//velocityX e velocityZ servono per gestire l'animazione della camminata.
		//il loro valore cambia in base al movimento del personaggio.
		//Vector3.Dot serve per fare il prodotto tra due vettori, moltiplicandolo poi per il coseno dell'angolo fra i due.
		//In particolare per velocityZ abbiamo currentMovement (ovvero la direzione del personaggio) e transform.forward (che restituisce un valore positivo
		//se il personaggio si muove in avanti, negativo altrimenti)
		//Per velocityX è la stessa cosa ma per il movimento laterale.
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

	//Funzione per il debug dell'attacco corpo a corpo
	void OnDrawGizmosSelected()
    {
		if (!animator.GetBool("isAttacking"))
			return;
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
