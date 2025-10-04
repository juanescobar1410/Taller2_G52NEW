using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;
    public int itemValue;
    public AudioClip collectSoundMoneda;
    public AudioClip collectSoundPocion;
    public AudioClip collectSoundLlave;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Name item " + itemName + "value item " + itemValue);
            if (HUD.Instance != null)
            {
                HUD.Instance.AñadirItem(itemName, itemValue);
            }
            Destroy(gameObject);
            AudioManager.Instance.ReproducirSonido(
                itemName == "Moneda" ? collectSoundMoneda :
                itemName == "Pocion" ? collectSoundPocion :
                itemName == "Llave" ? collectSoundLlave : null);
        }
    }
}