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
        Debug.Log(currentHealth);
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
            Color color = spriteRenderer.color;
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
    }

    void Die()
    {
        Transform childToDisable = transform.Find("SlumoHealthBarCanvas");
        if (childToDisable != null)
        {
            childToDisable.gameObject.SetActive(false);
        }

        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        if (enemyCollider != null)
        {
            enemyCollider.isTrigger = true;
        }

        if(gameObject.GetComponent<SlumoDivide>())
        {
            gameObject.GetComponent<SlumoDivide>().Divide();
            StartCoroutine(FadeOutAndDestroy());
            Destroy(gameObject);
        }

        if (GetComponent<SlumoDivide>())
        {
            GetComponent<SlumoDivide>().Door();
        }

        gameObject.tag = "Untagged";

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
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return null; // Espera un frame antes de continuar
        }

        // Asegurarse de que el sprite sea completamente transparente
        spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0f);

        // Destruir el objeto
        Destroy(gameObject);
    }
}
