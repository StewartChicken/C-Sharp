using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;

    private enum animationState { idle, running, jumping, falling}

    private Camera cam;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButton("Jump") && isGrounded()) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        updateAnimationState();
    }

    //Updates animation of sprite depending on velocity
    private void updateAnimationState()
    {

        animationState state;

        if (dirX > 0f)
        {
            sprite.flipX = false;
            state = animationState.running;
            
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
            state = animationState.running;
        }
        else
        {
            state = animationState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = animationState.idle;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = animationState.idle;
        }

        anim.SetInteger("state", (int)state);

        position = cam.ScreenToWorldPoint(Input.mousePosition);
     
        Vector3 rotation = position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ); 

        if(rotZ > 90 && rotZ <= 180 || rotZ >= -180 && rotZ < -90)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

}
