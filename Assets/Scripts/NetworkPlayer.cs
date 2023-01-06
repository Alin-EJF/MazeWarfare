using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    //public Transform leftHand;
    //public Transform rightHand;
    private PhotonView photonView;

    private Transform headRig;
    //private Transform rightHandRig;
    //private Transform leftHandRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView= GetComponent<PhotonView>();
        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig= rig.transform.Find("Camera Offset/Main Camera");
        // rightHandRig= rig.transform.Find("Camera Offset/RightHand Controller");
       //  leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");

    }

    // Update is called once per frame
    void Update()
    {   
        if (photonView.IsMine)
        {
            //rightHand.gameObject.SetActive(true);
            //leftHand.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
            
            MapPosition(head, headRig);
            //MapPosition(rightHand, rightHandRig);
            //MapPosition(leftHand, leftHandRig);
        }
;
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}