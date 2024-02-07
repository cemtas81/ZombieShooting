using UnityEngine;


public class CharacterMovement : MonoBehaviour {

	private Rigidbody myRigidbody;
    public float rotationSpeed = 5f;
    private CharacterAnimation playerAnimation;
    void Awake () {
		myRigidbody = GetComponent<Rigidbody>();
		playerAnimation = GetComponent<CharacterAnimation>();
				
	} 
	
	public void Movement (Vector3 direction, float speed) {
	
		myRigidbody.MovePosition (myRigidbody.position + (speed * Time.deltaTime * direction.normalized));
	}
    public void Rotation(Vector3 direction)
    {
        // Determine the rotation towards the target direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate towards the target rotation
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the new rotation
        myRigidbody.MoveRotation(newRotation);
    }
    public void Die() {
		
		myRigidbody.isKinematic = true;
		GetComponent<Collider>().enabled = false;
	}

}
