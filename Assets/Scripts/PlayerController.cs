using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // How fast the character moves
    public float jumpForce = 5f; // How high the character jumps
    private bool isJumping = false;
    private Rigidbody2D rb;
    private bool facingRight = true; // Variable to check the direction the player is facing

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        float move = Input.GetAxis("Horizontal");

        // Flip the character if it changes direction
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }

        // Move the character
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Make the character jump
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void Flip()
    {
        // Toggle the direction the player is facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
