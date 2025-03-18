using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    // Lista de vecinos
    public List<Nodo> vecinos = new List<Nodo>();

    // Rango de conexi�n
    public float rango = 5f;

    // Capa de obst�culos (como paredes)
    public LayerMask capaObstaculos;

    // N�mero m�ximo de vecinos permitidos
    public int maxVecinos = 3;

    private void Start()
    {
        // Encontrar los vecinos al iniciar el juego
        EncontrarVecinos();
    }

    // M�todo para encontrar y almacenar todos los nodos vecinos dentro del rango
    public void EncontrarVecinos()
    {
        // Busca todos los objetos Nodo en la escena
        Nodo[] todosLosNodos = FindObjectsOfType<Nodo>();
        vecinos.Clear(); // Limpia la lista de vecinos actuales

        foreach (Nodo nodo in todosLosNodos)
        {
            // Evita conectarse a s� mismo y comprueba si el nodo ya alcanz� el l�mite de vecinos
            if (nodo != this && vecinos.Count < maxVecinos)
            {
                // Calcula la distancia entre el nodo actual y el otro nodo
                float distancia = Vector3.Distance(transform.position, nodo.transform.position);

                // Si el otro nodo est� dentro del rango, verifica la l�nea de visi�n
                if (distancia <= rango)
                {
                    // Lanza un raycast desde el nodo actual hacia el nodo vecino
                    Vector3 direccion = nodo.transform.position - transform.position;
                    if (!Physics.Raycast(transform.position, direccion.normalized, distancia, capaObstaculos))
                    {
                        // Si no hay obst�culo en el camino y el nodo a�n no alcanz� el m�ximo de vecinos, a�adirlo
                        vecinos.Add(nodo);
                    }
                }
            }
        }
    }

    // Dibuja l�neas en el editor de Unity para visualizar las conexiones entre nodos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // Color de la l�nea de Gizmos

        // Dibuja una l�nea entre este nodo y cada uno de sus vecinos
        foreach (Nodo vecino in vecinos)
        {
            if (vecino != null)
            {
                Gizmos.DrawLine(transform.position, vecino.transform.position);  // Dibuja la l�nea entre los nodos
            }
        }
    }
}
