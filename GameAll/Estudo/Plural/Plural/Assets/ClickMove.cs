using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour, IClickable
{
    public GameObject player;
	
	public void OnClick (RaycastHit hit)
    { 
        var navigator = player.GetComponent<Navigator>();
        var netMove = player.GetComponent<NetworkMove>();
        navigator.NavigateTo(hit.point);

        netMove.OnMove(hit.point);
	}
}
