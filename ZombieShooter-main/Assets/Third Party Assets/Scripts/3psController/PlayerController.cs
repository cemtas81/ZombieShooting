
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{

	[HideInInspector]public Status playerStatus;
	[SerializeField] private LayerMask groundMask;
	//public ScreenController screenController;
	[SerializeField] private AudioClip damageSound;
    [SerializeField] private Camera cam;
    //public StarterAssetsInputs zone;
    private Vector3 direction;
	private PlayerMovement playerMovement;
	private CharacterAnimation playerAnimation;
	[SerializeField] private GameObject upgrade1,upgrade2,upgrade3,upgrade4,upgrade5,upgradePanel,sword,Bhole,arrowHolder;	
	[SerializeField] private int level_m;
    [SerializeField] private float activeDuration, inactiveDuration;
	private MyController myController1;
	private InputAction action1;
	//public GamePadCursorController joyAim;
	public ItemMover cursorAim;
	//public Outline outLine;
	private bool hitted,covering,canCover;
	public MultiAimConstraint bodyAim;
	
	private void Awake()
	{
		myController1=new MyController();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<CharacterAnimation>();
        playerStatus = GetComponent<Status>();
		
    }
	
	private void OnEnable()
	{
		myController1.Enable();
		action1 = myController1.MyGameplay.MoveCursor;
        //playerAnimation.Boom();
        EventManager.Onclicked += Cover;
        EventManager.OnUp += UnCover;
    }
	private void OnDisable()
	{
		myController1.Disable();
        EventManager.Onclicked -= Cover;
        EventManager.OnUp -= UnCover;
    }
 
    void Update () 
    {

#if UNITY_STANDALONE

		Gamepad gamepad=Gamepad.current;
		if (gamepad != null)
		{

			Cursor.visible = false;	
		}
		else
		{

			Cursor.visible=true;
	
		}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //screenController.Pause();
        }     
        Vector2 moving=action1.ReadValue<Vector2>();
      
        float xAxis = moving.x;
		float zAxis = moving.y;
#endif
#if UNITY_ANDROID || UNITY_IPHONE
		
		float xAxis = zone.move.x;
		float zAxis = zone.move.y;
#endif
		
		// creates a Vector3 with the new direction
		direction = new Vector3 (xAxis, 0, zAxis);
		if (cam != null)
		{
			direction = cam.transform.TransformDirection(direction);
		}
        direction.y = 0;
        // Normalize the movement vector and make it proportional to the speed per second.
        direction = direction.normalized;
        float velocityZ = Vector3.Dot(direction.normalized, transform.forward);
        float velocityX = Vector3.Dot(direction.normalized, transform.right);
        playerAnimation.VelocityZ(velocityZ);
        playerAnimation.VelocityX(velocityX);
        // player animations transition
        playerAnimation.Movement(direction.magnitude);

    }

	void Cover()
	{
		if (!covering && canCover)
		{
			playerAnimation.Cover(true);
			//bodyAim.enabled = false;

			bodyAim.weight = 0;
			covering = true;
		}

	}

	void UnCover()
	{
		if (covering)
		{
			StartCoroutine(UnCovering());

		}

	}
	IEnumerator UnCovering()
	{
        playerAnimation.Cover(false);
        yield return new WaitForSeconds(1.3f);

        bodyAim.weight = .7f;
        covering = false;

    }
	void FixedUpdate () {
		
		if (!covering)
		{
            playerMovement.Movement(direction, playerStatus.speed);
          
            playerMovement.PlayerRotation(groundMask);
            
        }
				
	}

	public void LoseHealth (int damage) {
		playerStatus.health -= damage;
		//screenController.UpdateHealthSlider();

		// plays the damage sound
		//AudioController.instance.PlayOneShot(damageSound);
		if (hitted!=true)
		{
            //outLine.eraseRenderer = false;
			hitted=true;
        }
		StartCoroutine(UnOutline());
		if (playerStatus.health <= 0)
			Die();
	}
	IEnumerator UnOutline() 
	{
			yield return new WaitForSeconds(.5f);			
			//outLine.eraseRenderer=true;
            hitted = false;

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Cover"))
		{
			canCover = true;
			direction = other.transform.forward*-1;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Cover"))
		{
			canCover = false;
		}	
	}
	public void Die () 
	{
		//screenController.GameOver();
		
	}	
    public void HealHealth(int amount) {
        playerStatus.health += amount;
		if (playerStatus.health > playerStatus.initialHealth)
			playerStatus.health = playerStatus.initialHealth;
		//screenController.UpdateHealthSlider();
    }

}

