using UnityEngine;

public class SmoothFollowAttack : CharacterMovement
{
    private Transform player;  // Reference to the player object
    public float followSpeed = 5f,searchSpeed;  // Speed of following
    public float attackDistance = 2f;  // Distance to initiate attack
    private Animator animator;  // Reference to the animator component
    private Rigidbody rb;
    private Vector3 targetPosition;
    private float aniSpeed;
    public bool searching;
    private void Start()
    {   
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        //aniSpeed = animator.speed;
    }
    private void OnEnable()
    {
        EventManager.Onclicked += MoveAround;
    }
    private void OnDisable()
    {
        EventManager.Onclicked -= MoveAround;
    }
    void MoveAround()
    {
        animator.applyRootMotion = true;
        //rb.isKinematic = true;
        searching = true;
        animator.SetBool("Follow", false);
        animator.SetBool("Search", true);
    }
    void FixedUpdate()
    {
        if (!searching)
        {
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            Rotation(new Vector3(targetPosition.x, 0, targetPosition.z));

            if (distance <= attackDistance)
            {

                Stop();
            }

            else if (distance > attackDistance)
            {

                Move();
            }
        }
        else
        {
            Search();
        }
       
    }
    void Search()
    {
        
        targetPosition = transform.position + transform.forward;
        Movement(targetPosition, searchSpeed);
    }
    void Move()
    {
        //animator.speed = 3f;
        animator.SetBool("Follow",true);
        // Calculate the desired position
        targetPosition = player.position - transform.position;      
        // Move the Rigidbody to the target position
        Movement(targetPosition,followSpeed);
    }
    void Stop()
    {
        //animator.SetTrigger("Attack");
        rb.velocity = Vector3.zero;
        Movement(Vector3.zero, 0);
    }
}