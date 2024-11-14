using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceContinue : MonoBehaviour
{
    [SerializeField] private GameObject opciones;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            opciones.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
