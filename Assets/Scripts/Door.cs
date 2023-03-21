using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public Key requiredKey;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
      if (Inventory.Instance.HasKey(requiredKey))
      {
        isLocked = true;
        animator.SetBool("isOpen", false);
      }
    }
}
