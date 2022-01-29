using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    protected string shotClassName = "WizardShot";
    protected Vector3 spawnPoint = Vector3.zero;
    protected Quaternion spawnRotation = Quaternion.identity;

    public PhotonView photonView;
    public GameObject head;
    public GameObject lefthand;
    public GameObject righthand;

    private GameObject xrOrigin;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        photonView = GetComponent<PhotonView>();

        xrOrigin = GameObject.Find("XR Origin");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(photonView.IsMine && Input.GetKeyDown(KeyCode.C))
        {
            // Shot parameters
            Vector3 shotSpawnPoint = righthand.transform.position + (righthand.transform.forward * 0.25f);
            // Create shot and direct it
            GameObject shot = PhotonNetwork.Instantiate(shotClassName, shotSpawnPoint, Quaternion.identity);
            shot.GetComponent<WizardShot>().SetLaunchParameters(shotSpawnPoint, righthand.transform.forward);
        }
    }

    [PunRPC]
    protected void MoveToSpawn()
    {
        Debug.Log("MoveToSpawn called on " + transform.parent.name);
        xrOrigin.transform.position = spawnPoint;
        //transform.rotation = spawnRotation;
    }
}
