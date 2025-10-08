using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamaraSeguimiento : MonoBehaviour
{
    // El objetivo que la c�mara seguir� (la bola de boliche)
    public Transform objetivo;

    // Distancia deseada de la c�mara al objetivo
    public Vector3 offset;

    // Velocidad con la que la c�mara se mueve hacia la posici�n deseada
    public float velocidadSuavizado = 0.125f;

    void Start()
    {
        // Si no se asigna un offset, usamos una posici�n inicial razonable
        if (offset == Vector3.zero)
        {
            // Ajusta estos valores si la c�mara est� demasiado cerca o lejos
            offset = new Vector3(0f, 1.5f, -2.5f);
        }
    }

    /* * LateUpdate se llama despu�s de que todos los Updates han sido llamados. 
     * Esto asegura que la c�mara sigue al objetivo DESPU�S de que el objetivo
     * se ha movido en el frame actual, dando un seguimiento suave.
    */
    void LateUpdate()
    {
        // Salimos si no hay objetivo asignado
        if (objetivo == null)
        {
            return;
        }

        // 1. Calcular la posici�n deseada de la c�mara (posici�n del objetivo + offset)
        Vector3 posicionDeseada = objetivo.position + offset;

        /* 2. Suavizar el movimiento (Lerp)
         * Mueve la posici�n actual de la c�mara hacia la posici�n deseada
         * de manera gradual, usando la velocidad de suavizado.
        */
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado);

        // 3. Aplicar la nueva posici�n
        transform.position = posicionSuavizada;

        // 4. Asegurar que la c�mara siempre mire al objetivo
        transform.LookAt(objetivo);
    }
}