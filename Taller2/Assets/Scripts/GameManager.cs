using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool FireballinScene = false;

    private float Globaltime = 0;
    public float Globaltime1 { get => Globaltime; set => Globaltime = value; }

    private int vidas = 3;

    public HUD hud;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (hud == null) hud = Object.FindFirstObjectByType<HUD>();
    }



    public void SumaTimeGlobal(float timeScene)
    {
        Globaltime += timeScene;
        Debug.Log("Tiempo global acumulado: " + Globaltime);
    }

    public void PerderVida()
    {
        vidas -= 1;
        hud.DesactivarVidas(vidas);
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
