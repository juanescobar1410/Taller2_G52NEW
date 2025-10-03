using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenes : MonoBehaviour
{

    public GameObject PanelInstrucciones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void lectorEscena(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    public void mostrarPanel()
    {
        PanelInstrucciones.SetActive(true);
    }

    public void cerrarPanel()
    {
        PanelInstrucciones.SetActive(false);
    }
}