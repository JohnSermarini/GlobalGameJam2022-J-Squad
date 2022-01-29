using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public PhotonView photonView;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "XR Origin") 
        {
            if (photonView == null)
                photonView = gameObject.GetComponent<PhotonView>();
            
            photonView.RequestOwnership();
        }
    }
}
