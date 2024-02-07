using UnityEngine;

public class ItemMover : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    public Texture2D texture, texture2;
    void Update()
    {
        Ray ray=cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit rayHit,float.MaxValue,layerMask))
        {
            transform.position = rayHit.point;
          
        }

    }
    

}
