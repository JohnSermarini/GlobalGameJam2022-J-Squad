using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private Game_Manager gameManager;
    public float dropHeight = 3.0f;

    public PhotonView photonView;
    public SphereCollider collider;
    public Rigidbody rb;
    public GameObject crystal;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager>();

        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        crystal = transform.GetChild(0).gameObject;

        //photonView.RPC("DropGemAtPosition", RpcTarget.All, transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collision with player
        if (collision.gameObject.name == "XR Origin")
        {
            Debug.LogError("XR Origin");
            //if (photonView == null)
            //photonView = gameObject.GetComponent<PhotonView>();

            photonView.RequestOwnership();

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                string playerWizardName = "";
                PhotonView playerPhotonView = players[i].transform.GetChild(0).GetComponent<PhotonView>();
                if (playerPhotonView == null)
                {
                    return;
                }
                if (playerPhotonView.IsMine == true) // Player grabbed Gem
                {
                    Debug.LogError("mine XR Origin");

                    playerWizardName = playerPhotonView.transform.parent.name;
                    //Wizard wizard = playerPhotonView.gameObject.GetComponent<Wizard>();
                    photonView.RPC("PickUpGem", RpcTarget.All, playerWizardName);//PickUpGem(wizard);
                }
            }
        }
    }

    [PunRPC]
    public void PickUpGem(string wizardName)
    {
        Debug.Log(wizardName + " picked up gem!");

        collider.enabled = false;
        rb.useGravity = false;
        crystal.SetActive(false);

        PhotonView gameManagerPhotonView = gameManager.GetComponent<PhotonView>();
        gameManagerPhotonView.RPC("GemGrabbed", RpcTarget.All, wizardName);
    }


    //[PunRPC]
    //public void DropGemAtPosition(string xPos, string yPos, string zPos)
    //{
    //    //Unparent if necessary
    //    Vector3 dropPosition = new Vector3(float.Parse(xPos), (float.Parse(yPos) + dropHeight), (float.Parse(zPos)));
    //    transform.position = dropPosition;

    //    collider.enabled = true;
    //    rb.useGravity = true;
    //    crystal.SetActive(true);
    //}

    //[PunRPC]
    //public void DropGem(string wizardNameWhoHadGem)
    //{
    //    collider.enabled = true;
    //    rb.useGravity = true;
    //    crystal.SetActive(true);

    //    // Drop Gem at player location
    //    // Get wizard object from name
    //    /*
    //    Wizard wizard = null;
    //    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    //    for(int i = 0; i < players.Length; i++)
    //    {
    //        if(players[i].name == wizardNameWhoHadGem)
    //        {
    //            wizard = players[i].GetComponentInChildren<Wizard>();
    //        }
    //    }
    //    if(wizard == null)
    //    {
    //        Debug.LogError("ERROR: Wizard " + wizardNameWhoHadGem + " cannot drop Gem because it cannot be found! Shit!");
    //    }
    //    */
    //    Wizard wizard = Wizard.GetWizardUsingName(wizardNameWhoHadGem);
    //    Vector3 wizardPosition = wizard.head.transform.position;
    //    photonView.RPC("DropGemAtPosition", RpcTarget.All, wizardPosition.x.ToString(), wizardPosition.y.ToString(), wizardPosition.z.ToString());

    //    PhotonView gameManagerPhotonView = gameManager.GetComponent<PhotonView>();
    //    gameManagerPhotonView.RPC("GemDropped", RpcTarget.All);
    //}


    public void DropGemAtPosition(Vector3 position, bool isIce)
    {
        //Unparent if necessary
        Debug.Log("Drop DropGemAtPosition called at " + position);
        transform.position = position;

        photonView.RPC("FixComponentOnDrop", RpcTarget.All);

        PhotonView gameManagerPhotonView = gameManager.GetComponent<PhotonView>();
        gameManagerPhotonView.RPC("GemDropped", RpcTarget.All, isIce);
    }

    [PunRPC]
    public void FixComponentOnDrop()
    {
        collider.enabled = true;
        rb.useGravity = true;
        crystal.SetActive(true);
    }
}
