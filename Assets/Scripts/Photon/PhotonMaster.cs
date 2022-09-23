using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonMaster : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text _statusText;

    [SerializeField]
    [Header("�o�g���V�[���̖��O")]
    private string _battleSceneName = "BattleScene";

    [SerializeField]
    [Header("����")]
    private TMP_InputField _inputField;

    [SerializeField]
    [Header("�����{�^��")]
    private Button _joinButton;

    [SerializeField]
    [Header("�p�l��")]
    GameObject _panel;

    private const int MaxPlayerPerRoom = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _panel.SetActive(false);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        _joinButton.onClick.AddListener(FindOponent);
    }

    private void OnGUI()// �f�o�b�O�p
    {
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }

    private void FindOponent()
    {
        if (_inputField.text.Length == 0) return;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    //Photon�̃R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        Debug.Log("�}�X�^�[�Ɍq���܂����B");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}�̗��R�Ōq���܂���ł����B");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("���[�����쐬���܂��B");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
        _panel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɎQ�����܂���");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayerPerRoom)
        {
            _statusText.text = "Search Enemy...";
            _panel.SetActive(true);
        }
        else
        {
            _panel.SetActive(false);
            _statusText.text = "Enemy is Coming";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayerPerRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                _panel.SetActive(false);
                _statusText.text = "Enemy is Coming";
                PhotonNetwork.LoadLevel(_battleSceneName);
            }
        }
    }
}
