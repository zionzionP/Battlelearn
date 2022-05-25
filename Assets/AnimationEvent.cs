using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationEvent : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();        
    }

    void Navstop()
    {
        navMeshAgent.speed = 0;
        //Debug.Log("stopped");
    }

    void Unactive()
    {
        this.gameObject.SetActive(false);
        

    }

    
}
