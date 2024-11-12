using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlumoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    public float shootingRange = 5f;

    private Transform player;
    private Animator animator;

    private bool isShooting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime && !isShooting)
            {
                isShooting = true;
                animator.SetTrigger("Shooting");
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    public void ShootBullet()
    {
        if (player == null || !isShooting) return;

        Vector2 direction = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        isShooting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
