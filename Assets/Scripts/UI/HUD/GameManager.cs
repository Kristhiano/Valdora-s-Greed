using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider greedBar;
    [SerializeField] private float greedBarMaxValue = 100f;
    private float currentGreed;
    [SerializeField] private float greedDecreaseRate = 1f;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject debuffIndicator;
    [SerializeField] private GameObject dashDisableIndicator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentGreed = greedBarMaxValue;
        UpdateGreedBar();
        debuffIndicator.SetActive(false);
        dashDisableIndicator.SetActive(false);
    }

    private void Update()
    {
 
        currentGreed -= greedDecreaseRate * Time.deltaTime;
        currentGreed = Mathf.Clamp(currentGreed, 0, greedBarMaxValue);
        UpdateGreedBar();

        ApplyDebuffs();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        RechargeGreedBar(value);
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString("0000");
    }

    private void RechargeGreedBar(int value)
    {
        float rechargeAmount = 0f;

        switch (value)
        {
            case 10: // Moneda de oro
                rechargeAmount = 10f;
                break;
            case 100: // Esmeralda
                rechargeAmount = 20f;
                break;
            case 500: // Rubí
                rechargeAmount = 30f;
                break;
        }

        currentGreed += rechargeAmount;
        currentGreed = Mathf.Clamp(currentGreed, 0, greedBarMaxValue);
        UpdateGreedBar();
    }

    private void UpdateGreedBar()
    {
        greedBar.value = currentGreed / greedBarMaxValue;
    }

    private void ApplyDebuffs()
    {
        if (currentGreed <= 0)
        {
            // Debuff severo
            playerMovement.speed = 4; 
            playerController.damageMultiplier = 2;
            playerMovement.canDash = false;
            dashDisableIndicator.SetActive(true);
        }
        else if (currentGreed <= 50)
        {
            playerMovement.speed = 6;
            playerController.damageMultiplier = 1.5f;
            debuffIndicator.SetActive(true);
        }
        else
        {
            // Resetear debuffs
            playerMovement.speed = playerMovement.defaultSpeed;
            playerController.damageMultiplier = 1;
            playerMovement.canDash = true; 
            debuffIndicator.SetActive(false); 
            dashDisableIndicator.SetActive(false);
        }
    }
}
