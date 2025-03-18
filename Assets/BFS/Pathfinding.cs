using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public List<Pf_Node> ConstructPathAStar(Pf_Node startingNode, Pf_Node goalNode)
    {
        if (!startingNode || !goalNode) return null;

        // Crear la cola de prioridad (frontier) para el algoritmo A*.
        PriorityQueue<Pf_Node> frontier = new PriorityQueue<Pf_Node>();
        frontier.Put(startingNode, startingNode.cost); // Usamos el costo inicial del nodo de inicio.

        // Diccionario que guarda el nodo anterior para cada nodo recorrido (para reconstruir el camino).
        Dictionary<Pf_Node, Pf_Node> cameFrom = new Dictionary<Pf_Node, Pf_Node>();
        cameFrom.Add(startingNode, null);

        // Diccionario que guarda el costo de llegar a cada nodo.
        Dictionary<Pf_Node, float> costSoFar = new Dictionary<Pf_Node, float>();
        costSoFar.Add(startingNode, startingNode.cost);

        // Mientras la cola de prioridad tenga elementos (nodos por explorar).
        while (frontier.Count > 0)
        {
            // Obtener el nodo con la mayor prioridad (menor costo estimado)
            Pf_Node current = frontier.Get();

            // Si llegamos al nodo objetivo, reconstruimos el camino.
            if (current == goalNode)
            {
                List<Pf_Node> path = new List<Pf_Node>();
                Pf_Node nodeToAdd = current;

                // Reconstruir el camino hacia atrás desde el nodo objetivo.
                while (nodeToAdd != null)
                {
                    path.Add(nodeToAdd);
                    nodeToAdd = cameFrom[nodeToAdd];
                }

                // Invertir el camino para que vaya del inicio al objetivo.
                path.Reverse();
                return path;
            }

            // Explorar los vecinos del nodo actual (obtenidos manualmente).
            foreach (Pf_Node next in current.GetNeighbors())
            {
                // Si el nodo está bloqueado, no lo exploramos.
                if (next.isBlocked) continue;

                // Calcular el costo desde el nodo actual al siguiente nodo (usando distancia).
                float dist = Vector3.Distance(goalNode.transform.position, next.transform.position);

                // El costo total es el costo del nodo actual + la distancia al siguiente nodo.
                float newCost = costSoFar[current] + dist;
                float priority = newCost + dist; // Prioridad = costo total + estimación al objetivo.

                // Si el vecino no ha sido explorado antes, o se encuentra un camino más barato, lo actualizamos.
                if (!cameFrom.ContainsKey(next))
                {
                    frontier.Put(next, priority);
                    cameFrom.Add(next, current);
                    costSoFar.Add(next, newCost);
                }
                else if (newCost < costSoFar[next])
                {
                    frontier.Put(next, priority);
                    cameFrom[next] = current;
                    costSoFar[next] = newCost;
                }
            }
        }

        // Si no se encuentra un camino, se retorna null.
        Debug.Log("no hay camino");
        return null;
    }

    // Heurística: en este caso usamos la distancia Euclidiana.
    private float Heuristic(Pf_Node a, Pf_Node b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }
}
