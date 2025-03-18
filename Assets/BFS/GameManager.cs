using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance { get; private set; }

    public Pf_Node startNode;
    public Pf_Node endNode;

    private Pathfinding pf; // No es necesario SerializeField porque no es un MonoBehaviour

    public List<PatrolAgent> agents;
    List<Pf_Node> path;

    // Se ejecuta antes de Start
    private void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject); // Destruye objetos duplicados
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }
}
    // Update is called once per frame
