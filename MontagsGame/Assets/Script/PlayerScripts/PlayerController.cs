using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using System.IO;
using System;
using HumanFeatures;


public class PlayerController : Character
{
	//Per controllare il valore della velocità
	public float moveSpeed;
	private Vector3 moveVelocity;
	private Rigidbody myRigidbody;

	//Per accedere a delle informazioni della camera
	private Camera mainCamera;

	//Serve per muovere il personaggio con il new input System
	private PlayerInput playerInput;
	private CharacterController characterController;

	//Variabili per salvare i valori di input del giocatore
	private Vector2 currentMovementInput;
	private Vector3 currentMovement;
	[HideInInspector] public bool isMovementPressed;
	[HideInInspector] public bool isAttackButtonPressed;
	[HideInInspector] public bool isAttacking;
	[HideInInspector] public bool isDeath;

	//Per l'animazione
	public Animator animator;
	//Queste due variabili servono per modificare l'animazione in base alla direzione del personaggio
	private float velocityX = 0.0f;
	private float velocityZ = 0.0f;

	//Per la gestione dello sparo
	public GameObject weapon;
	//Per collegarsi alla classe che gestisce l'arma
	private WeaponPlayerController weaponController;

	//Per accedere allo script RigBuilder
	[HideInInspector] public RigBuilder rigBuilder;

	//Per l'attacco corpo a corpo
	public Transform attackPoint;
	public LayerMask enemyLayers;
	public float attackRange;
	public float meleeDamage;

	//Per modificare i materiali dei figli runtime
	public Material[] material;
	private GameObject astroBody;
	private GameObject astroHead;
	[HideInInspector] public Renderer renderAstroBody;
	[HideInInspector] public Renderer renderAstroHead;

	//Per far fermare completamente il giocatore
	[HideInInspector] public bool isStopped = false;

	//Per capire se il collide con il piano che serve per passare al livello successivo
	[HideInInspector] public bool nextLevelPlaneCollision = false;

	//Per gestire la rotazione della spina dorsale
	public GameObject spineTarget;
	public float rotationSpeed = 2f;

	//Per gestire l'alert del nemico allo sparo del player
	public bool playerIsFiring = false;
	public float timerToReset = 1;

	//Per gestire l'arma corpo a corpo
	public GameObject rodWeapon;

	//Per gestire la possibilità di poter aumentare il campo visivo premendo shift
	public bool increasedVisualField;

	//Per gestire i Componenti
	//public Dictionary<string, Component> components;

	//Per gestire le feature
	//public Dictionary<string, HumanFeature> features;

	//Per gestire i modificatori
	//public Dictionary<string, Modifier> modifiers;

	//nuovo push to data
	//public List<Feature> features;





	

	GameObject[] traps;


	void Awake()
	{
		traps = GameObject.FindGameObjectsWithTag("Trap");
		playerInput = new PlayerInput();
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		rigBuilder = GetComponent<RigBuilder>();
		weaponController = GetComponentInChildren<WeaponPlayerController>();

		//Inizio - Componenti dei Figli
		astroBody = transform.Find("CorpoAstronauta").gameObject;
		astroHead = transform.Find("TestaAstronauta").gameObject;
		renderAstroBody = astroBody.GetComponent<Renderer>();
		renderAstroHead = astroHead.GetComponent<Renderer>();
		//Fine - Componenti dei Figli

		//Inizio - Inizializzazione delle feature
		string fileString = new StreamReader("Assets/Push-To-Data/Feature/Human/player_features.json").ReadToEnd();
		mapper = JsonUtility.FromJson<HumanFeaturesJsonMap>(fileString);
		base.Awake();
		components.Add(weaponController);
		foreach (Component c in components)
		{
			modifiers.AddRange(c.modifiers);
		}
		this.features = mapper.todict();
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

	//	enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
	}

