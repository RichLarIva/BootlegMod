using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
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
        mainMenu.SetActive(true);
        MenuManager.Instance.OpenMenu("MainMenu");
        Debug.Log("Joined Lobby");
    }


    public void StartGame()
    {
        MenuManager.Instance.OpenMenu("GameChooser");
    }

    public void Options()
    {
        MenuManager.Instance.OpenMenu("Options");
    }

    public void HostOrJoin()
    {
        MenuManager.Instance.OpenMenu("HostOrJoin");
    }

    public void HostMenu()
    {
        MenuManager.Instance.OpenMenu("HostLobby");
    }

    public void JoinLobby()
    {
        MenuManager.Instance.OpenMenu("JoinLobby");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ChooseMap(int i)
    {
        SceneManager.LoadScene(i);
        Debug.Log("Loading new scene");
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
