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
	[HideInInspector] private float velocityX = 0.0f;
	[HideInInspector] private float velocityZ = 0.0f;

	//Per la gestione dello sparo
	public GameObject weapon;
	//Per collegarsi alla classe che gestisce l'arma

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

	//Per gestire l'arma corpo a corpo
	public GameObject rodWeapon;

	//Per gestire il FOV
	[HideInInspector] public bool isFOVincreased;

	[HideInInspector] public GameObject[] traps;

	public GameObject levelController;


	void Awake()
	{
		levelController = GameObject.FindGameObjectWithTag("LevelController");
		traps = GameObject.FindGameObjectsWithTag("Trap");
		playerInput = new PlayerInput();
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		rigBuilder = GetComponent<RigBuilder>();
		//weaponController = GetComponentInChildren<WeaponPlayerController>();

		//Inizio - Componenti dei Figli
		astroBody = transform.Find("CorpoAstronauta").gameObject;
		astroHead = transform.Find("TestaAstronauta").gameObject;
		renderAstroBody = astroBody.GetComponent<Renderer>();
		renderAstroHead = astroHead.GetComponent<Renderer>();
		//Fine - Componenti dei Figli

		//Inizio - Inizializzazione delle feature
		string fileString = new StreamReader("Assets/Push-To-Data/Feature/Human/player_features.json").ReadToEnd();
		mapper = JsonUtility.FromJson<HumanFeaturesJsonMap>(fileString);		//Per l'inizializzazione delle collezioni (modifiers, features, components)
		base.Awake();		this.features = mapper.todict();
		//Fine - Inizializzazione delle feature

		//Callbacks per il movimento
		//ascolta quando il giocatore inizia a utilizzare l'azione Move
		playerInput.CharacterControls.Move.started += onMovementInput;
		//ascolta quando il giocatore rilascia i tasti
		playerInput.CharacterControls.Move.canceled += onMovementInput;
		//questa serve nel momento in cui si controlla il personaggio con il joystick
		//perchè il valore in questo caso non è solo 0 o 1 (ma modellato fra questi due)
		playerInput.CharacterControls.Move.performed += onMovementInput;

		//Callbacks per la gestione dello sparo
		playerInput.CharacterControls.Fire.performed += _ => weaponController.isFiring = true;
		playerInput.CharacterControls.Fire.canceled += _ => weaponController.isFiring = false;

		//Callbacks per l'attacco corpo a corpo
		playerInput.CharacterControls.MeleeAttack.performed += _ => isAttackButtonPressed = true;
		playerInput.CharacterControls.MeleeAttack.canceled += _ => isAttackButtonPressed = false;

		//Callbacks per il FOV
		playerInput.CharacterControls.IncreaseFOV.performed += _ => isFOVincreased = true;
		playerInput.CharacterControls.IncreaseFOV.canceled += _ => isFOVincreased = false;

		//Callbacks per utilizzare i gameItems
		playerInput.CharacterControls.Skull.performed += _ => GameObject.FindWithTag("LevelController").GetComponent<Skull>().EnableEffect();
		playerInput.CharacterControls.Helm.performed += _ => GameObject.FindWithTag("LevelController").GetComponent<PirateHelm>().EnableEffect();
		playerInput.CharacterControls.Telescope.performed += _ => GameObject.FindWithTag("LevelController").GetComponent<Telescope>().EnableEffect();
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
	public override void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();

		//Per la composizione delle feature
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		//per poter far muovere il personaggio
		//per potersi muovere il personaggio non deve attaccare e non deve essere morto
		isAttacking = animator.GetBool("isAttacking");
		isDeath = animator.GetBool("isDeath");

		if (!isAttacking && !isDeath && !isStopped)
		{
			characterController.Move(currentMovement * Time.deltaTime * (float)(this.features[HumanFeature.FeatureType.FT_SPEED].currentValue));
			handlePlayerRotation();
		}

		handleAnimation();
		base.Update();
	}
	//Per il setting dei valori delle feature dipendenti da altre (ad ogni frame)	public override void setFeatures()
    {
		//Per settare il valore della velocità in funzione del peso
		this.features[HumanFeature.FeatureType.FT_SPEED].currentValue = (((float)(this.features[HumanFeature.FeatureType.FT_SPEED].baseValue)) * ((float)(this.features[HumanFeature.FeatureType.FT_WEIGHT].baseValue))) / (float)(this.features[HumanFeature.FeatureType.FT_WEIGHT].currentValue);
	}

	//Per il setting dei valori delle feature dipendenti da altre (nella fase iniziale)
	public override void initializeFeatures()
	{
		//Per settare il valore della vita corrente al valore massimo
		//features[HumanFeature.FeatureType.FT_HEALTH].currentValue = features[HumanFeature.FeatureType.FT_MAX_HEALTH].currentValue;
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
			spineTarget.transform.position = new Vector3(pointToLook.x, spineTarget.transform.position.y, pointToLook.z);

			Vector3 targetDir = spineTarget.transform.position - transform.position;

			Quaternion rotTarget = Quaternion.LookRotation(new Vector3(targetDir.x, 0, targetDir.z));

			if (Vector3.Angle(targetDir, transform.forward) >= 40.0f)
			{
				if(!animator.GetBool("isWalking")){
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * 100f * Time.deltaTime);
				}
				else
				{
					transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeed * 300f * Time.deltaTime);
				}
				animator.SetBool("isRotating",true);
			}
			else
			{
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

	//Per gestire le animazioni
	void handleAnimation()
	{

		//Prende i parametri dall'animator
		bool isWalking = animator.GetBool("isWalking");
		//isRunning ancora non utilizzato
		bool isRunning = animator.GetBool("isRunning");

		if (levelController.GetComponent<LevelStateManager>().getCurrentState().Equals("LevelPauseState"))
		{
			FindObjectOfType<AudioManager>().Stop("Running");
        }
        else
        {
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
			nextLevelPlaneCollision = true;
        }
        else
        {
			nextLevelPlaneCollision = false;
		}
	}

	void OnEnable()
	{
		//Serve per abilitare la character controls action map
		playerInput.CharacterControls.Enable();
	}

	void OnDisable()
	{
		//Serve per disabilitare la character controls action map
		playerInput.CharacterControls.Disable();
	}

	//Funzione per il debug dell'attacco corpo a corpo
	void OnDrawGizmosSelected()
	{
		if (!animator.GetBool("isAttacking"))
			return;
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}

	public Modifier getModifierbyID(String id)
    {
		foreach ( Modifier m in modifiers)
        {
            if (m.ID.Equals(id.ToString()))
            {
				return m;
            }
        }
		return null;
    }
}