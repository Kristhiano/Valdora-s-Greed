using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;

    private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanundar();
            }

            else
            {
                Pausa();
            }
        }
    }
    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    public void Reanundar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene(0);
    }
}
