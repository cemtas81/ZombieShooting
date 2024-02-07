

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CharacterMovement {
    

    public GameObject pad;
    [SerializeField] private float range;
    public Transform target;
    public float speed = 2.0f,turnSpeed, nextUpdate;

    private Camera m_camera;
    public Transform aim;
    public Texture2D texture,texture2;
    private void Start()
    {
  
        m_camera = Camera.main; 

    }

    public void PlayerRotation(LayerMask groundMask)
    {

        // it uses a LayerMask that computes only the Raycasts that collide with the ground
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null)
        {

            //Vector3 mousePosition = Input.mousePosition;
            //Ray ray = m_camera.ScreenPointToRay(mousePosition);

            //if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            //{

            //    Vector3 positionPoint = hit.point - transform.position;
            //    //target.position = positionPoint;
            //    positionPoint.y = 0;
            //    Rotation(positionPoint.normalized);

            //    //Debug.DrawLine(ray.origin, hit.point, Color.red);
            //    if (hit.collider.gameObject.CompareTag("Zombie"))
            //    {
            //        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
            //    }
            //    else
            //    {
            //        Cursor.SetCursor(texture2, Vector2.zero, CursorMode.Auto);
            //    }
            //}
           
            Vector3 pos =target.position-transform.position;
            pos.y = 0;
            Rotation(pos.normalized);
        }

        else if (gamepad.rightStick.IsActuated(0F))
        {

            Vector3 positionPoint = aim.position - transform.position;

            positionPoint.y = 0;
            Rotation(positionPoint);
            target.position = positionPoint;

        }
    }
}
