using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider is the target object.
        // If you want to be more specific, you can check tags or layers, like: other.CompareTag("SomeTag")
        if (other.gameObject.tag == "Player")
        {
            UnfreezeConstraints();
        }
    }

    private void UnfreezeConstraints()
    {
        rb.constraints = RigidbodyConstraints2D.None; // This unfreezes all constraints for a 2D Rigidbody.
    }
}
