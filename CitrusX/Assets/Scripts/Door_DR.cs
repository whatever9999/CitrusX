/*
 * Dominique
 * 
 * The door knows if it is unlocked or not and opens itself through an animation when told to Open()
 * 
 * Chase (Changes) 08/02/2020
 * 
 * Added a public bool for if the door requires a key
 */
 

using UnityEngine;

public class Door_DR : MonoBehaviour
{
    public bool unlocked;
    public bool requiresKey;

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
