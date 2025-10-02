using UnityEngine;

public class SeguimientoFondo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]private Vector2 velocidadMovimiento;
    private Vector2 calculo;
    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        calculo = velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += calculo;
    }
}
