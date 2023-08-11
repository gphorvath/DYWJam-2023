using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Star : MonoBehaviour
{
    public float minWaitTime = 1.0f;  // Minimum time before falling
    public float maxWaitTime = 5.0f;  // Maximum time before falling

    public float bounceForce = 10.0f; // Adjust this to control bounce strength

    public float minGravityScale = 0.5f; // Minimum random gravity scale
    public float maxGravityScale = 2.0f; // Maximum random gravity scale

    private Rigidbody2D rb;
    private bool isFalling = false;
    private bool countdownStarted = false;
    private bool hasTouchedGround = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;  // Initially, disable gravity so the object doesn't fall
    }

    private void Update()
    {
        // Check if the object has touched the ground and stopped moving
        if (hasTouchedGround && rb.velocity.magnitude < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!countdownStarted && collision.gameObject.name == "TriggerStarfall")
        {
            // Set a random time before the object starts falling
            Invoke("StartFalling", Random.Range(minWaitTime, maxWaitTime));
            countdownStarted = true;
        }
    }

    private void StartFalling()
    {
        rb.gravityScale = Random.Range(minGravityScale, maxGravityScale);  // Randomize gravity scale
        isFalling = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            isFalling = false;
            hasTouchedGround = true;
        }
    }
}
