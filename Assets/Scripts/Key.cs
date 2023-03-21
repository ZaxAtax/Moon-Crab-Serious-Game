using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add key to player inventory
            other.gameObject.GetComponent<Inventory>().AddKey(keyName);
            
            // Destroy key object
            Destroy(gameObject);
        }
    }
}
