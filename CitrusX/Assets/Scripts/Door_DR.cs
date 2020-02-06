using UnityEngine;

public class Door_DR : MonoBehaviour
{
    private bool unlocked;

    public bool GetUnlocked() { return unlocked; }
    public void SetUnlocked(bool newUnlocked) { unlocked = newUnlocked; }
}
