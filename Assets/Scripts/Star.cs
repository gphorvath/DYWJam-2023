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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;  // Initially, disable gravity so the object doesn't fall
        Invoke("StartFalling", Random.Range(minWaitTime, maxWaitTime));
    }

    public void StartFalling()
    {
        rb.gravityScale = Random.Range(minGravityScale, maxGravityScale);  // Randomize gravity scale
        isFalling = true;
    }

        // New method to stop the object from falling
    public void StopFalling()
    {   
        CancelInvoke("StartFalling");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.gravityScale = 0;
        isFalling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            isFalling = false;
        }
    }
}
