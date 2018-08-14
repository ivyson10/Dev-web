using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour
{
    static SocketIOComponent socket;
    public GameObject playerPrefab;

    Dictionary<string, GameObject> players;

    // Use this for initialization
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn", OnSpawned);
        socket.On("move", OnMove);

        players = new Dictionary<string, GameObject>();
    }
    
    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
        //socket.Emit("move");
    }
	
    void OnSpawned(SocketIOEvent e)
    {
        Debug.Log("spawned " + e.data);
        var player = Instantiate(playerPrefab);

        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count: " + players.Count);
    }

    private void OnMove(SocketIOEvent e)
    {
        Debug.Log("player is moving " + e.data);
        var position = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));
        var player = players[e.data["id"].ToString()];
        var navigatePos = player.GetComponent<NavigatePosition>();        
        navigatePos.NavigateTo(position);
    }

    void OnRegistered(SocketIOEvent e)
    {
        Debug.Log("registered id: " + e.data);
    }

    float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }
}
