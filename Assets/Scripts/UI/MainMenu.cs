using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nombreObjetoActivo;   // Nombre del objeto hijo que quieres activar
    public string nombreObjetoInactivo; // Nombre del objeto hijo que quieres desactivar
    private bool teclaPresionada = false; // Variable para verificar si se presionó una tecla

    void Update()
    {
        if (!teclaPresionada && Input.GetKeyDown(KeyCode.Space)) // Verifica si no se ha presionado una tecla y se presiona alguna
        {
            ActivarObjeto();
            teclaPresionada = true; // Cambia el estado para no volver a activar
        }
    }

    private void ActivarObjeto()
    {
        // Busca los objetos hijos en el Canvas usando GetComponentsInChildren
        Transform[] objetosHijos = GetComponentsInChildren<Transform>(true); // true incluye los inactivos

        // Activar y desactivar los objetos hijos
        foreach (Transform objeto in objetosHijos)
        {
            if (objeto.name == nombreObjetoActivo)
                objeto.gameObject.SetActive(true); // Activa el objeto

            if (objeto.name == nombreObjetoInactivo)
                objeto.gameObject.SetActive(false); // Desactiva el objeto
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }

    public void Quit()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}
