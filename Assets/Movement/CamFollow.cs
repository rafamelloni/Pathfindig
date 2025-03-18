using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float fixedHeight = 10f; // Altura fija de la c�mara en el eje Y

    private void LateUpdate()
    {
        if (player != null)
        {
            // Actualizar la posici�n de la c�mara
            Vector3 newPosition = new Vector3(player.position.x, fixedHeight, player.position.z);
            transform.position = newPosition;
        }
    }
}


