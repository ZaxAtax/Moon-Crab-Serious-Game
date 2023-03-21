using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Key : MonoBehaviour
{
    public Text pickupText;
    public string keyName;

    private bool canPickUp = false;
    private bool isPickedUp = false;


    void Start()
    {
      pickupText.enabled = false;
    }

    private void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E) && !isPickedUp)
        {
            // Add key to player inventory
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject.GetComponent<Inventory>().AddKey(keyName);

            isPickedUp = true;
            pickupText.enabled = false;

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
