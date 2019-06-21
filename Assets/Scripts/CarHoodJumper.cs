using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHoodJumper : MonoBehaviour
{
    [SerializeField]
    private float hoodForce = 50f;

    private BoxCollider2D bc2d;
    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            rb2d = player.GetComponent<Rigidbody2D>();
            rb2d.velocity = new Vector2(rb2d.velocity.x, hoodForce);
            anim.SetTrigger("Open Hood");
        }
    }
}
