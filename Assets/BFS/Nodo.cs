using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
    // Lista de vecinos
    public List<Nodo> vecinos = new List<Nodo>();

    // Rango de conexión
    public float rango = 5f;

    // Capa de obstáculos (como paredes)
    public LayerMask capaObstaculos;

    // Número máximo de vecinos permitidos
    public int maxVecinos = 3;

    private void Start()
    {
        // Encontrar los vecinos al iniciar el juego
        EncontrarVecinos();
    }

    // Método para encontrar y almacenar todos los nodos vecinos dentro del rango
    public void EncontrarVecinos()
    {
        // Busca todos los objetos Nodo en la escena
        Nodo[] todosLosNodos = FindObjectsOfType<Nodo>();
        vecinos.Clear(); // Limpia la lista de vecinos actuales

        foreach (Nodo nodo in todosLosNodos)
        {
            // Evita conectarse a sí mismo y comprueba si el nodo ya alcanzó el límite de vecinos
            if (nodo != this && vecinos.Count < maxVecinos)
            {
                // Calcula la distancia entre el nodo actual y el otro nodo
                float distancia = Vector3.Distance(transform.position, nodo.transform.position);

                // Si el otro nodo está dentro del rango, verifica la línea de visión
                if (distancia <= rango)
                {
                    // Lanza un raycast desde el nodo actual hacia el nodo vecino
                    Vector3 direccion = nodo.transform.position - transform.position;
                    if (!Physics.Raycast(transform.position, direccion.normalized, distancia, capaObstaculos))
                    {
                        // Si no hay obstáculo en el camino y el nodo aún no alcanzó el máximo de vecinos, añadirlo
                        vecinos.Add(nodo);
                    }
                }
            }
        }
    }

    // Dibuja líneas en el editor de Unity para visualizar las conexiones entre nodos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // Color de la línea de Gizmos

        // Dibuja una línea entre este nodo y cada uno de sus vecinos
        foreach (Nodo vecino in vecinos)
        {
            if (vecino != null)
            {
                Gizmos.DrawLine(transform.position, vecino.transform.position);  // Dibuja la línea entre los nodos
            }
        }
    }
}
