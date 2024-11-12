using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Transform indicator; // Referencia al sprite indicador que rotará
    public float indicatorDistance = 1.5f; // Distancia del indicador del jugador
    private GameObject nearestEnemy; // Almacena al enemigo más cercano
    private Vector3 directionToNearestEnemy; // Dirección hacia el enemigo más cercano

    void Update()
    {
        FindNearestEnemy();
        PositionIndicator();
        RotateIndicator();
        HandleIndicatorVisibility();
    }

    void FindNearestEnemy()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Buscar el enemigo más cercano
        nearestEnemy = null; // Reiniciar la referencia al enemigo más cercano
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(player.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            directionToNearestEnemy = (nearestEnemy.transform.position - player.position).normalized;
        }
    }

    void PositionIndicator()
    {
        if (indicator != null && nearestEnemy != null)
        {
            // Colocar el indicador a una distancia fija del jugador
            Vector3 indicatorPosition = player.position + directionToNearestEnemy * indicatorDistance;
            indicator.position = indicatorPosition;
        }
    }

    void RotateIndicator()
    {
        if (indicator != null && nearestEnemy != null)
        {
            // Calcular el ángulo hacia el enemigo más cercano
            float angle = Mathf.Atan2(directionToNearestEnemy.y, directionToNearestEnemy.x) * Mathf.Rad2Deg;

            // Rotar el indicador para que apunte hacia el enemigo
            indicator.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Ajustar el ángulo por -90 grados
        }
    }

    void HandleIndicatorVisibility()
    {
        if (indicator != null)
        {
            // Activar el indicador si hay un enemigo, desactivarlo si no hay
            indicator.gameObject.SetActive(nearestEnemy != null);
        }
    }

    public Vector3 GetDirectionToNearestEnemy()
    {
        return directionToNearestEnemy;
    }
}
