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
    [Header("最大人数")]
    private int maxPlayers = 2;

    [SerializeField]
    [Header("公開・非公開")]
    private bool isVisible = true;

    [SerializeField]
    [Header("入室の可否")]
    private bool isOpen = true;

    [SerializeField]
    [Header("部屋名")]
    private string roomName = "Guest Room";

    #endregion

    #region unity event

    private void Awake()
    {
        // シーンの自動同期: 無効
        PhotonNetwork.AutomaticallySyncScene = false;
    }

    private void Start()
    {
        // Photonに接続
        Connect("1.0");
    }

    #endregion

    #region connect

    // Photonに接続する
    private void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ニックネームを付ける
    private void SetMyNickName(string nickName)
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LocalPlayer.NickName = nickName;
        }
    }

    #endregion

    #region join lobby

    // ロビーに入る
    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    #endregion

    #region join room

    // 部屋を作成して入室する
    public void CreateAndJoinRoom()
    {
        // ルームオプションの基本設定
        RoomOptions roomOptions = new RoomOptions
        {
            // 部屋の最大人数
            MaxPlayers = (byte)maxPlayers,

            // 公開
            IsVisible = isVisible,

            // 入室可
            IsOpen = isOpen
        };

        // 部屋を作成して入室する
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    // 部屋に入室する （存在しなければ作成して入室する）
    public void JoinOrCreateRoom()
    {
        // ルームオプションの基本設定
        RoomOptions roomOptions = new RoomOptions
        {
            // 部屋の最大人数
            MaxPlayers = (byte)maxPlayers,

            // 公開
            IsVisible = isVisible,

            // 入室可
            IsOpen = isOpen
        };

        // 入室 (存在しなければ部屋を作成して入室する)
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        }
    }

    // 特定の部屋に入室する
    public void JoinRoom(string targetRoomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRoom(targetRoomName);
        }
    }

    // ランダムな部屋に入室する
    public void JoinRandomRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    #endregion

    #region leave room

    // 部屋から退室する
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            // 退室
            PhotonNetwork.LeaveRoom();
        }
    }

    #endregion

    #region pun callbacks

    // Photonに接続した時
    public override void OnConnected()
    {
        // ニックネームを付ける
        SetMyNickName("name");
    }

    // Photonから切断された時
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    // マスターサーバーに接続した時
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        // ロビーに入る
        JoinLobby();
    }

    // ロビーに入った時
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
    }

    // ロビーから出た時
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }

    // 部屋を作成した時
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    // 部屋の作成に失敗した時
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }

    // 部屋に入室した時
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        // 部屋の情報を表示
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("RoomName: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("HostName: " + PhotonNetwork.MasterClient.NickName);
            Debug.Log("Stage: " + PhotonNetwork.CurrentRoom.CustomProperties["Stage"] as string);
            Debug.Log("Difficulty: " + PhotonNetwork.CurrentRoom.CustomProperties["Difficulty"] as string);
            Debug.Log("Slots: " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers);
        }
    }

    // 特定の部屋への入室に失敗した時
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
    }

    // ランダムな部屋への入室に失敗した時
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
    }

    // 部屋から退室した時
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    // 他のプレイヤーが入室してきた時
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
    }

    // 他のプレイヤーが退室した時
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
    }

    // マスタークライアントが変わった時
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched");
    }

    // ロビーに更新があった時
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }

    // ルームリストに更新があった時
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }

    // ルームプロパティが更新された時
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }

    // プレイヤープロパティが更新された時
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    // フレンドリストに更新があった時
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("OnFriendListUpdate");
    }

    // 地域リストを受け取った時
    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
    }

    // WebRpcのレスポンスがあった時
    public override void OnWebRpcResponse(OperationResponse response)
    {
        Debug.Log("OnWebRpcResponse");
    }

    // カスタム認証のレスポンスがあった時
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    // カスタム認証が失敗した時
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log("OnCustomAuthenticationFailed");
    }

    #endregion
}
