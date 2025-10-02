using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float Globaltime = 0;
    public float Globaltime1 { get => Globaltime; set => Globaltime = value; }

    private Dictionary<string, int> inventario = new Dictionary<string, int>();

    
    public TextMeshProUGUI txtMonedas;
    public TextMeshProUGUI txtPociones;
    public TextMeshProUGUI txtLlaves;

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

    private void ActualizarHUD()
    {
        if (txtMonedas != null) txtMonedas.text =  inventario["Moneda"].ToString();
        if (txtPociones != null) txtPociones.text =  inventario["Pocion"].ToString();
        if (txtLlaves != null) txtLlaves.text =  inventario["Llave"].ToString();
    }
    private void BuscarReferenciasUI()
    {
        var monedasObj = GameObject.Find("txtMonedas");
        var pocionesObj = GameObject.Find("txtPociones");
        var llavesObj = GameObject.Find("txtLlaves");

        if (monedasObj != null) txtMonedas = monedasObj.GetComponent<TextMeshProUGUI>();
        if (pocionesObj != null) txtPociones = pocionesObj.GetComponent<TextMeshProUGUI>();
        if (llavesObj != null) txtLlaves = llavesObj.GetComponent<TextMeshProUGUI>();

        ActualizarHUD();
    }

    public void SumaTimeGlobal(float timeScene)
    {
        Globaltime += timeScene;
        Debug.Log("Tiempo global acumulado: " + Globaltime);
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
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
