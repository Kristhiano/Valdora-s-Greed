using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public List<string> dialogueLines;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI continueText;
    public ScreenFader screenFader;

    public float typingSpeed = 0.05f;

    private int currentLineIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        ShowNextLine();
        continueText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                CompleteTyping();
            }
            else
            {
                ShowNextLine();
            }
        }
    }

    private void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Count)
        {
            continueText.gameObject.SetActive(false);
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            StartCoroutine(EndDialogueAndFade());
        }
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        continueText.gameObject.SetActive(true);
    }

    private void CompleteTyping()
    {
        StopAllCoroutines();
        dialogueText.text = dialogueLines[currentLineIndex - 1];
        isTyping = false;
        continueText.gameObject.SetActive(true);
    }

    private IEnumerator EndDialogueAndFade()
    {
        continueText.gameObject.SetActive(false);
        yield return screenFader.FadeToBlack();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
