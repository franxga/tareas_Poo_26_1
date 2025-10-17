using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Jugador : MonoBehaviour
{
    // Movimiento
    public float velocidad = 5f; //velocidad de movimiento
    public float gravedad = -9.8f; // velocidad de caida 

    private CharacterController controller; // pieza de lego que nos permite el movimiento del juego
    private Vector3 velocidadVertical; // nos va permitir saber que tan rapido caemos
    // Variables Vista
    public Transform camara; // registar que camara va a funcionar como los ojos del jugador(Player)
    public float sensibilidadMouse = 200f; // que tan rapido se va a mover el mouse para voltear a ver en diferentes direcciones
    private float rotacionXVertical = 0f; // es para indicar cuando grados tendra el jugador, hacia arriba o hacia abajo.
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // Esta funcion es para buscar la funcion CharacterController

        Cursor.lockState= CursorLockMode.Locked; // Esta linea bloquea el puntero del mouse en los limites de la pantalla
    }

    // Update is called once per frame
    void Update()
    {

        ManejadorVista();
            ManejadorMovimiento();
    }
    void ManejadorVista()
    {
        //1.  leer el input del mouse
        float mouseX= Input.GetAxis("Mouse X")* sensibilidadMouse*Time.deltaTime;//REGISTRA EL MOVIMIENTO HORIZONTAL
        float mouseY= Input.GetAxis("Mouse Y")* sensibilidadMouse*Time.deltaTime; //REGISTRA EL MOVIMIENTO VERTICAL

        //2.  Construir la rotacion horizontal
        transform.Rotate(Vector3.up * mouseX);
        //3.  registro de la rotacion vertical
        rotacionXVertical-= mouseY;
        //4.  limitar la rotacion vertical
        Mathf.Clamp(rotacionXVertical, -90f, 90f);
        //5.  Aplicar la rotacion
                               //son los ejes           x          Y  Z
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0);
    }

    void ManejadorMovimiento()
    {
        //1 leer el input de movimiento (WASD O la flechas de direccion)

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        //2 Crear el vector de movimiento

        // se almacena de forma locar el registro de direccion de movimiento
        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;

        // 3 Mover el CharacterController
        controller.Move(direccion * velocidad * Time.deltaTime);

        // 4 Aplicar la gravedad 
        // Registro si estoy en el piso para un futuro comportamiento de salto
        if (controller.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f; // Una pequeña fuerza hacia abajo para mantenerlo pegado al piso
        }
        //Aplicamos la aceleracion de la gravedad
        velocidadVertical.y += gravedad * Time.deltaTime;

        //Movemos el controlador hacia abajo
        controller.Move(velocidadVertical * Time.deltaTime);
    }
}
