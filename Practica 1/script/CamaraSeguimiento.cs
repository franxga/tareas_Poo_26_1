using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamaraSeguimiento : MonoBehaviour
{
    // El objetivo que la cámara seguirá (la bola de boliche)
    public Transform objetivo;

    // Distancia deseada de la cámara al objetivo
    public Vector3 offset;

    // Velocidad con la que la cámara se mueve hacia la posición deseada
    public float velocidadSuavizado = 0.125f;

    void Start()
    {
        // Si no se asigna un offset, usamos una posición inicial razonable
        if (offset == Vector3.zero)
        {
            // Ajusta estos valores si la cámara está demasiado cerca o lejos
            offset = new Vector3(0f, 1.5f, -2.5f);
        }
    }

    /* * LateUpdate se llama después de que todos los Updates han sido llamados. 
     * Esto asegura que la cámara sigue al objetivo DESPUÉS de que el objetivo
     * se ha movido en el frame actual, dando un seguimiento suave.
    */
    void LateUpdate()
    {
        // Salimos si no hay objetivo asignado
        if (objetivo == null)
        {
            return;
        }

        // 1. Calcular la posición deseada de la cámara (posición del objetivo + offset)
        Vector3 posicionDeseada = objetivo.position + offset;

        /* 2. Suavizar el movimiento (Lerp)
         * Mueve la posición actual de la cámara hacia la posición deseada
         * de manera gradual, usando la velocidad de suavizado.
        */
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, velocidadSuavizado);

        // 3. Aplicar la nueva posición
        transform.position = posicionSuavizada;

        // 4. Asegurar que la cámara siempre mire al objetivo
        transform.LookAt(objetivo);
    }
}