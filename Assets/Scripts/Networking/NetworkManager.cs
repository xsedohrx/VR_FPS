using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

[System.Serializable]
public class DefaultRoom {
    public string name;
    public int sceneIndex, maxPlayers;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject roomUI;

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try connect to server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server!");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("We joined the lobby");
        roomUI.SetActive(true);
    }

    public void InitializeRoom(int defaultRoomindex) {
        DefaultRoom roomSettings = defaultRooms[defaultRoomindex];

        //Load Scene
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);

        //Create The Room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player has joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
