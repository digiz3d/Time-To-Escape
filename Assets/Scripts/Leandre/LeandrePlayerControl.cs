using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandrePlayerControl : MonoBehaviour
{
    [SerializeField]
    private float runningSpeed = 15f;
    [SerializeField]
    private float slidingSpeed = 35f;

    [SerializeField]
    private float jumpForce = 25f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform spriteTransform;

    private bool isGrounded = false;
    public bool isOverSlide = false;
    public bool hasMagnet = false; 

    [SerializeField]
    private GameObject ghostPrefab;

    private Direction direction = Direction.none;
    private bool jump = false;
    private bool slide = false;

    private Rigidbody2D rb;
    private Collider2D col2d;

    private Animator anim;

    public float currentSpeedX = 0f;
    public float targetSpeed = 0f;

    [SerializeField]
    private float slideFallFactor;

    #region dash related fields
    [Header("Dash settings")]
    [SerializeField]
    private float dashingSpeed = 35f;
    [SerializeField]
    private float dashCooldown = 10f;
    [SerializeField]
    private float dashDuration = 1f;

    private bool dashing = false;
    private float currentDashDuration = 0f;
    private float currentDashCoolDown = 0f;
    #endregion

    [Header("Ripple effect")]
    [SerializeField]
    private GameObject rippleEffectPrefab;


    #region step effect
    private bool willStepEffect = false;
    [SerializeField]
    private GameObject stepPrefab = null;
    #endregion

    #region slide effect
    [SerializeField]
    private ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule psEmission;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        anim = spriteTransform.GetComponent<Animator>();
        anim.SetBool("IsSliding", false);
        psEmission = particleSystem.emission;
    }

    void Update()
    {
        anim.ResetTrigger("IsDashing");
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Ripples();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }

        isGrounded = Physics2D.IsTouchingLayers(col2d, groundLayer);

        if (willStepEffect && isGrounded)
        {
            Instantiate(stepPrefab, transform.position + Vector3.down * 0.8f, Quaternion.identity);
            willStepEffect = false;
        }

        if (rb.velocity.y > 20f)
        {
            willStepEffect = true;
        }

        // Check if the user is grounded to set the jump animation
        if (isGrounded == true)
        {
            anim.SetBool("IsJumping", false);
        }

        //Check if the user is not sliding anymore
        if (slide == false)
        {
            anim.SetBool("SlideActivated", false);
            anim.SetBool("IsSliding", false);
            psEmission.enabled = false;
        }


        // if the user wants to jump while touching the ground
        if (jump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        #region isOverSlide Detection
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 5, groundLayer);

        if (hit.collider != null)
        {
            float angle = Vector2.SignedAngle(Vector2.up, hit.normal);
            float angleAbs = Mathf.Abs(angle);
            // if the slide is between those numbers...
            if (15f < angleAbs && angleAbs < 75f)
            {
                // and is in the right direction :
                if (direction == Direction.right && angle < 0f)
                {
                    isOverSlide = true;
                }
                else if (direction == Direction.left && angle > 0f)
                {
                    isOverSlide = true;
                }
                else
                {
                    isOverSlide = false;
                }
            }
            else
            {
                isOverSlide = false;
            }
        }
        else
        {
            isOverSlide = false;
        }
        if (!isOverSlide)
        {
            slide = false;
        }
        #endregion

        // if the user wants to slide and can slide
        if (direction == Direction.none)
        {
            targetSpeed = 0f;
        }
        else if (slide && isOverSlide)
        {
            if (hit.collider != null)
            {
                // lets make the guy fall faster
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - Time.deltaTime * slideFallFactor);
                SetTargetSpeed(slidingSpeed);
                anim.SetBool("SlideActivated", true);
                anim.SetBool("IsSliding", true);
                psEmission.enabled = true;

            }
            else
            {
                slide = false;
                SetTargetSpeed(runningSpeed);
            }

        }
        else
        {
            slide = false;
            SetTargetSpeed(runningSpeed);
        }

        if (dashing && currentDashDuration <= dashDuration)
        {
            currentDashDuration += Time.deltaTime;
            rb.velocity = new Vector2(dashingSpeed, rb.velocity.y);
            anim.SetTrigger("IsDashing");
            currentDashCoolDown = dashCooldown;
            Instantiate(ghostPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        }
        else
        {
            currentDashCoolDown -= Time.deltaTime;
            dashing = false;
            currentSpeedX = Mathf.Lerp(currentSpeedX, targetSpeed, Time.deltaTime * 2f);
            rb.velocity = new Vector2(Mathf.Clamp(currentSpeedX, -slidingSpeed, slidingSpeed), rb.velocity.y);
        }
        jump = false;
    }

    public float GetCurrentSpeedX()
    {
        return currentSpeedX;
    }

    public void SetTargetSpeed(float speed)
    {
        switch (direction)
        {
            case Direction.right:
                targetSpeed = speed;
                break;


            case Direction.left:
                targetSpeed = -speed;

                break;

            default:
                targetSpeed = 0f;
                break;
        }
    }

    public void setSlidingSpeed(float newSpeed)
    {
        slidingSpeed = newSpeed;
    }

    public void setRunningSpeed(float newSpeed)
    {
        runningSpeed = newSpeed;
    }

    public float getSlidingSpeed()
    {
        return slidingSpeed;
    }

    public float getRunningSpeed()
    {
        return runningSpeed;
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public void GoRight()
    {
        anim.SetBool("IsRunning", true);
        if (direction == Direction.right)
        {
            Dash();
            return;
        }
        direction = Direction.right;
        spriteTransform.localRotation = Quaternion.Euler(spriteTransform.localRotation.eulerAngles.x, 0f, spriteTransform.localRotation.eulerAngles.z);
    }

    public void GoLeft()
    {
        // stops the player
        direction = Direction.none;
        // spriteTransform.localRotation = Quaternion.Euler(spriteTransform.localRotation.eulerAngles.x, 180f, spriteTransform.localRotation.eulerAngles.z);
    }

    public void Jump()
    {
        jump = true;
        slide = false;
        anim.SetBool("IsJumping", true);
    }

    public void Slide()
    {
        slide = true;
    }

    public void Dash()
    {
        if (currentDashCoolDown <= 0f)
        {
            dashing = true;
            currentDashDuration = 0f;
        }
    }

    public void Ripples()
    {
        Instantiate(rippleEffectPrefab, gameObject.transform);
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 5);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 5, groundLayer);

        if (hit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(hit.point, hit.point + hit.normal);

            //Debug.Log("2-angle = " + Vector2.Angle(Vector2.up, hit.normal));
            //Debug.Log("2-angle = " + Vector2.SignedAngle(Vector2.up, hit.normal));
        }
    }
    */
}

public enum Direction
{
    none,
    left,
    right
}