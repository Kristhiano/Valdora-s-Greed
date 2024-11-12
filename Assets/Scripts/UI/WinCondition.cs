using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinCondition : MonoBehaviour
{
    public GameObject winScreen;               // Objeto a activar cuando se gane el juego
    public EnemySpawner enemySpawner;          // Referencia al script EnemySpawner asignada desde el Inspector

    private bool victoryTriggered = false;     // Para asegurarnos de que solo se active una vez

    void Start()
    {
        // Asegurarse de que el objeto winScreen esté desactivado al inicio
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar la condición de victoria sólo cuando aún no se ha activado la victoria
        if (!victoryTriggered)
        {
            CheckWinCondition();
        }
    }

    private void CheckWinCondition()
    {
        // Verificar si no hay enemigos activos en ambas listas
        if (enemySpawner != null &&
            enemySpawner.activeEnemiesType1.Count == 0 &&
            enemySpawner.activeEnemiesType2.Count == 0)
        {
            TriggerWin();
        }
    }

    private void TriggerWin()
    {
        // Activar el GameObject de victoria y comenzar la corrutina de transición
        if (winScreen != null && !victoryTriggered)
        {
            victoryTriggered = true;  // Asegurarnos de que solo se active una vez
            winScreen.SetActive(true);
            StartCoroutine(LoadFirstSceneAfterDelay());
        }
    }

    private IEnumerator LoadFirstSceneAfterDelay()
    {
        // Esperar 2 segundos y luego cargar la primera escena
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
