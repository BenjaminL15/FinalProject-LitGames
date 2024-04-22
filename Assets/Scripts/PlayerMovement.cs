using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator move;
    private float xDir = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;

    private enum MovementState { idle, running, jumping, falling }
    
    
    // Start is called before the first frame update
    void Start()
    {
       rb =  GetComponent<Rigidbody2D>();
       sprite = GetComponent<SpriteRenderer>();
       move = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        // GetAxis will go back to zero gradually
        // GetAxisRaw will go back to zero immediately 

        xDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);



        if (Input.GetButtonDown("Jump"))
        {
            //Vector 3 is a data holder for three values (x,y,z)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState() 
    {
        MovementState state;
        if (xDir > 0f) 
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (xDir < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        move.SetInteger("state", (int)state);
    }
}
