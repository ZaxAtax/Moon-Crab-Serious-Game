using UnityEngine;

public class Key : MonoBehaviour
{
    public Text pickupText;
    public string keyName;
    public float pickupRange = 3f;

    private bool canPickUp = false;

    private void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            // Add key to player inventory
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject.GetComponent<Inventory>().AddKey(keyName);
            
            // Destroy key object
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPickUp = true;
            pickupText.enabled = true;
            pickupText.text = "Press E to pick up";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canPickUp = false;
            pickupText.enabled = false;
        }
    }
}
