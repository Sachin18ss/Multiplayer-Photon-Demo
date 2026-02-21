using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float speed = 5f;

    void Update()
    {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0f, v) * speed *Time.deltaTime);
   }

}
