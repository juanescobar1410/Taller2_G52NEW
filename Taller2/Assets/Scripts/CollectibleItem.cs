using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string itemName;
    public int itemValue;


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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AñadirFruta(itemName, itemValue);
            }
            Destroy(gameObject);
        }
    }
}