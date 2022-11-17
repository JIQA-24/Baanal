using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class _NetworkManager : MonoBehaviourPunCallbacks
{
    //Singleton
    public static _NetworkManager instance;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    /*public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        Debug.Log("Conectado");
        CreateRoom("testRoom");
    }*/

    //Conexion
    public void CreateRoom(string _name)
    {
        PhotonNetwork.CreateRoom(_name);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Se creo el room : " + PhotonNetwork.CurrentRoom.Name);
    }
    public void JoinRoom(string _name)
    {
        PhotonNetwork.JoinRoom(_name);
    }

    public void LeveaRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void LoadScene(string _nameScene)
    {
        PhotonNetwork.LoadLevel(_nameScene);
    }
}