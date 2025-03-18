using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5f; // Velocidad de movimiento

    private void Update()
    {
        // Obtener entradas del teclado
        float horizontal = Input.GetAxis("Horizontal"); // A y D
        float vertical = Input.GetAxis("Vertical"); // W y S

        // Calcular el movimiento
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;

        // Aplicar el movimiento al transform del jugador
        transform.Translate(movement, Space.World);
    }


}


