using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            //if(playerMovement.rb.linearVelocityY < -2)
            //{
            //    Debug.Log("asdf");
            //    Destroy(transform.parent.gameObject);
            //}
            playerMovement.numOfJumps = 1;
        }
    }
}
