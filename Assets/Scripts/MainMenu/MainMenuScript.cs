using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;

public class MainMenuScript : MonoBehaviourPunCallbacks
{
    public GameObject gameChooser;
    public GameObject optionsMenu;
    public GameObject hostOrJoin;
    public GameObject joinLobbyMenu;
    public GameObject hostLobbyMenu;

    // Start is called before the first frame update
    void Awake()
    {
        gameChooser.SetActive(false);
        optionsMenu.SetActive(false);
        Debug.Log("Connecting to master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        joinLobbyMenu.SetActive(false);
        hostLobbyMenu.SetActive(false);
        hostOrJoin.SetActive(false);
        optionsMenu.SetActive(false);
        gameChooser.SetActive(true);
    }

    public void HostGame(int map)
    {
        joinLobbyMenu.SetActive(false);
        hostLobbyMenu.SetActive(true);
        optionsMenu.SetActive(false);
        gameChooser.SetActive(false);
        string serverName = "blahblahblah";
        PhotonNetwork.CreateRoom(serverName);
        SceneManager.LoadScene(map);
        Time.timeScale = 1f;
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public void JoinServer()
    {
        joinLobbyMenu.SetActive(true);
        hostLobbyMenu.SetActive(false);
        optionsMenu.SetActive(false);
        gameChooser.SetActive(false);
        PhotonNetwork.JoinRoom("blahblahblah");
    }

    

    public void Options()
    {
        joinLobbyMenu.SetActive(false);
        hostLobbyMenu.SetActive(false);
        hostOrJoin.SetActive(false);
        gameChooser.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void HostOrJoinMenu()
    {
        joinLobbyMenu.SetActive(false);
        hostLobbyMenu.SetActive(false);
        gameChooser.SetActive(false);
        optionsMenu.SetActive(false);
        hostOrJoin.SetActive(true);
    }

    

    
}
