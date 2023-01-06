using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{

   private GameObject spawnedPlayerPrefab;

   public override void OnJoinedRoom()
   {
       
        base.OnJoinedRoom();
        /*int[] xSpawnPoints = new int[]{100, -100, 200} ;
        int[] zSpawnPoints = new int[]{100, -100, -100};
        var index = Random.Range(0, xSpawnPoints.Length);
        XROrigin.transform.Translate(xSpawnPoints[index],0,zSpawnPoints[index]);*/
        /*spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player demo", transform.position, transform.rotation);*/
    
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
   }

   public override void OnLeftRoom()
   {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
   }
}
