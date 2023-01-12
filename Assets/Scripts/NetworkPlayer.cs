using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviour
{
    public Transform body;
    public Transform soldierBody;
    //public Transform leftHand;
    //public Transform rightHand;
    private PhotonView photonView;
    private Transform bodyRig;
    private PlayerManager playerManager;
    //private Transform rightHandRig;
    //private Transform leftHandRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        
        if (photonView.IsMine){
            
        XROrigin rig = FindObjectOfType<XROrigin>();
        GameObject spawnPoints = GameObject.Find("SpawnPoints");
        int random = UnityEngine.Random.Range(0, 2);
        GameObject spawnPoint = spawnPoints.transform.GetChild(random).gameObject;
        Vector3 newPosition = new Vector3(spawnPoint.transform.position.x, rig.transform.position.y,spawnPoint.transform.position.z);
        rig.transform.position = newPosition;
        bodyRig = rig.transform.Find("Camera Offset/Main Camera");
        
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (photonView.IsMine)
        {
            /*rightHand.gameObject.SetActive(true);
            leftHand.gameObject.SetActive(false);*/
            soldierBody.gameObject.SetActive(false);
            
            MapPosition(body, bodyRig);

			GameObject winPortal = GameObject.Find("WinPortal");

            Debug.Log(Mathf.RoundToInt(body.position.x) + " " + Mathf.RoundToInt(body.position.z) );
            
            if ( Mathf.RoundToInt(body.position.x) >= -3
                 && Mathf.RoundToInt(body.position.x)  <= 6 
                 &&  Mathf.RoundToInt(body.position.z) >= 44
                 &&  Mathf.RoundToInt(body.position.z) <= 55 )
            {
                Debug.Log("Win");
                playerManager.Win();
            }
           
            body.GetComponent<BoxCollider>().enabled = false;
            //MapPosition(rightHand, rightHandRig);
            //MapPosition(leftHand, leftHandRig);
        }
  
    }

    void MapPosition(Transform target, Transform rigTransform)
    {
          target.position = rigTransform.position;
          target.rotation = rigTransform.rotation;
    }
}
