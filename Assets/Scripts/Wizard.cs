using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    protected string shotClassName = "WizardShot";
    protected virtual string ShotClassName { get => shotClassName; set => shotClassName = value; }

    public PhotonView photonView;

    public GameObject head;
    public GameObject lefthand;
    public GameObject righthand;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(photonView.IsMine && Input.GetKeyDown(KeyCode.C))
        {
            // Shot parameters
            Vector3 shotSpawnPoint = righthand.transform.position + (righthand.transform.forward * 0.25f);
            // Create shot and direct it
            GameObject shot = PhotonNetwork.Instantiate(ShotClassName, shotSpawnPoint, Quaternion.identity);
            shot.GetComponent<WizardShot>().SetLaunchParameters(shotSpawnPoint, righthand.transform.forward);
        }
    }
}
