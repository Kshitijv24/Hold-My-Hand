using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();

        if(PhotonNetwork.IsMasterClient == false)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    public void Restart()
    {
        photonView.RPC("RestartRPC", RpcTarget.All);
    }

    [PunRPC]
    private void RestartRPC()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
