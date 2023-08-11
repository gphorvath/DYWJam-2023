using UnityEngine;

public class FreezeStarfall : MonoBehaviour
{
    public StarfallSpawner starfallSpawner; // Reference to the StarfallSpawner script

    private bool onCooldown = false; // Flag to check if the script is on cooldown
    private float cooldownDuration = 3f; // Duration for which the script should stay on cooldown

    private bool starsAreFrozen = false; // Flag to keep track of the starfall's state

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Assuming your player has the tag "Player", adjust if necessary
        if (!onCooldown && collision.CompareTag("Player"))
        {
            // Toggle the state of the stars and the spawning
            starsAreFrozen = !starsAreFrozen;
            if (starfallSpawner)
            {
                starfallSpawner.ToggleSpawning(starsAreFrozen); 
            }

            // Activate cooldown
            onCooldown = true;
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }
}
