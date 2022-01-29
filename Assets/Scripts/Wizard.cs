using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject head;
    public GameObject lefthand;
    public GameObject righthand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Shot! Gang gang!!");
            // Shot parameters
            Vector3 shotSpawnPoint = righthand.transform.position + (righthand.transform.forward * 0.25f);
            //Quaternion shotAngle = Quaternion.Euler(righthand.transform.forward);
            // Create shot and direct it
            GameObject shot = PhotonNetwork.Instantiate("WizardShot", shotSpawnPoint, Quaternion.identity);
            shot.GetComponent<WizardShot>().SetLaunchParameters(shotSpawnPoint, righthand.transform.forward);
            //shot.transform.parent = this.transform;
            //shot.GetComponent<WizardShot>().LaunchShot(shotSpawnPoint, shot.transform.forward);
        }
    }
}
