using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class _GameController : MonoBehaviourPunCallbacks
{
    //Instancia
    public static _GameController instance;

    public bool isGameEnd = false;
    public string playerPrefab;

    public Transform[] spawnPlayer;
    public PlayerMovement[] players;

    private int playerInGame;

    private void Awake()
    {
        instance = this;
    } 
    
    // Start is called before the first frame update
    void Start()
    {
        players = new PlayerMovement[PhotonNetwork.PlayerList.Length];
        photonView.RPC("InGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void InGame()
    {
        playerInGame++; // contador de jugadores
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();// mandar llamar posicionamiento de player
        }
    }

    void SpawnPlayer()
    {
        int randomPosition = Random.Range(0, spawnPlayer.Length);
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab, spawnPlayer[0].position, Quaternion.identity);
        
        PlayerMovement playScript = playerObj.GetComponent<PlayerMovement>();
        playScript.photonView.RPC("Prueba", RpcTarget.All, PhotonNetwork.LocalPlayer);

        Shooter playScript_1 = playerObj.GetComponent<Shooter>();
        playScript_1.photonView.RPC("Prueba1", RpcTarget.All, PhotonNetwork.LocalPlayer);

        Health playScript_2 = playerObj.GetComponent<Health>();
        playScript_2.photonView.RPC("Prueba2", RpcTarget.All, PhotonNetwork.LocalPlayer);

        CharacterController2D playScript_3 = playerObj.GetComponent<CharacterController2D>();
        playScript_3.photonView.RPC("Prueba3", RpcTarget.All, PhotonNetwork.LocalPlayer);

    }

}
