using UnityEngine;

public class FreezeStarfall : MonoBehaviour
{
    public StarfallSpawner starfallSpawner;
    public GameObject door;
    public bool oneTimeUse = true;

    public AudioSource audioSource;
    public AudioClip soundEffect;

    private bool onCooldown = false;
    private float cooldownDuration = 3f;
    private bool starsAreFrozen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onCooldown && collision.CompareTag("Player"))
        {
            PlaySoundEffect();

            Freeze();

            DeactivateDoor();

            if (oneTimeUse)
            {
                this.gameObject.SetActive(false);
                return;
            }

            onCooldown = true;
            Invoke("ResetCooldown", cooldownDuration);
        }
    }

    private void PlaySoundEffect()
    {
        if (audioSource && soundEffect)
        {
            audioSource.clip = soundEffect;
            audioSource.Play();
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
        if (door)
        {
            door.SetActive(false);
        }
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }
}
