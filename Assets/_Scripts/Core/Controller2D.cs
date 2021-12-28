using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller2D : MonoBehaviour
{

	public static Controller2D controller;
	
    [Header("Player Controls")]
    public float moveSpeed = 4f;
    public int playerHealth = 1;

    //LayerMask to determine what is considered ground for player
    public LayerMask whatIsGround;

    //for checking if player is grounded
    public Transform groundCheck;

    public bool playerCanMove = true;

    //private variables below
    Transform _transform;
    Rigidbody2D _rigidbody;
    Animator _animator;

    // hold player motion in this timestep
	float _vx;
	float _vy;
	Vector2 moveDirection;

	// player tracking
	bool facingRight = false;
	[HideInInspector] public bool isGrounded = false;
	bool isRunning = false;

	// store the layer the player is on (setup in Awake)
	int _playerLayer;

	void Awake()
	{
		if(controller == null) controller = GameObject.FindObjectOfType<Controller2D>();

		// get a reference to the components we are going to be changing and store a reference for efficiency purposes
		_transform = GetComponent<Transform> ();
		
		_rigidbody = GetComponent<Rigidbody2D> ();
		if (_rigidbody==null) // if Rigidbody is missing
			Debug.LogError("Rigidbody2D component missing from this gameobject");

		_animator = GetComponent<Animator>();
		if (_animator==null) // if Animator is missing
			Debug.LogError("Animator component missing from this gameobject");

		// determine the player's specified layer
		_playerLayer = this.gameObject.layer;
	}

    // Start is called before the first frame update
    void Start()
    {
		if(GameManager.gm)
		{
			GameManager.gm.LoadText();
			if(GameManager.gm.currentLevel == "Scene05")
			{
				InventoryPrefab.ip.SetValue();
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (DialogueManager.dm.dialogueIsPlaying || GameManager.gm.introTextPlaying)
		{
			FreezeMotion();
		}
		else UnFreezeMotion();

        // exit update if player cannot move or game is paused
		if (!playerCanMove || (Time.timeScale == 0f))
			return;
		
		// determine horizontal velocity change based on the horizontal input
		moveDirection = InputManager.im.GetMoveDirection();

		// set the running animation state
		_animator.SetBool("isRunning", isRunning);

		// Determine if running based on the horizontal movement
		if (InputManager.im.moving)
		{
			isRunning = true;
		}
		else isRunning = false;

        // Change the actual velocity on the rigidbody
		_rigidbody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y);
    }

    void LateUpdate()
    {
        // get the current scale
		Vector3 localScale = _transform.localScale;

		// Determine the currect scale
		if (moveDirection.x > 0) // moving right so face right
		{
			facingRight = true;
		} else if(moveDirection.x < 0) {  // moving left so face left
			facingRight= false;
		}

		// check to see if scale x is right for the player
		// if not, multiple by -1 which is an easy way to flip a sprite
		if (((facingRight) && (localScale.x<0)) || ((!facingRight) && (localScale.x>0))) {
			localScale.x *= -1;
		}

		// update the scale
		_transform.localScale = localScale;
    }

     // do what needs to be done to freeze the player
 	public void FreezeMotion() {
		playerCanMove = false;
        _rigidbody.velocity = new Vector2(0,0);
		_rigidbody.isKinematic = true;
	}

	// do what needs to be done to unfreeze the player
	public void UnFreezeMotion() {
		playerCanMove = true;
		_rigidbody.isKinematic = false;
	}

    //public function on victory over the level
	public void Complete() {
		FreezeMotion ();
		if (GameManager.gm) // do the game manager level compete stuff, if it is available
			GameManager.gm.LevelCompete();
	}

	// public function to respawn the player at the appropriate location
	public void Respawn(Vector3 spawnloc) {
		UnFreezeMotion();
		playerHealth = 1;
		_transform.parent = null;
		_transform.position = spawnloc;
	}
}
