using UnityEngine;

public class DummieController : MonoBehaviour
{
    public float vida = 100f;
    [HideInInspector ]public bool isDead = false;
    public void TakeDamage(float damage)
    {
        vida -= damage;

        if (vida <= 0)
        {
            isDead = true;
        }
    }
}
