using UnityEngine;

public class SmoothFollowAttack : CharacterMovement
{
    private Transform player;  // Reference to the player object
    public float followSpeed = 5f;  // Speed of following
    public float attackDistance = 2f;  // Distance to initiate attack
    private Animator animator;  // Reference to the animator component
    private Rigidbody rb;
    private Vector3 targetPosition;
    private float aniSpeed;
    private void Start()
    {   
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        aniSpeed = animator.speed;
    }
    void FixedUpdate()
    {       
       
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;
        Rotation(targetPosition);
        if (distance <= attackDistance)
        {
            
            Stop();
        }

      
        if (distance>attackDistance)
        {
           
            Move();
        }
       
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
        rb.MovePosition(transform.position);
    }
}