using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolAgent : MonoBehaviour
{
    [SerializeField] private List<Pf_Node> patrolWaypoints; // Lista de nodos de patrullaje
    [SerializeField] private float moveSpeed = 2f; // Velocidad del agente
    [SerializeField] private float moveSpeedPersue = 2f; // Velocidad del agente
    private int currentWaypointIndex = 0; // Índice del waypoint actual
    private Transform targetWaypoint;

    public bool isPatrolling = false;
    public bool isAlerted = false;
    public bool isReturning = false;
    private bool isPersuing = false;

    public bool pathCalculated = false;
    public bool hasReturnedToPatrol = false;
    public bool alertNotified = false;

    private Pathfinding pathfinding;
    public List<Pf_Node> allNodes;




    public Transform Player;
    public Agent agent = default;

    public Fov agenteFov;

    
    private string currentState;

    private void Start()
    {
        allNodes = new List<Pf_Node>(FindObjectsOfType<Pf_Node>());
        if (patrolWaypoints.Count > 0)
        {
            targetWaypoint = patrolWaypoints[currentWaypointIndex].transform;
        }
        pathfinding = new Pathfinding();
        SetState("Patrolling");
    }

    private void Update()
    {
        print(currentState);

        if (isPatrolling)
        {
            Patrol();
            if (agenteFov.FieldOfView())
            {
                SetState("Persuing");

            }
            
        }
        else if (isAlerted)
        { 
            
            if (!agent.FollowPath())
            {
                SetState("Returning");
            }
            
            if (agenteFov.FieldOfView())
            {
                SetState("Persuing");

            }
        }
        else if (isReturning)
        {
             
            
            if (!agent.FollowPath())
            {
                SetState("Patrolling");
            }

            if (agenteFov.FieldOfView())
            {
                
                SetState("Persuing");

            }

        }
        else if (isPersuing)
        {
            PersuePlayer();
            if (!agenteFov.FieldOfView())
            {
                
                SetState("Alerted");

            }
        }
  
    }



    public void SetState(string newState)
    {
        // Resetea todos los estados
        isPatrolling = false;
        isAlerted = false;
        isReturning = false;
        isPersuing = false;
        
        currentState = newState;
        // Activa el nuevo estado
        if (newState == "Patrolling") 
        {
            hasReturnedToPatrol = false;
            isPatrolling = true;
        }
        
        if (newState == "Alerted") 
        {
            SetPathAlerted();
            isAlerted = true;
            
        }

        if (newState == "Returning") 
        {
            ReturnPath();
            isReturning = true;
        }

        if (newState == "Persuing") 
        {
            NotifyAgents();
            isPersuing = true;
            
        } 
    }

    private void ReturnPath()
    {
        

        Pf_Node StartNode = GetAgentNodeAtPosition();
        Pf_Node endNode = patrolWaypoints[0];

        agent.SetPath(pathfinding.ConstructPathAStar(StartNode, endNode));
        

    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Count;
            targetWaypoint = patrolWaypoints[currentWaypointIndex].transform;
        }

        MoveToWaypoint(targetWaypoint.position);
    }

    private void MoveToWaypoint(Vector3 waypointPosition)
    {
        Vector3 direction = (waypointPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.forward = direction;
    }

    public void PersuePlayer()
    {
    
        if (Player != null)
        {

                Vector3 direction = (Player.position - transform.position).normalized;

                // Mueve la cápsula en la dirección del objetivo
                transform.Translate(direction * moveSpeedPersue * Time.deltaTime, Space.World);

                // Opcional: Haz que la cápsula mire hacia el objetivo
                transform.LookAt(Player);
            
        }
    }


   
  
        private void NotifyAgents()
    {
        foreach (PatrolAgent agent in GameManager.Instance.agents)
        {
            if (agent.currentState == "Persuing") continue;
            {
                agent.SetState("Alerted");
            } 
        }
    }


    private void SetPathAlerted()
    {

        Pf_Node agentNode = GetAgentNodeAtPosition();
        Pf_Node playerNode = GetPlayerNodeAtPosition();

        Debug.Log("nodo agente " + agentNode.name);
        Debug.Log("nodo player " + playerNode.name);

        Debug.Log(pathfinding.ConstructPathAStar(agentNode, playerNode));

        agent.SetPath(pathfinding.ConstructPathAStar(agentNode, playerNode));
        
        
        
    }

    public Pf_Node GetAgentNodeAtPosition()
    {
        Pf_Node closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (Pf_Node node in allNodes)
        {
            float distance = Vector3.Distance(node.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }
        return closestNode;
    }

    public Pf_Node GetPlayerNodeAtPosition()
    {
        Pf_Node closestNode = null;
        float closestDistance = float.MaxValue;

        foreach (Pf_Node node in allNodes)
        {
            float distance = Vector3.Distance(node.transform.position, Player.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }
        return closestNode;
    }
}

