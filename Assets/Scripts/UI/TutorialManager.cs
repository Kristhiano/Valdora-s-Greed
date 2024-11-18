using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement playerMovement;
    private PlayerController playerController;
    private Animator ancianoTutoAnimator;
    public DummieController dummieController;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI InstructionsText;
    public GameObject drakeTuto;
    public GameObject ancianoTuto;
    public GameObject dummie;
    public GameObject anciano;
    public GameObject player;
    public ScreenFader screenFader;
    public Vector3 playerStartPosition;
    public PlayableDirector playableDirector;
    public float typingSpeed = 0.02f;
    public CinemachineVirtualCamera gameplayCamera;
    public CinemachineVirtualCamera virtualCamera;

    public float saltar = 1;
    private float tsaltar = 0;

    private Queue<string> sentences = new Queue<string>();
    private bool isTyping = false;
    private string currentSentence = "";
    private bool dialogueEnded = false;
    private Coroutine typingCoroutine;
    private Vector3 initialPlayerPosition;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerController = player.GetComponent<PlayerController>();
        ancianoTutoAnimator = anciano.GetComponent<Animator>();
        initialPlayerPosition = player.transform.position;
 
        StartCoroutine(TutorialSequence());
        InstructionsText.gameObject.SetActive(false);
    }

    private IEnumerator TutorialSequence()
    {
        yield return StartCoroutine(StartIntroCinematic());

        SetDialogueLines(new List<string>
        {
            "¿Qué es lo que veo? ¿Otra alma imprudente que se deja seducir por esta boca de sombras? Da media vuelta, joven, antes de que sea demasiado tarde. Este lugar no perdona.",
            "Déjame adivinar... has venido en busca de las riquezas que, dicen, yacen ocultas en las entrañas de esta mazmorra. Ah, los rumores corren más rápido que el buen juicio.",
            "Por la expresión en tu rostro, está claro que tu decisión está tomada. Y aunque sé que ignorarás mis advertencias, permíteme ofrecerte algunos consejos para que no caigas al primer paso que des en la oscuridad.",
            "Veamos qué tan firmes son esas piernas cuando se enfrenten a las sombras que acechan en la oscuridad...",
        });

        yield return StartCoroutine(ShowDialogue());

        yield return screenFader.FadeToBlack();

        Cambio(false);
        player.transform.position = playerStartPosition;

        yield return screenFader.FadeToClear();

        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Usa <color=red>" + "W, A, S, D" + "</color> para moverte.";
        yield return new WaitUntil(() => PlayerHasMoved());
        yield return new WaitForSeconds(3);
        InstructionsText.gameObject.SetActive(false);

        yield return screenFader.FadeToBlack();

        Cambio(true);
        yield return screenFader.FadeToClear();

        SetDialogueLines(new List<string>
        {
            "Correr no te salvará siempre; habrá momentos en los que las sombras te arrinconen sin piedad... Deberás aprender a moverte con astucia y escabullirte entre las grietas de la oscuridad.",
        });
        yield return StartCoroutine(ShowDialogue());

        yield return screenFader.FadeToBlack();

        Cambio(false);
        player.transform.position = playerStartPosition;

        yield return screenFader.FadeToClear();
        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Pulsa Espacio mientras te mueves para realizar un dodge. Deberás esperar unos segundos antes de poder usarlo nuevamente";

        yield return new WaitUntil(() => PlayerHasDashed());
        yield return new WaitForSeconds(3);
        InstructionsText.gameObject.SetActive(false);

        yield return screenFader.FadeToBlack();

        Cambio(true);
        yield return screenFader.FadeToClear();

        SetDialogueLines(new List<string>
        {
            "Y ahora, lo más crucial: tu defensa.",
            "...",
            "Espera... ¿acaso no traes armas contigo?",
            "*Suspira profundamente*... Ah, la imprudencia de los jóvenes de hoy. Muy bien, toma esta vieja ballesta; te será imprescindible si pretendes salir con vida de ese abismo.",
            "Ahora, intenta practicar. Dispara a ese muñeco que está allí; es un buen objetivo para probar tu puntería.",
            "Aunque, considerando tu falta de experiencia, dudo que puedas moverte y disparar al mismo tiempo...",

        });
        yield return StartCoroutine(ShowDialogue());

        yield return screenFader.FadeToBlack();

        Cambio(false);
        dummie.SetActive(true);
        player.transform.position = playerStartPosition;

        yield return screenFader.FadeToClear();
        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Usa el mouse para apuntar y dispara con Click Izquierdo. Ten en cuenta que no podrás moverte mientras disparas.";

        yield return new WaitUntil(() => PlayerHasAttacked());
        dummie.SetActive(false);
        yield return new WaitForSeconds(2);
        InstructionsText.gameObject.SetActive(false);

        yield return screenFader.FadeToBlack();

        Cambio(true);
        yield return screenFader.FadeToClear();
        SetDialogueLines(new List<string>
        {
            "Bien, ya has aprendido lo básico... al menos por ahora.",
            "Conozco a tu clase; no eres el primero, y no serás el último. Si pasas demasiado tiempo sin recolectar tesoros, sentirás cómo la codicia que habita en ti comenzará a desesperarte, y la ansiedad invadirá cada rincón de tu ser. Ten cuidado, porque esos efectos pueden ser fatales.",
            "Así que asegúrate de llenar bien tus bolsillos con tesoros; los encontrarás esparcidos por el suelo o arrebatándoselos a las criaturas que acechan en la mazmorra.",
            "Toma esta brújula; su aguja apunta siempre hacia la criatura más cercana... aunque, ten cuidado, no todos los encuentros serán bienvenidos.",
            "¡Y mira muy bien por donde caminas! Se dice que en las profundidades de la mazmorra reside un ser sumamente peligroso. Avanza con precaución, o podrías encontrar tu final.",
            "No tengo más consejos que ofrecerte, así que te deseo buena suerte... y espero que logres salir con vida de esta oscuridad.",
        });
        yield return StartCoroutine(ShowDialogue());

        yield return StartCoroutine(Level());
    }

    private void Cambio(bool c)
    {
        virtualCamera.gameObject.SetActive(c);
        drakeTuto.SetActive(c);
        ancianoTuto.SetActive(c);
        anciano.SetActive(!c);
        player.SetActive(!c);
        gameplayCamera.gameObject.SetActive(!c);
    }

    private IEnumerator StartIntroCinematic()
    {
        playableDirector.Play(); 
        yield return new WaitWhile(() => playableDirector.state == PlayState.Playing);
    }

    private void SetDialogueLines(List<string> lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
    }

    private IEnumerator ShowDialogue()
    {
        dialogueEnded = false;
        dialoguePanel.SetActive(true);
        DisplayNextSentence();

        yield return new WaitUntil(() => dialogueEnded);
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();

        if (ancianoTutoAnimator != null)
        {
            ancianoTutoAnimator.SetBool("isTalking", true);
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueEnded = true;

        if (ancianoTutoAnimator != null)
        {
            ancianoTutoAnimator.SetBool("isTalking", false);
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private bool PlayerHasMoved()
    {
        playerMovement.canDash = false;
        playerController.enabled = false;
        float movementThreshold = 9f; 
        float distanceMoved = Vector3.Distance(initialPlayerPosition, player.transform.position);
        return distanceMoved >= movementThreshold;
    }


    private bool PlayerHasDashed()
    {
        playerMovement.canDash = true;
        playerController.enabled = false;

        if (playerMovement == null)
        {
            return false; 
        }

        return playerMovement.isDashing; 
    }


    private bool PlayerHasAttacked()
    {
        playerController.enabled = true;
        if (dummieController != null && dummieController.isDead)
        {
            return true; 
        }
        return false;
    }

    private IEnumerator Level()
    {
        yield return screenFader.FadeToBlack();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentSentence;
                isTyping = false; 
            }
            else
            {
                if (sentences.Count > 0) 
                {
                    DisplayNextSentence();
                }
                else
                {
                    EndDialogue();
                }
            }
        }

        if(Input.GetKey(KeyCode.E))
        {
            tsaltar += Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
            tsaltar = 0;
        }
        if(tsaltar > saltar)
        {
            StartCoroutine(Level());
            return;
        }
    }

}
