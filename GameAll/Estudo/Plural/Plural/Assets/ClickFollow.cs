using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFollow : MonoBehaviour, IClickable
{
    public Follower myPlayerFollower;

    public void OnClick(RaycastHit hit)
    {
        Debug.Log("Following " + hit.collider.gameObject.name);
        myPlayerFollower.target = transform;
        
    }
}
