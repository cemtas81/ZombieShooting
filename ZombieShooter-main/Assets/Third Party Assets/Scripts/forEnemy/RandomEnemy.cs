using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    private List<GameObject> list=new List<GameObject>();
    private int randomIndex;
    // Start is called before the first frame update
    void Awake()
    {
        Transform parentT= GetComponent<Transform>();
        foreach (Transform item in parentT)
        {
            list.Add(item.gameObject);
        }
        ActivateRandom();
    }
    void ActivateRandom()
    {
        if (list.Count>0)
        {
            randomIndex = Random.Range(0, list.Count);
            list[randomIndex].SetActive(true);  
        }
    }
}
