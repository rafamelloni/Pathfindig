using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Agent : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    private List<Pf_Node> _path = new List<Pf_Node>();

    [SerializeField] Pf_Node closetsNode;
    [SerializeField] Pf_Node playerNode;

    public PatrolAgent agentPatrol;

    //camino para que el agente regrese al patrullaje
    


    

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetPath(List<Pf_Node> path)
    {
        if (path.Count <= 0) return;

        _path = path;

    }

    public bool FollowPath()
    {
        if (_path == null || _path.Count == 0)
        {
          return false;
        }
        
        Vector3 targetPosition = _path[0].transform.position;
        Vector3 dir = targetPosition - transform.position;
        dir.y = 0;

        transform.position += dir.normalized * _speed * Time.deltaTime;

        if (dir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (dir.magnitude < 0.25f)
        {
            _path.RemoveAt(0);
        }
        return true;
     
    }


}
