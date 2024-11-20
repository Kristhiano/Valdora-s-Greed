using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 30;
    [HideInInspector] public int currentHealth;
    private Animator animator;
    [HideInInspector] public bool isDead = false;

    [SerializeField] private PlayerHealthBar healthBar;
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public Color invulnerableColor = Color.white;
    public float damageDuration = 0.2f;

    [HideInInspector] public bool canMove = true;
    private bool isInvulnerable = false;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float shootCooldown = 0.5f;

    private bool hasShot = false;
    private bool bulletSpawned = false;

    [SerializeField] private ParticleSystem bloodFX;

    [HideInInspector] public float damageMultiplier = 1;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.InitHealthBar(currentHealth);
        canMove = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canMove && !hasShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //canMove = false;
        hasShot = true;
        bulletSpawned = false;
        animator.SetTrigger("Shoot");
        SpawnBullet();
        StartCoroutine(ResetMovementAfterShooting());
    }

    void SpawnBullet()
    {
        if (!bulletSpawned && bulletPrefab != null)
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            
            Vector3 direction = (mousePosition - transform.position).normalized;

            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }

            animator.SetFloat("ShootX", direction.x);
            animator.SetFloat("ShootY", direction.y);

            bulletSpawned = true; 
        }
    }

    IEnumerator ResetMovementAfterShooting()
    {
        yield return new WaitForSeconds(shootCooldown);
        canMove = true;
        hasShot = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isInvulnerable) return;

        currentHealth -= Mathf.RoundToInt(damage * damageMultiplier);
        Debug.Log("Jugador ha recibido daño");
        healthBar.UpdateCurrentHealth(currentHealth);

        StartCoroutine(FlashDamageColor());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashDamageColor()
    {
        bloodFX.Play();
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageDuration);
        spriteRenderer.color = Color.white;
    }

    public void EnableInvulnerability(float duration)
    {
        StartCoroutine(InvulnerabilityCoroutine(duration));
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        isInvulnerable = true;
        spriteRenderer.color = invulnerableColor;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
        spriteRenderer.color = Color.white;
    }

    void Die()
    {
        canMove = false;
        isDead = true;

        Debug.Log("Jugador ha muerto");

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }
}
