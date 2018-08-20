using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;

    public float scanFrequency = 0.5f;
    public float stopFollowDistance = 2;
    float lastScanTime = 0;

    Navigator navigator;

	// Use this for initialization
	void Start ()
    {
        navigator = GetComponent<Navigator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isReadyToScan() && !isInRange())
        {
            Debug.Log("scanning nav path");
            navigator.NavigateTo(target.position);
        }
		
	}

    bool isReadyToScan()
    {
        return Time.time - lastScanTime > scanFrequency && target;
    }

    bool isInRange()
    {
        var distance = Vector3.Distance(target.position , transform.position);
        return distance < stopFollowDistance;
    }
}
