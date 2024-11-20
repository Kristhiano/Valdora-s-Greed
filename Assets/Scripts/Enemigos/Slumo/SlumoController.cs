using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlumoController : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageDuration = 0.2f;

    [HideInInspector][SerializeField] EnemyHealthBar healthBar;
    [HideInInspector] public bool isDead = false;
    private Collider2D enemyCollider;

    public float fadeDuration = 2f; // Duración para la desvanecimiento al morir

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Slumo ha recibido daño");

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
        Debug.Log("Slumo ha muerto");

        gameObject.tag = "Untagged";

        //FindObjectOfType<EnemySpawner>().EnemyDefeated();

        Transform childToDisable = transform.Find("SlumoHealthBarCanvas");
        if (childToDisable != null)
        {
            childToDisable.gameObject.SetActive(false);
        }

        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Debug.Log("Trigger de muerte activado");
        }
        else
        {
            Debug.LogError("Animator no encontrado en el objeto.");
        }

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
            yield return null; // Espera un frame antes de continuar
        }

        // Asegurarse de que el sprite sea completamente transparente
        spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0f);

        // Destruir el objeto
        Destroy(gameObject);
    }
}
