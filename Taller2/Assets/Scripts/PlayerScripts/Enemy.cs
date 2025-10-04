using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float fuerzaRebote = 3f;
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    private bool enMovimiento;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animacion;
    private bool recibiendoDanio;
    private bool haMuerto = false;
    public int puntosPorMuerte = 50;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animacion = GetComponent<Animator>();
    }
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            movement = new Vector2(direction.x, 0);
            enMovimiento = true;
        }
        else
        {
            movement = Vector2.zero;
            enMovimiento = false;
        }
        if(!recibiendoDanio) 
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        animacion.SetBool("enMovimiento", enMovimiento);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
     


        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PerderVida();
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            collision.gameObject.GetComponent<PlayerController>().RecibeDanio(direccionDanio, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bola"))
        {
            
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            RecibeDanio(direccionDanio, 1);
        }
    }


    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            Debug.Log("Recibiendo daño");
            MatarEnemigo();
        }
    }

    private void MatarEnemigo()
    {
        if (haMuerto) return; // Evita doble conteo
        haMuerto = true;

        Debug.Log($"Enemigo destruido. +{puntosPorMuerte} puntos");

        if (HUD.Instance != null)
            HUD.Instance.AñadirPuntos(puntosPorMuerte);

        // Aquí puedes agregar animación o efecto antes de destruir
        Destroy(gameObject, 0.2f);
        
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.linearVelocity= Vector2.zero;
        
    }


}
