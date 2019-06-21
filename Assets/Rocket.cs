using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private BoxCollider2D bc2d;
    private Rigidbody2D rb2d;
    private bool takeOff = false;
    public float speed = 10.0f;

    public GameObject playerInRocket = null;

    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;
        if(takeOff == true)
        {
            rb2d.AddForce(new Vector2(0, 100));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            takeOff = true;
            playerInRocket.SetActive(true);
        }
    }
}
