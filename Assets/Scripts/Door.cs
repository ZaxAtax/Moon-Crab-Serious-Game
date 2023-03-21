using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public Key requiredKey;

    private Animator animator;
    private bool canInteract = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
      if (canInteract && Input.GetKeyDown(KeyCode.E))
      {
        Interact();
      }
    }

    public void Interact()
    {
      if (Inventory.Instance.HasKey(requiredKey) && !isLocked)
      {
        isLocked = true;
        animator.SetBool("isOpen", false);
      }
      else if (Inventory.Instance.HasKey(requiredKey) && isLocked)
      {
        isLocked = false;
        animator.SetBool("isOpen", true);
      }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        canInteract = true;
      }
    }

    void OnTriggerExit(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        canInteract = false;
      }
    }
}
