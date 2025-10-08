using System.Collections;

using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;



public class Control_Bola : MonoBehaviour

{


    public Rigidbody rb;



    //Variables para apuntar

    public float velocidadDeApuntado = 5f;

    public float limiteIzquierdo = -2F;

    public float limiteDerecho = 2f;







    public float fuerzaDeLanzamiento = 1000f;



    private bool haSidoLanzada = false;

    // Start is called before the first frame update

    void Start()

    {



    }



    // Update is called once per frame

    void Update()

    {// Expresion: mientras que haSidoLanzada sea falso puedes disparar

        if (haSidoLanzada == false)

        {

            Apuntar();

            if (Input.GetKeyDown(KeyCode.Space))

            {

                Lanzar();

            }

        }

    }



    void Apuntar()

    {

        /* 1. Leer un inputHorizontal de tipo Axis, te permite resgitar

         entradas con la teclas A y D; y Flecha Izq y Flecha Derecha*/

        float inputHorizontal = Input.GetAxis("Horizontal");




    // 2.mover la bola hacia los lados /

    transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);




    // 3, Delimitar el movimiento de la bola/

    Vector3 posicionActual = transform.position;

        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);



        transform.position = posicionActual;

    }



    void Lanzar()
    {
        haSidoLanzada = true;
        // Aplicamos la fuerza de forma inmediata para el lanzamiento
        rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);
        // Ya no se usa SetParent aquí. El script de la cámara se encargará.
    }

}// Bienvenido a la entrada al infierno
