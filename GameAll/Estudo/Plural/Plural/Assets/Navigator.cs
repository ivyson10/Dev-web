using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    NavMeshAgent agent;

	
	void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
	}

	public void NavigateTo (Vector3 position)
    {
        agent.SetDestination(position);
	}

    void Update()
    {
       
    }
}
