using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum CoinType { Coin, Emerald, Ruby } // Define tipos de monedas

    public CoinType coinType; // Campo para seleccionar el tipo de moneda en el Inspector
    private int value; // Valor de la moneda

    private void Start()
    {
        // Asigna un valor basado en el tipo de moneda
        switch (coinType)
        {
            case CoinType.Coin:
                value = 10;
                break;
            case CoinType.Emerald:
                value = 100;
                break;
            case CoinType.Ruby:
                value = 500;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Asegúrate de que el jugador tiene el tag "Player"
        {
            GameManager.Instance.AddScore(value); // Suma el valor de la moneda al puntaje
            Destroy(gameObject); // Destruir la moneda
        }
    }
}
