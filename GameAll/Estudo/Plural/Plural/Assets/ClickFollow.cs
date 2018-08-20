using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFollow : MonoBehaviour, IClickable
{
    public GameObject myPlayer;

    public void OnClick(RaycastHit hit)
    {
        Debug.Log("Following " + hit.collider.gameObject.name);
    }
}
