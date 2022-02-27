using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    Rigidbody2D rb;
    public SpriteRenderer spriteFlip;
    Animator animator;
    GameObject grabDetector;
    Transform grabDetectorTransformComponent;

   [Header("Grounder")]
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    
    [Header("Movement")] 
    [SerializeField] public float speed = 0;
    [SerializeField] public float jumpForce = 0;
    public float fallMultiplier = 2.5f; //public in this instance is another way of doing [SerializeField]
    public float lowJumpMultiplier = 2f;
    public float rememberGroundedFor = 0.1f;
    float lastTimeGrounded;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteFlip = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        grabDetector = GameObject.Find("Grab Detector");
        grabDetectorTransformComponent = grabDetector.transform;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimateWalking();
        FlipSprite();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }
    
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    private void AnimateWalking()
    {
        if (Input.GetKey(KeyCode.A)) //|| Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Walking Animation", true);
        }
        else if (Input.GetKey(KeyCode.D))  //|| Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Walking Animation", true);
        }
        else 
        {
            animator.SetBool("Walking Animation", false);
        }
    }

    private void FlipSprite()
    {
        if(Input.GetKeyDown(KeyCode.A)) //|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(spriteFlip != null)
            {
                //spriteFlip.flipX = true;
                //Vector3 myVector = new Vector3(transform.position.x - 0.4f, transform.position.y);
                //Quaternion myRotation = grabDetectorTransformComponent.transform.rotation;
                transform.localScale = new Vector3(-3, 3, 3);
            }
        }
        if(Input.GetKeyDown(KeyCode.D)) //|| Input.GetKeyDown(KeyCode.RightArrow))
        {
            //spriteFlip.flipX = false;
            transform.localScale = new Vector3(3, 3, 3);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

}
