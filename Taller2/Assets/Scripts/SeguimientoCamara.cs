
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{

    public Transform personajeObjetivo;
    public float velocidadCamara = 0.025f;
    public Vector3 desplazamiento;

    

    void LateUpdate()
    {
        Vector3 posicion = personajeObjetivo.position + desplazamiento;

        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicion, velocidadCamara);
        transform.position = posicionSuavizada;

    }
}

