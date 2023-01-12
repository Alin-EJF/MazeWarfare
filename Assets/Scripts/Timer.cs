using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviourPunCallbacks
{
    private float countdown = 120;
    private PhotonView PV;
    public TMP_Text secondsText;
    private bool timeIsUp = false;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown = countdown - Time.deltaTime;

        secondsText.text = countdown.ToString("F0");
        
        if (countdown <= 0)
        {
            timeIsUp = true;
        }

        if (timeIsUp)
        {
            PV.RPC("ShowEndGame", RpcTarget.All);
        }
    }

    [PunRPC]
    public void ShowEndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Winner");
    }
}
