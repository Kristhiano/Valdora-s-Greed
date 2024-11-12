using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectiveText : MonoBehaviour
{
    public TMP_Text objectiveText; // Referencia al componente TextMeshPro
    public float fadeDuration = 5f; // Duración del desvanecimiento

    private void Start()
    {
        // Iniciar la corrutina para desvanecer el texto
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        // Esperar 5 segundos antes de comenzar a desvanecer
        yield return new WaitForSeconds(4f);

        // Obtener el color actual del texto
        Color textColor = objectiveText.color;

        // Tiempo que ha pasado
        float elapsedTime = 0f;

        // Mientras no haya pasado la duración del desvanecimiento
        while (elapsedTime < fadeDuration)
        {
            // Calcular la nueva opacidad
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textColor.a = newAlpha;
            objectiveText.color = textColor;

            // Incrementar el tiempo transcurrido
            elapsedTime += Time.deltaTime;
            yield return null; // Esperar el siguiente frame
        }

        // Asegurarse de que el texto sea completamente transparente
        textColor.a = 0f;
        objectiveText.color = textColor;

        // Desactivar solo el componente TMP_Text
        objectiveText.gameObject.SetActive(false);
    }
}
