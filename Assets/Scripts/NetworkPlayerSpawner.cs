using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
   public Transform playerSpawnPoint;
   private GameObject spawnedPlayerPrefab;

   public override void OnJoinedRoom()
   {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", playerSpawnPoint.position, playerSpawnPoint.rotation);
   }

   public override void OnLeftRoom()
   {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
   }
}
