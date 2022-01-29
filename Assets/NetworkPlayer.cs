using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        //head = transform.GetChild(0).Find("Main Camera");
        leftHand = transform.GetChild(0).Find("LeftHand Controller");
        rightHand = transform.GetChild(0).Find("RightHand Controller");
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            //head.gameObject.SetActive(false);
            //leftHand.gameObject.SetActive(false);
            //rightHand.gameObject.SetActive(false);

            //MapPosition(head, XRNode.Head);
            //MapPosition(leftHand, XRNode.LeftHand);
            //MapPosition(rightHand, XRNode.RightHand);
        }
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}