	// Update is called once per frame
	void Update()
	{
		//per poter far muovere il personaggio
		//per potersi muovere il personaggio non deve star attaccando e non deve essere morto
		isAttacking = animator.GetBool("isAttacking");
		isDeath = animator.GetBool("isDeath");



		//Solo per Debug -> per controllare il valore corrente della velocita tramite l'Inspector

		/*
		moveSpeed = (float) features["moveSpeed"].currentValue;
		Debug.Log("SPEED BASE VALUE: " + features["moveSpeed"].baseValue);
		*/

		if (!isAttacking && !isDeath && !isStopped)
		{

			if (Input.GetKeyDown(KeyCode.T))
			{
				for (int i = 0; i < traps.Length; i++)
				{

					traps[i].GetComponent<Animation>().Play();

				}

			}

			if (!isAttacking && !isDeath && !isStopped)
			{
				characterController.Move(currentMovement * Time.deltaTime * (float)((features)[HumanFeature.FeatureType.FT_SPEED]).currentValue);
				handlePlayerRotation();
				handleFiring();
			}

			handleAnimation();

			applyModifiers();

		}
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
			//transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
			spineTarget.transform.position = new Vector3(pointToLook.x, spineTarget.transform.position.y, pointToLook.z);
			//weapon.transform.LookAt(new Vector3(pointToLook.x, weapon.transform.position.y, pointToLook.z));

			Vector3 targetDir = spineTarget.transform.position - transform.position;
			//Debug.Log("Gradi: " + Vector3.Angle(targetDir, transform.forward));

			Quaternion rotTarget = Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.z));
			if (Vector3.Angle(targetDir, transform.forward) >= 40.0f)
			{
				//Debug.Log("Gradi: " + Vector3.Angle(targetDir, transform.forward));
				//Quaternion rotTarget = Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.z));
				if(!animator.GetBool("isWalking")){
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * 100f * Time.deltaTime);
				}
				else
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * 300f * Time.deltaTime);
				}
				animator.SetBool("isRotating",true);
                //transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
			}
			else
			{
				//weapon.transform.LookAt(new Vector3(pointToLook.x, weapon.transform.position.y, pointToLook.z));
				weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, rotTarget, 50f * Time.deltaTime);
				animator.SetBool("isRotating", false);
			}
		}
	}

	//Funzione necessaria per risalire al nome dell'animazione corrente
	public string GetCurrentClipName()
	{
			AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
			return clipInfo[0].clip.name;
	}

	//Per gestire lo sparo dell'arma
	void handleFiring()
    {

		if (weaponController.isFiring && weaponController.ammoCount > 0)
		{
			
			playerIsFiring = true;
			//weaponController.UpdateFiring(Time.deltaTime);
		}

		if (playerIsFiring && timerToReset > 0)
		{
			timerToReset -= Time.deltaTime;
		}
		else
		{
			playerIsFiring = false;
			timerToReset = 1;
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
			FindObjectOfType<AudioManager>().Play("Running");
			animator.SetBool("isWalking", true);
		}
		else if (!isMovementPressed && isWalking)
		{
			FindObjectOfType<AudioManager>().Stop("Running");
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

	//Rilevamento della collisione con il pavimento che permette di andare al livello successivo
	void OnCollisionEnter(Collision hit)
	{
	   if (hit.collider.tag == "NextLevelPlane")
	   {
			 Debug.Log("Prossimo Piano:sta collidendo");
			 nextLevelPlaneCollision = true;
	   }

		 Debug.Log("il player sta collidendo con qualcosa");
	}

	//Se non collide più con il pavimento che permette di andare al livello successivo

	void OnCollisionExit(Collision hit)
  {
      if (hit.collider.tag == "NextLevelPlane")
      {
				Debug.Log("Prossimo Piano: non sta piu collidendo");
				nextLevelPlaneCollision = false;
      }
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

	public bool getBoolToAlert()
    {
		return playerIsFiring;
    }
}
