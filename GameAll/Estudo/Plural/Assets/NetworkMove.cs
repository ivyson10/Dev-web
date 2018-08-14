using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkMove : MonoBehaviour
{
    public SocketIOComponent socket;

    public void OnMove (Vector3 position)
    {
        //send pos to node
        Debug.Log("sending position to node " + VectorToJson(position));
        socket.Emit("move", new JSONObject(VectorToJson(position)));
    }

    string VectorToJson (Vector3 vector)
    {
        return string.Format(@"{{""x"":""{0}"", ""y"":""{1}""}}", vector.x, vector.z);
    }
}
