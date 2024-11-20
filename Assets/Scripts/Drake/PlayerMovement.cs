using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0.5f;
    public float defaultSpeed = 8f;
    public float dashForce = 10f;
    public float dashCooldown = 3f;
    public float dashDuration = 0.1f;

    private Rigidbody2D rb;
    private Vector2 input;
    private Animator anim;
    private Vector2 lastMoveDirection;
    private PlayerController playerController;
    [HideInInspector] public bool isDashing;
    [HideInInspector] public float lastDashTime;
    private Collider2D playerCollider;

    [HideInInspector] public bool canDash = true;

    [SerializeField] private ParticleSystem dustFX;
    [SerializeField] private ParticleSystem sweatFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        playerCollider = GetComponent<Collider2D>();

        lastDashTime = -dashCooldown;
    }


    private void Update()
    {
        ProcessInputs();
        Debug.Log(input.magnitude);
        Animate();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDashTime + dashCooldown && input.magnitude > 0 && isDashing == false && canDash)
        {
            StartCoroutine(DashCoroutine());
        }

        if (Time.time >= lastDashTime + 0.6f && Time.time < lastDashTime + dashCooldown)
        {
            if (!sweatFX.isPlaying)
            {
                sweatFX.Play();
            }
        }
        else
        {
            if (sweatFX.isPlaying)
            {
                sweatFX.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            if (playerController.canMove)
            {
                rb.velocity = input * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = moveX;
        input.y = moveY;
        input.Normalize();
    }

    void Animate()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
        anim.SetBool("isDashing", isDashing);
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;
        lastDashTime = Time.time;
        dustFX.Play();

        playerController.EnableInvulnerability(dashDuration);

        gameObject.layer = LayerMask.NameToLayer("IgnoreEnemies");

        Vector2 dashDirection = input.magnitude > 0 ? input : lastMoveDirection;
        rb.velocity = dashDirection * dashForce;

        yield return new WaitForSeconds(dashDuration);

        gameObject.layer = LayerMask.NameToLayer("Default");

        rb.velocity = Vector2.zero;
        isDashing = false;
    }
}