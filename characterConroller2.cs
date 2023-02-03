using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterConroller2 : MonoBehaviour
{
    public float jumpForce = 4.0f;
    public float speed = 1.0f;
    public float moveDirection;

    private bool jump;
    private bool grounded = true;
    private bool moving;
    private Rigidbody2D _rigidbody2D;
    private Animator anim;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();

    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(_rigidbody2D.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        _rigidbody2D.velocity = new Vector2(speed * moveDirection, _rigidbody2D.velocity.y);
        if(jump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            jump = false;
        }
    }

    private void Update()
    {
        if(grounded && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if(Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed", speed);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed", speed);
            }
        }
        else if (grounded)
        {
            moveDirection = .0f;
            anim.SetFloat("speed", .0f);
        }

        if(grounded && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", grounded);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("ground"))
        {
            grounded = true;
            anim.SetBool("grounded", grounded);
        }
    }
}
