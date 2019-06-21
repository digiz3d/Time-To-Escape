using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreRocketPart : MonoBehaviour
{
    private AddShipPiece addShipPieceScript;

    private Transform playerTransform = null;

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, 0.11f);
        }
    }

    public void AttachShipPieceScript(AddShipPiece s)
    {
        addShipPieceScript = s;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            addShipPieceScript.AddPiece();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Magnet")
        {
            playerTransform = collision.gameObject.transform;
        }
    }
}
