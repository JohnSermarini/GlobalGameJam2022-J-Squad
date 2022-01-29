using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public PhotonView photonView;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError(this.gameObject.name + " collider entered " + collision.gameObject.name);
        if (collision.gameObject.name == "XR Rig") 
        {
            if (photonView == null)
                photonView = gameObject.GetComponent<PhotonView>();
            photonView.RequestOwnership();
        }
    }
}
