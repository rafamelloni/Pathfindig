using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float viewRadius;
    [SerializeField] private LayerMask obstacleLayer;
    public float viewAngle = 60f;
    public PatrolAgent agente;
    public bool playerInSight;


    private void Start()
    {
        
        agente = GetComponent<PatrolAgent>();

        agente.isPatrolling = true; // Aseguramos que el agente siempre arranque patrullando.
        agente.isAlerted = false;
        agente.isReturning = false;
        agente.hasReturnedToPatrol = false;
    }

    private void Update()
    {
        
    }

    public bool FieldOfView()
    {
        Vector3 dir = target.transform.position - transform.position;

        if (dir.sqrMagnitude > viewRadius * viewRadius)
        {
            
            return false;
        }

        if (Vector3.Angle(transform.forward, dir) <= viewAngle / 2)
        {

            if (!Physics.Raycast(transform.position, dir, out RaycastHit hit, dir.magnitude, obstacleLayer))
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.red);
                return true;
            }

        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 lineA = GetVectorFromAngle(viewAngle / 2 + transform.eulerAngles.y);
        Vector3 lineB = GetVectorFromAngle(-viewAngle / 2 + transform.eulerAngles.y);

        Gizmos.DrawLine(transform.position, transform.position + lineA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * viewRadius);

    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}

