using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region field

    [SerializeField]
    [Header("�ő�l��")]
    private int maxPlayers = 2;

    [SerializeField]
    [Header("���J�E����J")]
    private bool isVisible = true;

    [SerializeField]
    [Header("�����̉�")]
    private bool isOpen = true;

    [SerializeField]
    [Header("������")]
    private string roomName = "Guest Room";

    #endregion

    #region unity event

    private void Awake()
    {
        // �V�[���̎�������: ����
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    private void Start()
    {
        // Photon�ɐڑ�
        Connect("1.0");
    }

    #endregion

    #region connect

    // Photon�ɐڑ�����
    private void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // �j�b�N�l�[����t����
    private void SetMyNickName(string nickName)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = nickName;
        }
    }

    #endregion

    #region join lobby

    // ���r�[�ɓ���
    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #endregion

    #region join room

    // �������쐬���ē�������
    public void CreateAndJoinRoom()
    {
        // ���[���I�v�V�����̊�{�ݒ�
        RoomOptions roomOptions = new RoomOptions
        {
            // �����̍ő�l��
            MaxPlayers = (byte)maxPlayers,

            // ���J
            IsVisible = isVisible,

            // ������
            IsOpen = isOpen
        };

        // �������쐬���ē�������
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    // �����ɓ������� �i���݂��Ȃ���΍쐬���ē�������j
    public void JoinOrCreateRoom()
    {
        // ���[���I�v�V�����̊�{�ݒ�
        RoomOptions roomOptions = new RoomOptions
        {
            // �����̍ő�l��
            MaxPlayers = (byte)maxPlayers,

            // ���J
            IsVisible = isVisible,

            // ������
            IsOpen = isOpen
        };

        // ���� (���݂��Ȃ���Ε������쐬���ē�������)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        }
    }

    // ����̕����ɓ�������
    public void JoinRoom(string targetRoomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRoom(targetRoomName);
        }
    }

    // �����_���ȕ����ɓ�������
    public void JoinRandomRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    #endregion

    #region leave room

    // ��������ގ�����
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            // �ގ�
            PhotonNetwork.LeaveRoom();
        }
    }

    #endregion

    #region pun callbacks

    // Photon�ɐڑ�������
    public override void OnConnected()
    {
        // �j�b�N�l�[����t����
        SetMyNickName("name");
    }

    // Photon����ؒf���ꂽ��
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    // �}�X�^�[�T�[�o�[�ɐڑ�������
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        // ���r�[�ɓ���
        JoinLobby();
    }

    // ���r�[�ɓ�������
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    // ���r�[����o����
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    // �������쐬������
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    // �����̍쐬�Ɏ��s������
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }

    // �����ɓ���������
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        // �����̏���\��
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("RoomName: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("HostName: " + PhotonNetwork.MasterClient.NickName);
            Debug.Log("Stage: " + PhotonNetwork.CurrentRoom.CustomProperties["Stage"] as string);
            Debug.Log("Difficulty: " + PhotonNetwork.CurrentRoom.CustomProperties["Difficulty"] as string);
            Debug.Log("Slots: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }

    // ����̕����ւ̓����Ɏ��s������
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
    }

    // �����_���ȕ����ւ̓����Ɏ��s������
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
    }

    // ��������ގ�������
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    // ���̃v���C���[���������Ă�����
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
    }

    // ���̃v���C���[���ގ�������
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
    }

    // �}�X�^�[�N���C�A���g���ς������
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched");
    }

    // ���r�[�ɍX�V����������
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }

    // ���[�����X�g�ɍX�V����������
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }

    // ���[���v���p�e�B���X�V���ꂽ��
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }

    // �v���C���[�v���p�e�B���X�V���ꂽ��
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    // �t�����h���X�g�ɍX�V����������
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("OnFriendListUpdate");
    }

    // �n�惊�X�g���󂯎������
    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
    }

    // WebRpc�̃��X�|���X����������
    public override void OnWebRpcResponse(OperationResponse response)
    {
        Debug.Log("OnWebRpcResponse");
    }

    // �J�X�^���F�؂̃��X�|���X����������
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    // �J�X�^���F�؂����s������
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log("OnCustomAuthenticationFailed");
    }

    #endregion
}
