using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCollidingWithHead : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Crate") && this.gameObject.GetComponentInParent<PlayerController2D>().m_Grounded == true)
        {
            if (Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y) > 0.5 || Mathf.Abs(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x) > 0.5)
            {
                Events.Respawn();
            }
        }
    }
}
