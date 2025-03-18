using System.Collections.Generic;
using UnityEngine;

public class Pf_Node : MonoBehaviour
{
    [SerializeField] private List<Pf_Node> _neighbors = new(); // Vecinos configurables desde el Inspector
    public bool isBlocked = false; // Nodo bloqueado (opcional)
    public bool isInPath = false; // Nodo en el camino (inicialmente false)
    public float cost = 0; // Costo asociado al nodo (puedes configurarlo manualmente)

    // Devuelve los vecinos del nodo
    public List<Pf_Node> GetNeighbors()
    {
        return _neighbors;
    }

    private void OnDrawGizmos()
    {
        // Cambiar color si el nodo está en el camino
        Gizmos.color = isInPath ? Color.red : Color.green;

        // Dibuja las conexiones de los vecinos
        foreach (Pf_Node neighbor in _neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }

        // Dibuja un círculo pequeño para representar el nodo
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}
