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
    [Header("バトルシーンの名前")]
    private string _battleSceneName = "BattleScene";

    [SerializeField]
    [Header("入力")]
    private TMP_InputField _inputField;

    [SerializeField]
    [Header("入室ボタン")]
    private Button _joinButton;

    [SerializeField]
    [Header("パネル")]
    GameObject _panel;

    [SerializeField]
    string _garbageCanPath;

    [SerializeField]
    string _dirtyPersonPath;

    [SerializeField]
    Vector3 _garbageCanPosition;

    [SerializeField]
    Vector3 _dirtyPersonPosition;

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

    private void OnGUI()// デバッグ用
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

    //Photonのコールバック
    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターに繋ぎました。");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}の理由で繋げませんでした。");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("ルームを作成します。");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayerPerRoom });
        _panel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに参加しました");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if (playerCount != MaxPlayerPerRoom)
        {
            var garbageCan = PhotonNetwork.Instantiate(_garbageCanPath, _garbageCanPosition, Quaternion.identity);
            DontDestroyOnLoad(garbageCan);
            _statusText.text = "Search Enemy...";
            _panel.SetActive(true);
        }
        else
        {
            var dirtyPerson = PhotonNetwork.Instantiate(_dirtyPersonPath, _dirtyPersonPosition, Quaternion.identity);
            DontDestroyOnLoad(dirtyPerson);
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
