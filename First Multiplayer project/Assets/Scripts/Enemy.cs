using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject enemyDeathFx;

    private PlayerController[] players;
    private PlayerController nearestPlayer;
    private PhotonView photonView;
    private Score score;

    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        photonView = GetComponent<PhotonView>();
        score = FindObjectOfType<Score>();
    }

    private void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        if(distanceOne < distanceTwo)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }

        if(nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.tag == "GoldenRay")
            {
                score.AddScore();
                photonView.RPC("SpawnParticleRPC", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    private void SpawnParticleRPC()
    {
        Instantiate(enemyDeathFx, transform.position, Quaternion.identity);
    }
}
