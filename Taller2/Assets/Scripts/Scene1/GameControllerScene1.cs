using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScene1 : MonoBehaviour
{


    public Timer tiempoEscena;
    public GameObject PanelTime;


    public void GuardarTiempoEscena()
    {

        // Accedemos al stopTime
        tiempoEscena.TimerStop();
        float timeScene1 = tiempoEscena.StopTime1;
        // Lo mandamos al GameManager
        GameManager.Instance.SumaTimeGlobal(timeScene1);

        Debug.Log("Tiempo de esta escena: " + timeScene1);
        //Debug.Log("Tiempo global acumulado: " + GameManager.Instance.Globaltime1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            GuardarTiempoEscena();
            lectorEscena("Scene2");
            //PanelTime.SetActive(false);
        }
    }
    public void lectorEscena(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
