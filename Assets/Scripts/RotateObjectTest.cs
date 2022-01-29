using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateObjectTest : MonoBehaviour
{
    public PhotonView photonView;
    private float rotationSpeed = 1.0f;
    private bool bRotatingZ;
    private bool bRotatingY;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(photonView != null)
        {
            // Rotate around Z Axis
            if(Input.GetMouseButtonDown(0))
            {
                photonView.RequestOwnership();

                // Track that the local object is being rotated and where the rotation starts from.
                bRotatingZ = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                bRotatingZ = false;
            }
            else if(bRotatingZ)
            {
                transform.Rotate(Vector3.up, rotationSpeed);
            }

            // Rotate around y axis
            if(Input.GetMouseButtonDown(1))
            {
                photonView.RequestOwnership();

                // Track that the local object is being rotated and where the rotation starts from.
                bRotatingY = true;
            }
            else if(Input.GetMouseButtonUp(1))
            {
                bRotatingY = false;
            }
            else if(bRotatingY)
            {
                transform.Rotate(Vector3.right, rotationSpeed);
            }
        }
    }
}

