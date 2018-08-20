using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

public class Network : MonoBehaviour
{
    static SocketIOComponent socket;

    public GameObject myPlayer;

    public Spawner spawner;

    // Use this for initialization
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn", OnSpawned);
        socket.On("move", OnMove);
        socket.On("registered", OnRegistered);
        socket.On("disconnected", OnDisconnected);
        socket.On("requestPosition", OnRequestPosition);
        socket.On("updatePosition", OnUpdatePosition);
    }
    
    void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
        //socket.Emit("move");
    }

    void OnSpawned(SocketIOEvent e)
    {
        Debug.Log("spawned " + e.data);
        var player = spawner.SpawnPlayer (e.data ["id"].ToString());

        if (e.data["x"])
        {
            var movePosition = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));
            var navigatePos = player.GetComponent<Navigator>();
            navigatePos.NavigateTo(movePosition);
        }
    }

    private void OnMove(SocketIOEvent e)
    {
        Debug.Log("player is moving " + e.data);
        var position = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));
        var player = spawner.FindPlayer(e.data["id"].ToString());
        var navigatePos = player.GetComponent<Navigator>();        
        navigatePos.NavigateTo(position);
    }

    void OnRegistered(SocketIOEvent e)
    {
        Debug.Log("registered id: " + e.data);
    }

    void OnRequestPosition (SocketIOEvent e)
    {
        Debug.Log("server is requesting position");

        socket.Emit("updatePosition", new JSONObject(VectorToJson(myPlayer.transform.position)));
    }

    void OnDisconnected (SocketIOEvent e)
    {
        Debug.Log("Player disconnected: " + e.data);
        var id = e.data["id"].ToString();      
        spawner.Remove(id);
    }

    void OnUpdatePosition(SocketIOEvent e)
    {
        Debug.Log("Updating position: " + e.data);

        var position = new Vector3(GetFloatFromJson(e.data, "x"), 0, GetFloatFromJson(e.data, "y"));
        var player = spawner.FindPlayer(e.data["id"].ToString());

        player.transform.position = position;
    }

    float GetFloatFromJson(JSONObject data, string key)
    {
        return float.Parse(data[key].ToString().Replace("\"", ""));
    }

    public static string VectorToJson(Vector3 vector)
    {
        return string.Format(@"{{""x"":""{0}"", ""y"":""{1}""}}", vector.x, vector.z);
    }
}
