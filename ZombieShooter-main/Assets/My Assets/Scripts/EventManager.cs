
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class EventManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static EventManager Instance { get; private set; }
    public static event ClickAction Onclicked;
    public static event ClickAction OnUp;
    public Button cover;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Onclicked();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            OnUp();
        }
    }

}
