
using UnityEngine;

public class Detector : MonoBehaviour
{
    private SmoothFollowAttack attack;
    private void Start()
    {
        attack = GetComponentInParent<SmoothFollowAttack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attack.enabled = true;
        }
    }
}
