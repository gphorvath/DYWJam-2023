using System.Collections.Generic;
using UnityEngine;

public class CollisionToggler : MonoBehaviour
{
    [Header("Settings")]
    public bool useOnce = true;

    [Header("GameObjects to Enable on Collision")]
    public List<GameObject> objectsToEnable;

    [Header("GameObjects to Disable on Collision")]
    public List<GameObject> objectsToDisable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Enable game objects
            foreach (GameObject obj in objectsToEnable)
            {
                obj.SetActive(true);
            }

            // Disable game objects
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }

            if(useOnce)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
