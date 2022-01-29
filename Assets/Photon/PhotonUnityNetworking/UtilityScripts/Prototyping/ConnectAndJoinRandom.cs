// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectAndJoinRandom.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Utilities, 
// </copyright>
// <summary>
//  Simple component to call ConnectUsingSettings and to get into a PUN room easily.
// </summary>
// <remarks>
//  A custom inspector provides a button to connect in PlayMode, should AutoConnect be false.
//  </remarks>                                                                                               
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

using UnityEngine;

//using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.UtilityScripts
{
    /// <summary>Simple component to call ConnectUsingSettings and to get into a PUN room easily.</summary>
    /// <remarks>A custom inspector provides a button to connect in PlayMode, should AutoConnect be false.</remarks>
    public class ConnectAndJoinRandom : MonoBehaviourPunCallbacks
    {
        public GameObject MyWizard;

        //SpawnPoints
        public Transform FireWizTransform;
        public Transform IceWizTransform;
        public Transform ThirdWizTransform;


        /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
        public bool AutoConnect = true;

        /// <summary>Used as PhotonNetwork.GameVersion.</summary>
        public byte Version = 1;

        /// <summary>Max number of players allowed in room. Once full, a new room will be created by the next connection attemping to join.</summary>
        [Tooltip("The max number of players allowed in room. Once full, a new room will be created by the next connection attemping to join.")]
        public byte MaxPlayers = 4;

        public int playerTTL = -1;

        public void Awake()
        {
            FireWizTransform = GameObject.Find("FireWizardSpawnPoint").GetComponent<Transform>();
            IceWizTransform = GameObject.Find("IceWizardSpawnPoint").GetComponent<Transform>();
            ThirdWizTransform = GameObject.Find("ThirdWizard").GetComponent<Transform>();
        }

        public void Start()
        {
            if (this.AutoConnect)
            {
                this.ConnectNow();
            }
        }

        public void ConnectNow()
        {
            Debug.Log("ConnectAndJoinRandom.ConnectNow() will now call: PhotonNetwork.ConnectUsingSettings().");


            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex;

        }


        // below, we implement some callbacks of the Photon Realtime API.
        // Being a MonoBehaviourPunCallbacks means, we can override the few methods which are needed here.


        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN. This client is now connected to Master Server in region [" + PhotonNetwork.CloudRegion +
                "] and can join a room. Calling: PhotonNetwork.JoinRandomRoom();");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby(). This client is now connected to Relay in region [" + PhotonNetwork.CloudRegion + "]. This script now calls: PhotonNetwork.JoinRandomRoom();");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available in region [" + PhotonNetwork.CloudRegion + "], so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

            RoomOptions roomOptions = new RoomOptions() { MaxPlayers = this.MaxPlayers };
            if (playerTTL >= 0)
                roomOptions.PlayerTtl = playerTTL;

            PhotonNetwork.CreateRoom(null, roomOptions, null);
        }

        // the following methods are implemented to give you some context. re-implement them as needed.
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("OnDisconnected(" + cause + ")");
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            CreatePlayer();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Destroy(MyWizard);
        }
        
        public void CreatePlayer()
        {
            int playerCount = PhotonNetwork.PlayerList.Length;
            Debug.Log(playerCount);
            if(playerCount == 1)
            {
                this.transform.position = IceWizTransform.position;
                this.transform.rotation = IceWizTransform.rotation;
                MyWizard = PhotonNetwork.Instantiate("IceWizard", IceWizTransform.transform.position, IceWizTransform.transform.rotation);
            }
            else if(playerCount == 2)
            {
                this.transform.position = FireWizTransform.position;
                this.transform.rotation = FireWizTransform.rotation;
                MyWizard = PhotonNetwork.Instantiate("FireWizard", FireWizTransform.transform.position, IceWizTransform.transform.rotation);
            }
            else
            {
                this.transform.position = ThirdWizTransform.position;
                this.transform.rotation = ThirdWizTransform.rotation;
                MyWizard = PhotonNetwork.Instantiate("FireWizard", ThirdWizTransform.transform.position, ThirdWizTransform.transform.rotation);
            }
        }
    }
}
