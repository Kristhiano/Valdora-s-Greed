using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyType1Prefab;            // Prefab del enemigo tipo 1
    public GameObject enemyType2Prefab;            // Prefab del enemigo tipo 2
    public int maxEnemiesType1 = 5;                // Número máximo de enemigos tipo 1 a spawnear
    public int maxEnemiesType2 = 5;                // Número máximo de enemigos tipo 2 a spawnear
    public float minSpawnDistance = 2.0f;          // Distancia mínima entre enemigos
    public float minSpawnDistanceFromPlayer = 3.0f; // Distancia mínima del jugador

    [Header("Spawn Zones")]
    public List<Transform> spawnZones;             // Zonas de aparición

    [HideInInspector] public List<GameObject> activeEnemiesType1 = new List<GameObject>(); // Lista de enemigos tipo 1 activos
    [HideInInspector] public List<GameObject> activeEnemiesType2 = new List<GameObject>(); // Lista de enemigos tipo 2 activos
    [HideInInspector] public int totalSpawnedType1 = 0;             // Contador de enemigos tipo 1 generados
    [HideInInspector] public int totalSpawnedType2 = 0;             // Contador de enemigos tipo 2 generados
    public Transform player;                         // Referencia al jugador

    [Header("UI Elements")]
    public TMP_Text enemiesRemainingText;           // Referencia al texto de enemigos restantes
    private int totalEnemiesRemaining;              // Total de enemigos restantes (sumando ambos tipos)
    private int totalEnemiesDefeated;               // Total de enemigos derrotados (suma de ambos tipos)

    void Start()
    {
        // Inicializar el total de enemigos restantes
        totalEnemiesRemaining = maxEnemiesType1 + maxEnemiesType2;

        // Inicializar el total de enemigos derrotados
        totalEnemiesDefeated = 0;

        // Inicializar el spawn de enemigos
        StartCoroutine(SpawnEnemies());
        UpdateEnemiesRemainingText();
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Limpiar enemigos nulos de las listas (para casos en que ya hayan sido destruidos)
            activeEnemiesType1.RemoveAll(enemy => enemy == null);
            activeEnemiesType2.RemoveAll(enemy => enemy == null);

            // Verificar si se puede spawnear enemigos tipo 1
            if (totalSpawnedType1 < maxEnemiesType1 && activeEnemiesType1.Count < maxEnemiesType1)
            {
                TrySpawnEnemy(enemyType1Prefab, activeEnemiesType1, ref totalSpawnedType1);
            }

            // Verificar si se puede spawnear enemigos tipo 2
            if (totalSpawnedType2 < maxEnemiesType2 && activeEnemiesType2.Count < maxEnemiesType2)
            {
                TrySpawnEnemy(enemyType2Prefab, activeEnemiesType2, ref totalSpawnedType2);
            }

            // Actualizar el texto de enemigos restantes
            UpdateEnemiesRemainingText();

            yield return new WaitForSeconds(2f); // Tiempo de espera entre intentos de spawn
        }
    }

    private void TrySpawnEnemy(GameObject enemyPrefab, List<GameObject> enemyList, ref int totalSpawned)
    {
        // Seleccionar una zona de spawn aleatoria
        int randomIndex = Random.Range(0, spawnZones.Count);
        Transform spawnPoint = spawnZones[randomIndex];

        // Verificar que la posición esté a la distancia mínima de otros enemigos y del jugador
        if (IsPositionValid(spawnPoint.position, minSpawnDistance, enemyList) &&
            IsPositionValidFromPlayer(spawnPoint.position, minSpawnDistanceFromPlayer))
        {
            // Generar el enemigo
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemyList.Add(newEnemy);
            totalSpawned++; // Incrementar el contador de enemigos generados
        }
    }

    private bool IsPositionValid(Vector3 position, float minDistance, List<GameObject> enemyList)
    {
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null && Vector3.Distance(position, enemy.transform.position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsPositionValidFromPlayer(Vector3 position, float minDistance)
    {
        if (player != null && Vector3.Distance(position, player.position) < minDistance)
        {
            return false;
        }
        return true;
    }

    private void UpdateEnemiesRemainingText()
    {
        // Calcular los enemigos restantes
        int remainingEnemies = totalEnemiesRemaining - totalEnemiesDefeated;
        enemiesRemainingText.text = "Enemigos restantes: " + remainingEnemies;
    }

    // Método para restar un enemigo derrotado (llámalo desde el script del enemigo)
    public void EnemyDefeated()
    {
        totalEnemiesDefeated++;
        UpdateEnemiesRemainingText();
    }
}
