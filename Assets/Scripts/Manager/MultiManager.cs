using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

/// MonoBehaviourPunCallbacks ���
public class MultiManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerOrigin;

    //[SerializeField] private CameraManager MainCamera;
    [SerializeField] private GameObject StartCanvas;


    private string gameVersion = "1"; // ���� ����

    //public Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ

    //public Button joinButton; // �� ���� ��ư

    private GameObject PlayerInstance;

    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    void Start()
    {
        DontDestroyOnLoad(this);

        if (StartCanvas)
        {
            DontDestroyOnLoad(StartCanvas);
        }


        //joinButton?.onClick.AddListener(Connect);

        //���ӿ� �ʿ��� ���� (���� ����) ����
        PhotonNetwork.GameVersion = this.gameVersion;
        //������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();


        //this.joinButton.interactable = false;
        //this.connectionInfoText.text = "������ ������ ������...";
    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
/*        this.joinButton.interactable = true;
        this.connectionInfoText.text = "�¶��� : ������ ������ ���� ��";*/
    }

    // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
/*        this.joinButton.interactable = false;
        this.connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ���... ";*/
        //������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    // �� ���� �õ�
    public void Connect()
    {
        // �ߺ� ���� ����
/*        this.joinButton.interactable = false;
        this.joinButton.gameObject.SetActive(false);*/

        // ������ ������ ���� ���̶��
        if (PhotonNetwork.IsConnected)
        {

            //�뿡 �����Ѵ�.
            //this.connectionInfoText.text = "�뿡 ����....";
            Debug.Log("�뿡 ������...");
            PhotonNetwork.JoinOrCreateRoom("Main", new RoomOptions { MaxPlayers  = 0}, TypedLobby.Default);
        }
        else
        {
            //this.connectionInfoText.text = "�������� : ������ ������ ���� ��Ŵ \n �ٽ� ���� �õ��մϴ�.";
            //������ ������ ������ ���� ���� �õ�
            Debug.Log("������ ���� ��������........");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
/*    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        this.connectionInfoText.text = "�� �� ����, ���ο�� ����...";
        //�ִ� �ο��� 4������ ���� + ���� ����
        //���̸� , 4�� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });

    }
*/

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����!");

        //this.connectionInfoText.text = "�� ���� ����!";

        //��� �� �����ڰ� Main ���� �ε��ϰ� ��
        //PhotonNetwork.LoadLevel("Main");

        // ȣ��Ʈ�� �ƴϸ� ����
        /*        if(!PhotonNetwork.IsMasterClient)
                {
                    // Resources ������ �������� �־�� �Ѵ�
                    //PlayerInstance = PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity);
                    PlayerInstance = PhotonNetwork.Instantiate(this.playerPrefab.name, this.transform.position, this.transform.localRotation);

                    MainCamera.init(ref PlayerInstance);

                    //PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity);
                }*/

        // ���� ������ ������ ����
        //PlayerInstance = PhotonNetwork.Instantiate(this.playerPrefab.name, playerOrigin.transform.position, playerOrigin.transform.localRotation);
        PlayerInstance = PhotonNetwork.Instantiate("Player 1", playerOrigin.transform.position, playerOrigin.transform.localRotation);
        Destroy(playerOrigin);
        //MainCamera.ReTargeting(ref PlayerInstance);

        //Debug.Log(PhotonNetwork.NickName);

        //Debug.Log(Path.Combine(@"..\Prefabs", this.playerPrefab.name));

        //PhotonNetwork.Instantiate(Path.Combine(@"..\Prefabs", this.playerPrefab.name), Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity);

    }
}
