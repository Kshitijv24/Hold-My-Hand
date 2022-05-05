using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI healthDisplay;
    public GameObject gameOverPannel;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void TakeDamage()
    {
        photonView.RPC("TakeDamageRPC", RpcTarget.All);
    }

    [PunRPC]
    private void TakeDamageRPC()
    {
        health--;

        if(health <= 0)
        {
            health = 0;
            gameOverPannel.SetActive(true);
        }

        healthDisplay.text = health.ToString();
    }
}
