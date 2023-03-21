using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeLock : MonoBehaviour
{
    public string code; // the correct code to unlock the lock
    public GameObject lockObject; // the object that will be unlocked when the correct code is entered
    public Text codeInputField; // the input field where the player enters the code
    public Text feedbackText; // a text field to display feedback to the player

    private bool isLocked = true; // flag to keep track of whether the lock is currently locked or unlocked
    private bool canInteract = false; // flag to keep track of whether the player is close enough to interact with the lock
    private bool isInputtingCode = false; // flag to keep track of whether the player is currently inputting a code

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                feedbackText.text = "Please enter the correct code to unlock the lock.";
                codeInputField.gameObject.SetActive(true);
                isInputtingCode = true;
            }
        }

        if (isInputtingCode && Input.GetKeyDown(KeyCode.Escape))
        {
            codeInputField.gameObject.SetActive(false);
            isInputtingCode = false;
        }

        if (isInputtingCode && Input.GetKeyDown(KeyCode.Return))
        {
            CheckCode();
        }
    }

    public void CheckCode()
    {
        string inputCode = codeInputField.text;

        if (inputCode == code)
        {
            isLocked = false;
            feedbackText.text = "Code correct! The lock is now unlocked.";
            lockObject.SetActive(false); // unlock the object by deactivating its GameObject
        }
        else
        {
            feedbackText.text = "Incorrect code. Please try again.";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isLocked)
        {
            feedbackText.text = "Please enter the correct code to unlock the lock.";
        }
        if (other.gameObject.CompareTag("Player"))
        {
          canInteract = true;
        }
        else
        {
          canInteract = false;
        }
    }
}
