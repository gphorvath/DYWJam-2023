using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // How fast the character moves
    public float jumpForce = 5f; // How high the character jumps
    private bool isJumping = false;
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        float move = Input.GetAxis("Horizontal");

        // Move the character
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Make the character jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the character is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
