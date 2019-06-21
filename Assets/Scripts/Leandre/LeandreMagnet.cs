using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreMagnet : MonoBehaviour
{
    [SerializeField]
    private LeandrePlayerMagnet playerMagnet;

    private void Update()
    {

    }

    public void AttachPlayerMagnet(LeandrePlayerMagnet s)
    {
        playerMagnet = s;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMagnet.EnableField();
            Destroy(gameObject);
            collision.transform.GetChild(0).GetComponent<CircleCollider2D>().enabled = true;
            StartCoroutine(MagnetOn(collision.transform.GetChild(0).GetComponent<CircleCollider2D>()));

        }
    }

    IEnumerator MagnetOn (CircleCollider2D c)
    {
        yield return new WaitForSeconds(20);
        c.enabled = false;
    }
}
