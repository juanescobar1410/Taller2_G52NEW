using UnityEngine;

public class GameControllerScene1 : MonoBehaviour
{


    public Timer tiempoEscena;


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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
