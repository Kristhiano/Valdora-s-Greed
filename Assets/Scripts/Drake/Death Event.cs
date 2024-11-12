using System.Collections;
using UnityEngine;

public class DeathEvent : MonoBehaviour
{
    [SerializeField] private PlayerController playerController; // Referencia al controlador del jugador
    [SerializeField] private Canvas hudCanvas;               // Canvas del HUD
    [SerializeField] private Canvas menupausaCanvas;
    [SerializeField] private GameObject indicator;           // Indicador que se desactivará con el HUD
    [SerializeField] private Cinemachine.CinemachineVirtualCamera cinemachineCamera; // Cámara de Cinemachine
    [SerializeField] private GameObject defeatPanel;         // Panel de derrota que se mostrará al morir

    private void Start()
    {
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(false); // Asegurarse de que el panel de derrota esté desactivado al inicio
        }
    }

    private void Update()
    {
        // Iniciar la secuencia de muerte si el jugador está muerto
        if (playerController != null && playerController.isDead)
        {
            StartCoroutine(HandleDeathSequence());
        }
    }

    private IEnumerator HandleDeathSequence()
    {
        // Desactivar el HUD
        if (hudCanvas != null)
        {
            hudCanvas.gameObject.SetActive(false);
        }

        if (menupausaCanvas != null)
        {
            menupausaCanvas.gameObject.SetActive(false);
        }

        // Desactivar el indicador
        if (indicator != null)
        {
            indicator.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        if (cinemachineCamera != null)
        {
            cinemachineCamera.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(2f);
        if (defeatPanel != null)
        {
            defeatPanel.SetActive(true);
        }
    }
}
