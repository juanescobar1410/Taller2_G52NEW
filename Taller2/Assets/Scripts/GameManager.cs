using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float Globaltime = 0;
    public float Globaltime1 { get => Globaltime; set => Globaltime = value; }
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SumaTimeGlobal(float timeScene)
    {
        Globaltime += timeScene;
        Debug.Log("Tiempo global acumulado: " + Globaltime);
    }

    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
