using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeLock : MonoBehaviour
{
    public string code; // the correct code to unlock the lock
    public GameObject lockObject; // the object that will be unlocked when the correct code is entered
    public InputField codeInputField; // the input field where the player enters the code
    public Text feedbackText; // a text field to display feedback to the player
    public Text interactText;

    private bool isLocked = true; // flag to keep track of whether the lock is currently locked or unlocked
    private bool canInteract = false; // flag to keep track of whether the player is close enough to interact with the lock
    private bool isInputtingCode = false; // flag to keep track of whether the player is currently inputting a code


    void Start()
    {
      codeInputField.gameObject.SetActive(false);
      codeInputField.interactable = false;
      feedbackText.enabled = false;
      interactText.enabled = false;
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                feedbackText.text = "Please enter the correct code to unlock the lock.";
                Cursor.lockState = CursorLockMode.None;
                codeInputField.gameObject.SetActive(true);
                Cursor.visible = true;
                codeInputField.interactable = true;
                isInputtingCode = true;
            }
        }

        if (isInputtingCode && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            codeInputField.interactable = false;
            isInputtingCode = false;
            codeInputField.gameObject.SetActive(false);
        }

        if (isInputtingCode && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CheckCode());
        }
    }

    public IEnumerator CheckCode()
    {
        string inputCode = codeInputField.text;

        if (inputCode == code)
        {
            codeInputField.interactable = false;
            isInputtingCode = false;
            isLocked = false;
            Cursor.visible = false;
            codeInputField.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            feedbackText.text = "Code correct! The lock is now unlocked.";
            
            yield return new WaitForSeconds(1f);

            feedbackText.enabled = false;
            interactText.enabled = false;
            lockObject.SetActive(false); // unlock the object by deactivating its GameObject
        }
        else
        {
            feedbackText.text = "Incorrect code. Please try again.";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          canInteract = true;
          interactText.enabled = true;
          feedbackText.enabled = true;
          interactText.text = "Press E to interact";
        }

    }

    void OnTriggerExit(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        canInteract = false;
        interactText.enabled = false;
        feedbackText.enabled = false;
        codeInputField.gameObject.SetActive(false);
      }
    }
}
