﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject myPlayer;
    public GameObject playerPrefab;
    Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public GameObject SpawnPlayer(string id)
    {
        var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        player.GetComponent<ClickFollow>().myPlayerFollower = myPlayer.GetComponent<Follower>();
        players.Add(id, player);
        return player;
    }

    public GameObject FindPlayer(string id)
    {
        return players[id];
    }

    public void Remove(string id)
    {
        var player = players[id];
        Destroy(player);
        players.Remove(id);
    }
}
