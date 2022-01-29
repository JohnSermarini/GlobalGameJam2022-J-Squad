using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWizard : Wizard
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        shotClassName = "IceShot";
        spawnPoint = GameObject.Find("IceWizardSpawnPoint").transform.position;
        spawnRotation = GameObject.Find("IceWizardSpawnPoint").transform.rotation;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        /*
        if(photonView.IsMine && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("MoveToSpawn about to call");
            photonView.RPC("MoveToSpawn", RpcTarget.All);
        }
        */
    }
}
