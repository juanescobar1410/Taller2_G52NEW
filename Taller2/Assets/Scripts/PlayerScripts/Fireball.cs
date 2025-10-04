using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField]private float speed = 4.5f;
    [SerializeField]private float TimeToDestroy = 2.5f;

    private float time = 0f;
    public float direction = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(direction == -1)
        {
            Vector3 scaleTemp = transform.localScale;
            scaleTemp.x *= -1;
            transform.localScale = scaleTemp;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * direction * Time.deltaTime, transform.position.y, transform.position.z);
        time += Time.deltaTime;
        if (time >= TimeToDestroy)
        {
            Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
           
    }

    private void OnDestroy()
    {
        PlayerController player = Object.FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.ClearFireball();
        }
    }

}
