using UnityEngine;

public class Door_DR : MonoBehaviour
{
    private bool unlocked;

    private Animator animator;

    public bool GetUnlocked() { return unlocked; }
    public void SetUnlocked(bool newUnlocked) { unlocked = newUnlocked; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        animator.SetBool("Open", true);
    }
}
