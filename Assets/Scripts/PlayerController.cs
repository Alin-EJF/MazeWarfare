using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviour, IDamageable
{
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    public Image healthbar;
    private Rigidbody rb;

    private PhotonView PV;

    PlayerManager playerManager;
    
  

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {   
         if(!PV.IsMine)
         {
           Destroy(GetComponentInChildren<Camera>().gameObject);
           Destroy(rb);
         }
    }

    void Update()
    {
        if(!PV.IsMine)
            return;
    }
    
    public void TakeDamage(float damage)
    {
        PV.RPC(nameof(RPC_TakeDamage), PV.Owner, damage);
    }
    
    [PunRPC]
    void RPC_TakeDamage(float damage)
    {
        if (!PV.IsMine)
            return;
		
        currentHealth -= damage;

        Debug.Log("currentHealth" + currentHealth);
        
        /*healthbarImage.fillAmount = currentHealth / maxHealth;*/

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playerManager.Die();
    }
}
