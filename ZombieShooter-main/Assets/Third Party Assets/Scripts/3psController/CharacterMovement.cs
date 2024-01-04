using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour {

	private Rigidbody myRigidbody;
	
    private CharacterAnimation playerAnimation;
    void Awake () {
		myRigidbody = GetComponent<Rigidbody>();
		playerAnimation = GetComponent<CharacterAnimation>();
		
		
	} 
	
	public void Movement (Vector3 direction, float speed) {
	
		myRigidbody.MovePosition (myRigidbody.position + (speed * Time.deltaTime * direction.normalized));
	}
	
	public void Rotation (Vector3 direction) 
	{
		// rotates the enemy towards the player
		Quaternion newRotation = Quaternion.LookRotation(direction);
		myRigidbody.MoveRotation (newRotation);
	
    }

	public void Die() {
		
		myRigidbody.isKinematic = true;
		GetComponent<Collider>().enabled = false;
	}

}
