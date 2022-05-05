using UnityEngine;
using Photon.Pun;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;

    public int score = 0;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void AddScore()
    {
        photonView.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    private void AddScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
