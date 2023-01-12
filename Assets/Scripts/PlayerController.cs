using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks, IDamageable
{

    [SerializeField] Image healthbarImage;
    [SerializeField] GameObject ui;
    const float maxHealth = 100f;
    float currentHealth = maxHealth;
    private NetworkPlayer rb;

    private PhotonView PV;

    PlayerManager playerManager;
    
    void Awake()
    {
        rb = GetComponent<NetworkPlayer>();
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    void Start()
    {   
         if(!PV.IsMine)
         {
           Destroy(GetComponentInChildren<Camera>().gameObject);
           Destroy(rb);
           Destroy(ui);
         }
    }

    void Update()
    {
        if(!PV.IsMine)
            return;
        
        if ( Mathf.RoundToInt(rb.transform.position.x) >= -3
             && Mathf.RoundToInt(rb.transform.position.x)  <= 6 
             &&  Mathf.RoundToInt(rb.transform.position.z) >= 44
             &&  Mathf.RoundToInt(rb.transform.position.z) <= 55 )
        {
            playerManager.GainPoints();
        }

    }

    public void TakeDamage(float damage)
    {
        PV.RPC(nameof(RPC_TakeDamage), PV.Owner, damage);
    }
    
    [PunRPC]
    void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        if (!PV.IsMine)
            return;
		
        currentHealth -= damage;
        
        Debug.Log("currentHealth" + currentHealth);
        
        healthbarImage.fillAmount = currentHealth / maxHealth;
        Debug.Log(healthbarImage.fillAmount);
        if(currentHealth <= 0)
        {
            Die();
            PlayerManager.Find(info.Sender).GetKill();
        }
    }
    void Die()
    {
        playerManager.Die();
    }
}
