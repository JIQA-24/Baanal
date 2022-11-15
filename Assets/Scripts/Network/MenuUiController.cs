using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuUiController : MonoBehaviourPunCallbacks
{
    public GameObject mainWindow;
    public GameObject lobbyWidow;

    [Header("MenuPrincipal")]
    public Button createRoomBtn;
    public Button joinRoomBtn;

    [Header("Lobby")]
    public Button StartGameBtn;
    public TextMeshProUGUI playertextList;

    public override void OnConnectedToMaster()
    {
        createRoomBtn.interactable = true; 
        joinRoomBtn.interactable = true;
    }

    public void JoinRoom(TMP_InputField _roomName)
    {
        _NetworkManager.instance.JoinRoom(_roomName.text);
    }

    public void CreateRoom(TMP_InputField _roomName)
    {
        _NetworkManager.instance.CreateRoom(_roomName.text);
    }

    public void GetPlayerName(TMP_InputField _playerName)
    {
        PhotonNetwork.NickName = _playerName.text;
    }

    public override void OnJoinedRoom()
    {
        lobbyWidow.SetActive(true);
        mainWindow.SetActive(false);
        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
    }

    [PunRPC]
    public void UpdatePlayerInfo()
    {
        playertextList.text = ""; //Limpiar campo de texto
        foreach (Player player in PhotonNetwork.PlayerList) //Ciclo para pintar todos los player en el Room
        {
            playertextList.text += player.NickName + "\n";//Agregar nombre de players
        }

        if (PhotonNetwork.IsMasterClient) //Verificar si el cliente es Master Clien
        {
            StartGameBtn.interactable = true;//Activar boton de iniciar juego
        }
        else
        {
            StartGameBtn.interactable = false; // Desactivar boton de inicio de juego
        }
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
        lobbyWidow.SetActive(false);
        mainWindow.SetActive(true);
    }

    public void StartGame()
    {
        _NetworkManager.instance.photonView.RPC("LoadScene",RpcTarget.All,"PrototypeMultiplayer");
    }
}
