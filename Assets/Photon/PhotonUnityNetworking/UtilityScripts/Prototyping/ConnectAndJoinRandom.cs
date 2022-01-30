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
        public bool usingNewWizard = false;
        public GameObject MyWizard;
        public GameObject XROrigin;
        public GameObject Gem;

        //SpawnPoints
        public Transform FireWizTransform;
        public Transform IceWizTransform;
        public Transform ThirdWizTransform;
        public Transform GemSpawn;


        /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
        public bool AutoConnect = true;

        /// <summary>Used as PhotonNetwork.GameVersion.</summary>
        public byte Version = 1;

        [Tooltip("The max number of players allowed in room. Once full, a new room will be created by the next connection attemping to join.")]
        public byte MaxPlayers = 4;

        public int playerTTL = -1;

        public void Awake()
        {
            FireWizTransform = GameObject.Find("FireWizardSpawnPoint").GetComponent<Transform>();
            IceWizTransform = GameObject.Find("IceWizardSpawnPoint").GetComponent<Transform>();
            ThirdWizTransform = GameObject.Find("ThirdWizard").GetComponent<Transform>();
            GemSpawn = GameObject.Find("GemSpawnPoint").GetComponent<Transform>();
            XROrigin = GameObject.Find("XR Origin");
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
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex;

        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedLobby()
        {
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


        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            CreatePlayer();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            //if(MyWizard != null)
            //    Destroy(MyWizard.transform.parent);
            Destroy(MyWizard);
        }
        
        public void CreatePlayer()
        {
            int playerCount = PhotonNetwork.PlayerList.Length;
            if(playerCount == 1)
            {
                this.transform.position = IceWizTransform.position;
                this.transform.rotation = IceWizTransform.rotation;
                if(usingNewWizard)
                    MyWizard = PhotonNetwork.Instantiate("WizardModel", transform.position, transform.rotation);
                else
                    MyWizard = PhotonNetwork.Instantiate("IceWizard", transform.position, transform.rotation);

                // Spawn gem
                if(Gem == null)
                    Gem = PhotonNetwork.Instantiate("Gem", GemSpawn.position, GemSpawn.rotation);
            }
            else if(playerCount == 2)
            {
                this.transform.position = FireWizTransform.position;
                this.transform.rotation = FireWizTransform.rotation;
                if(usingNewWizard)
                    MyWizard = PhotonNetwork.Instantiate("WizardModel", transform.position, transform.rotation);
                else
                    MyWizard = PhotonNetwork.Instantiate("FireWizard", transform.position, transform.rotation);

            }
            else
            {
                this.transform.position = ThirdWizTransform.position;
                this.transform.rotation = ThirdWizTransform.rotation;
                if(usingNewWizard)
                    MyWizard = PhotonNetwork.Instantiate("WizardModel", transform.position, transform.rotation);
                else
                    MyWizard = PhotonNetwork.Instantiate("FireWizard", transform.position, transform.rotation);
            }
            XROrigin.transform.position = this.transform.position;
            XROrigin.transform.rotation = this.transform.rotation;
        }
    }
}
