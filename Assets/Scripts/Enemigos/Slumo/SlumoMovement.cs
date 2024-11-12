using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlumoMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb2d;
    private Transform target;
    private Animator animator; // Referencia al Animator
    [SerializeField] private float minFollowDistance;
    [SerializeField] private float maxFollowDistance;

    private bool isFacingRight = true;

    private SlumoController SlumoController;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Asignar el Animator
        SlumoController = GetComponent<SlumoController>();
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (SlumoController != null && SlumoController.isDead)
        {
            rb2d.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }

        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) >= maxFollowDistance)
            {
                rb2d.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
            else if (Vector2.Distance(transform.position, target.position) <= minFollowDistance)
            {
                rb2d.velocity = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
            else
            {
                Vector2 direction = target.position - transform.position;
                rb2d.velocity = direction.normalized * speed;

                animator.SetBool("isMoving", true);

                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);

                if (direction.x > 0 && !isFacingRight)
                {
                    Flip();
                }
                else if (direction.x < 0 && isFacingRight)
                {
                    Flip();
                }
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, minFollowDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxFollowDistance);
    }
}
