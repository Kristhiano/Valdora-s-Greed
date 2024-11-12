using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 4f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            EnemyController asechador = collision.GetComponent<EnemyController>();
            SlumoController slumo = collision.GetComponent<SlumoController>();
            DummieController dummie = collision.GetComponent<DummieController>();

            if (asechador != null)
            {
                asechador.TakeDamage(damage);
            }
            if (slumo != null)
            {
                slumo.TakeDamage(damage);
            }

            Destroy(gameObject);
            
            if (dummie != null)
            {
                dummie.TakeDamage(damage);
            }
        }

        else if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

