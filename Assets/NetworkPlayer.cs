using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    public bool usingNewWizard;
    public Transform head;
    public Transform lefthand;
    public Transform righthand;
    public PhotonView photonView;

    public Transform headRig;
    public Transform leftHandRig;
    public Transform rightHandRig;

    private void Start()
    {
        headRig = GameObject.Find("Main Camera").GetComponent<Transform>();
        leftHandRig = GameObject.Find("LeftRig").GetComponent<Transform>();
        rightHandRig = GameObject.Find("RightRig").GetComponent<Transform>();

        if (usingNewWizard)
        {
            //ParentConstraint head_PC = head.GetComponent<ParentConstraint>();
            //head_PC.constraintActive = false;
            //ConstraintSource csHead = new ConstraintSource();
            //csHead.sourceTransform = headRig.transform;
            //head_PC.SetSource(0, csHead);
            //head_PC.constraintActive = true;

            ConstraintSource csRight = new ConstraintSource();
            ParentConstraint rightHand_PC = righthand.GetComponent<ParentConstraint>();
            rightHand_PC.constraintActive = false;
            csRight.sourceTransform = rightHandRig.transform;
            rightHand_PC.SetSource(0, csRight);
            rightHand_PC.constraintActive = true;

            ConstraintSource csLeft = new ConstraintSource();
            ParentConstraint leftHand_PC = lefthand.GetComponent<ParentConstraint>();
            leftHand_PC.constraintActive = false;
            csLeft.sourceTransform = leftHandRig.transform;
            leftHand_PC.SetSource(0, csLeft);
            leftHand_PC.constraintActive = true;
        }
    }

    void Update()
    {
        if (photonView.IsMine) 
        {
            //righthand.gameObject.SetActive(false);
            //lefthand.gameObject.SetActive(false);
            //head.gameObject.SetActive(false);

            MapPosition(head, headRig);
            MapPosition(righthand, rightHandRig);
            MapPosition(lefthand, leftHandRig);
        }
    }

    void MapPosition(Transform target, Transform rigTransform) 
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
