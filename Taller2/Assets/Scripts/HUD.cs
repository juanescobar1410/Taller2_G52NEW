using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject[] vidas;
    public static HUD Instance;

    private Dictionary<string, int> inventario = new Dictionary<string, int>();


    public TextMeshProUGUI txtMonedas;
    public TextMeshProUGUI txtPociones;
    public TextMeshProUGUI txtLlaves;
    public GameObject gameOverPanel;
    public TextMeshProUGUI txtPuntosHUD;

    private int totalPuntos = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inventario["Moneda"] = 0;
        inventario["Pocion"] = 0;
        inventario["Llave"] = 0;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void AñadirItem(string nombre, int cantidad)
    {
        if (!inventario.ContainsKey(nombre))
        {
            inventario[nombre] = 0;
        }

        inventario[nombre] += cantidad;

        ActualizarHUD();
        Debug.Log($"Item recogido: {nombre} (+{cantidad}) | Totales: Monedas={inventario["Moneda"]}, Pociones={inventario["Pocion"]}, Llaves={inventario["Llave"]}");
    }

    public void AñadirPuntos(int cantidad)
    {
        totalPuntos += cantidad;
        ActualizarPuntos();
        Debug.Log($"Puntos añadidos: {cantidad} | Total = {totalPuntos}");
    }

    private void ActualizarPuntos()
    {
        if (txtPuntosHUD != null)
            txtPuntosHUD.text = totalPuntos.ToString();


    }

    public int GetTotalPuntos() => totalPuntos;

    private void ActualizarHUD()
    {
        if (txtMonedas != null) txtMonedas.text = inventario["Moneda"].ToString();
        if (txtPociones != null) txtPociones.text = inventario["Pocion"].ToString();
        if (txtLlaves != null) txtLlaves.text = inventario["Llave"].ToString();
    }
    private void BuscarReferenciasUI()
    {
        var monedasObj = GameObject.Find("txtMonedas");
        var pocionesObj = GameObject.Find("txtPociones");
        var llavesObj = GameObject.Find("txtLlaves");

        if (monedasObj != null) txtMonedas = monedasObj.GetComponent<TextMeshProUGUI>();
        if (pocionesObj != null) txtPociones = pocionesObj.GetComponent<TextMeshProUGUI>();
        if (llavesObj != null) txtLlaves = llavesObj.GetComponent<TextMeshProUGUI>();

        var vidaObjs = GameObject.FindGameObjectsWithTag("Vida");
        if (vidaObjs.Length > 0)
        {
            vidas = vidaObjs;
        }


        ActualizarHUD();
    }

    public int GetMonedas() => inventario["Moneda"];
    public int GetPociones() => inventario["Pocion"];
    public int GetLlaves() => inventario["Llave"];


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BuscarReferenciasUI();


        if (GameManager.Instance != null)
        {
            for (int i = 0; i < vidas.Length; i++)
            {
                vidas[i].SetActive(i < GameManager.Instance.GetVidas());
            }
        }
    }


    public void MostrarGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
            GameManager.Instance.ResetVidas(); 

        ResetAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void IrAlMenu()
    {
        Time.timeScale = 1f; // reanuda el juego

        // Destruir el HUD al volver al menú
        if (HUD.Instance != null)
        {
            Destroy(HUD.Instance.gameObject);
            HUD.Instance = null;
        }

        SceneManager.LoadScene("Menu");
    }



    public void ResetAll()
    {
        // Reset inventario
        inventario["Moneda"] = 0;
        inventario["Pocion"] = 0;
        inventario["Llave"] = 0;
        ActualizarHUD();

        // Reset vidas (todas activas)
        if (vidas != null && vidas.Length > 0)
        {
            foreach (GameObject vida in vidas)
            {
                if (vida != null)
                    vida.SetActive(true);
            }
        }

        // Ocultar panel Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }



    public void DesactivarVidas(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void ActivarVidas(int indice)
    {
        vidas[indice].SetActive(true);
    }
}
