using UnityEngine;

public class FreezeStarfall : MonoBehaviour
{
    public StarfallSpawner starfallSpawner; // Reference to the StarfallSpawner script
    public GameObject door; // Reference to the door object
    public bool oneTimeUse = true;

    private bool onCooldown = false; // Flag to check if the script is on cooldown
    private float cooldownDuration = 3f; // Duration for which the script should stay on cooldown
    private bool starsAreFrozen = false; // Flag to keep track of the starfall's state

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Assuming your player has the tag "Player", adjust if necessary
        if (!onCooldown && collision.CompareTag("Player"))
        {
            Freeze();

            DeactivateDoor();

            if(oneTimeUse)
            {
                this.gameObject.SetActive(false);
                return;
            }

            // Activate cooldown
            onCooldown = true;
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void Freeze()
    {
        if (starfallSpawner)
        {
            starfallSpawner.ToggleSpawning(!starsAreFrozen);
            starsAreFrozen = !starsAreFrozen;
        }
    }

    private void DeactivateDoor()
    {
        if(door)
        {
            door.SetActive(false);
        }
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }
}
