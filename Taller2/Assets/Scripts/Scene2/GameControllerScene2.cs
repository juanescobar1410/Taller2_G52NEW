using TMPro;
using UnityEngine;

public class GameControllerScene2 : MonoBehaviour
{
    public Timer tiempoEscena;
    public GameObject panelResultados;
    public TextMeshProUGUI txtTiempoGlobal;

    public TextMeshProUGUI txtMonedasPanel;
    public TextMeshProUGUI txtPocionesPanel;
    public TextMeshProUGUI txtLlavesPanel;

    public void GuardarTiempoEscena()
    {
        // Accedemos al stopTime
        tiempoEscena.TimerStop();
        float timeScene2 = tiempoEscena.StopTime1;
        // Lo mandamos al GameManager
        GameManager.Instance.SumaTimeGlobal(timeScene2);

        Debug.Log("Tiempo de esta escena: " + timeScene2);
        //Debug.Log("Tiempo global acumulado: " + GameManager.Instance.Globaltime1);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GuardarTiempoEscena();
            Time.timeScale = 0f;

            panelResultados.SetActive(true);

            ActualizarResultados();

            // Mostrar resultados en consola también
            //Debug.Log($"Resultados: Cerezas={GameManager.Instance.Cerezas}, Sandías={GameManager.Instance.Sandias}");
        }
    }
    public void ActualizarResultados()
    {
        if (GameManager.Instance != null)
        {
            // Lee los contadores de GameManager
            //int totalCerezas = GameManager.Instance.Cerezas;
            //int totalSandias = GameManager.Instance.Sandias;
            float tiempo = GameManager.Instance.Globaltime1;

            // Muestra en los textos
            //txtCerezas.text = totalCerezas.ToString();
            //txtSandias.text = totalSandias.ToString();
            txtTiempoGlobal.text = tiempo.ToString("F2");

            Debug.Log("Resultados cargados en el panel");
        }
      
    }


    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HUD.Instance != null)
        {
            txtMonedasPanel.text = HUD.Instance.GetMonedas().ToString();
            txtPocionesPanel.text = HUD.Instance.GetPociones().ToString();
            txtLlavesPanel.text = HUD.Instance.GetLlaves().ToString();
        }
    }
}
