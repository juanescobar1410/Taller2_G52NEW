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
        if (vidas > 0)
        {
            vidas--; // primero bajamos el contador

            if (HUD.Instance != null)
            {
                HUD.Instance.DesactivarVidas(vidas); // apagamos el corazón en ese índice
                Debug.Log("Apagando corazón en índice: " + vidas);
            }

            if (vidas <= 0)
            {
                Debug.Log("Game Over 😵");
                // Aquí pones lógica de reinicio, game over, etc.
            }
        }
    }

    public int GetVidas() => vidas;


    public void SincronizarVidas(int vidasActuales)
    {
        if (hud != null && hud.vidas != null)
        {
            for (int i = 0; i < hud.vidas.Length; i++)
            {
                hud.vidas[i].SetActive(i < vidasActuales);
            }
        }
    }



    void Start()
    {

    }


    void Update()
    {

    }
}
