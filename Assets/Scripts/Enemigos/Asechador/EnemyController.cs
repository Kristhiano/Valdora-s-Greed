using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int damage = 10;
    public int maxHealth = 1;
    private int currentHealth;

    private Transform target;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime;

    public Color damageColor = Color.red;
    public float damageDuration = 0.2f;
    public float attackRange = 1f;
    private bool hasDealtDamage = false;

    public float fadeDuration = 2f; // Duración para el desvanecimiento gradual
    [HideInInspector] public bool isDead = false;

    [SerializeField] private EnemyHealthBar healthBar;
    private Collider2D enemyCollider;

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        hasDealtDamage = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth > 0)
        {
            StartCoroutine(FlashDamageColor());
        }
        else
        {
            Die();
        }
    }

    private IEnumerator FlashDamageColor()
    {
        if (!isDead)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
    }

    void Die()
    {
        gameObject.tag = "Untagged";
        spriteRenderer.color = Color.white;

        Transform childToDisable = transform.Find("AsechadorHealthBarCanvas");
        if (childToDisable != null)
        {
            childToDisable.gameObject.SetActive(false);
        }

        isDead = true;
        animator.SetTrigger("Die");

        if (enemyCollider != null)
        {
            enemyCollider.isTrigger = true;
        }

        ItemDropManager itemDropManager = FindObjectOfType<ItemDropManager>();
        if (itemDropManager != null)
        {
            itemDropManager.DropItems(transform.position);
        }

        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        Color spriteColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(spriteColor.a, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            yield return null; 
        }

        spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0f);
        Destroy(gameObject);
    }

    void Update()
    {
        if (isDead || target == null || !IsPlayerAlive()) return;

        if (Vector2.Distance(transform.position, target.position) < attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Vector2 direction = target.position - transform.position;
        direction.Normalize();

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    public void DealDamage()
    {
        if (!hasDealtDamage && Vector2.Distance(transform.position, target.position) < attackRange)
        {
            hasDealtDamage = true;
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    public void ResetDamageFlag()
    {
        hasDealtDamage = false;
    }

    bool IsPlayerAlive()
    {
        PlayerController player = target.GetComponent<PlayerController>();
        return player != null && player.currentHealth > 0;
    }
}
