using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoseCondition : MonoBehaviour
{


    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene(0);
    }
}
