using UnityEngine;
using Photon.Pun;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float dashTime;
    public float minX, maxX, minY, maxY;
    public TextMeshProUGUI nameDisplay;

    private PhotonView photonView;
    private Health healthScript;
    private LineRenderer lineRenderer;
    private float resetSpeed;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        healthScript = FindObjectOfType<Health>();
        lineRenderer = FindObjectOfType<LineRenderer>();
        resetSpeed = speed;

        if (photonView.IsMine)
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = photonView.Owner.NickName;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;

            transform.position += (Vector3)moveAmount;

            //Wrap();

            lineRenderer.SetPosition(0, transform.position);

            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                StartCoroutine("Dash");
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    //private void Wrap()
    //{
    //    if(transform.position.x < minX)
    //    {
    //        transform.position = new Vector2(maxX, transform.position.y);
    //    }

    //    if(transform.position.x > maxX)
    //    {
    //        transform.position = new Vector2(minX, transform.position.y);
    //    }

    //    if(transform.position.y < minY)
    //    {
    //        transform.position = new Vector2(maxY, transform.position.x);
    //    }

    //    if(transform.position.y > maxY)
    //    {
    //        transform.position = new Vector2(minY, transform.position.x);
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.tag == "Enemy")
            {
                healthScript.TakeDamage();
            }
        }
    }
}
