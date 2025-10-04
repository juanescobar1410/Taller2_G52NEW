using TMPro;
using UnityEngine;

public class GameControllerScene2 : MonoBehaviour
{
    public Timer tiempoEscena;

    public GameObject panelResultados;
    public GameObject panelTimer;

    public TextMeshProUGUI txtPuntosResultados;
    public TextMeshProUGUI txtMonedasPanel;
    public TextMeshProUGUI txtPocionesPanel;
    public TextMeshProUGUI txtLlavesPanel;

    private void Awake()
    {
        BuscarReferenciasUI();
    }

    public void GuardarTiempoEscena()
    {
        if (tiempoEscena != null)
        {
            // Detenemos el timer y obtenemos el tiempo final
            tiempoEscena.TimerStop();
            float tiempoFinal = tiempoEscena.StopTime1;

            // Guardar en GameManager
            GameManager.Instance.SumaTimeGlobal(tiempoFinal);

            // Mostrar en UI
            txtPuntosResultados.text = tiempoFinal.ToString("F2");

            Debug.Log("⏱ Tiempo de esta escena: " + tiempoFinal);
            Debug.Log("🧮 Tiempo global acumulado: " + GameManager.Instance.Globaltime1);
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el Timer en la escena.");
        }

        // Actualizar ítems recolectados
        if (HUD.Instance != null)
        {
            txtMonedasPanel.text = HUD.Instance.GetMonedas().ToString();
            txtPocionesPanel.text = HUD.Instance.GetPociones().ToString();
            txtLlavesPanel.text = HUD.Instance.GetLlaves().ToString();
        }

        // Activar panel de resultados
        if (panelResultados != null) panelResultados.SetActive(true);
        if (panelTimer != null) panelTimer.SetActive(false);

        Debug.Log("📊 Resultados cargados en el panel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GuardarTiempoEscena();
            Time.timeScale = 0f;
        }
    }

    // 🔍 Busca referencias automáticamente en el Canvas (incluyendo objetos inactivos)
    private void BuscarReferenciasUI()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            Debug.LogError("❌ No se encontró el Canvas en la escena.");
            return;
        }

        Transform[] allChildren = canvas.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            switch (child.name)
            {
                case "PanelTime":
                    tiempoEscena = child.GetComponentInChildren<Timer>();
                    panelTimer = child.gameObject;
                    break;

                case "panelResultados":
                    panelResultados = child.gameObject;
                    break;

                case "txtPuntosResultados":
                    txtPuntosResultados = child.GetComponent<TextMeshProUGUI>();
                    break;

                case "txtMonedasPanel":
                    txtMonedasPanel = child.GetComponent<TextMeshProUGUI>();
                    break;

                case "txtPocionesPanel":
                    txtPocionesPanel = child.GetComponent<TextMeshProUGUI>();
                    break;

                case "txtLlavesPanel":
                    txtLlavesPanel = child.GetComponent<TextMeshProUGUI>();
                    break;
            }
        }

        Debug.Log("✅ Referencias UI (incluyendo inactivos) encontradas correctamente.");
    }
}