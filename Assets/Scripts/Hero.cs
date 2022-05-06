using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float speed = 3.0f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15.0f;

    private bool isGrounded = false;

    private Rigidbody2D heroRigidbody;
    private Animator heroAnim;
    private SpriteRenderer heroSprite;

    private void Awake()
    {
        heroRigidbody = GetComponent<Rigidbody2D>();
        heroAnim = GetComponent<Animator>();
        heroSprite = GetComponentInChildren<SpriteRenderer>();

    }

    private States State  
    {
        get { return (States)heroAnim.GetInteger("state"); }
        set { heroAnim.SetInteger("state", (int)value); }

    }

    private void FixedUpdate()  
    {
        CheckGround();
    }


    private void Update()
    {

        if (isGrounded)
        {
            State = States.idle;
        }

        if (Input.GetButton("Horizontal"))  
        {
            Run();

        }

        if (isGrounded && Input.GetButtonDown("Jump"))  
        {
            Jump();
        }
    }

    private void Run()
    {
        if (isGrounded) { State = States.run; }

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        heroSprite.flipX = dir.x < 0.0f;
    }

    private void Jump()  
    {
        
        heroRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    private void CheckGround()  
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded)
        {
            State = States.jump;
        }
    }

}

public enum States
{
    idle,
    jump,
    run
}
