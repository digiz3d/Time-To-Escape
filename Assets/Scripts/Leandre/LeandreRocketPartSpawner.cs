using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreRocketPartSpawner : MonoBehaviour
{
    public float minDistance = 5f;
    public float maxDistance = 10f;

    public GameObject[] objects;
    public LayerMask groundLayer;

    private float lastPositionX;
    private float nextPosition = 10f;

    [SerializeField]
    private float yOffset = 0.5f;

    [SerializeField]
    private AddShipPiece AddShipPieceToAttach;

    private GameObject GetRandomObject()
    {
        return objects[(int)Mathf.Ceil(Random.Range(0, objects.Length))];
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - lastPositionX < nextPosition) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 150f, groundLayer);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 150f, Color.red);
        if (hit.point != null && hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "End") return;
            float randomY = Random.Range(0f, 4f);
            GameObject go = Instantiate(GetRandomObject(), new Vector3(hit.point.x, hit.point.y + yOffset + randomY, 3f), Quaternion.identity);
            go.GetComponent<LeandreAutoDestroy>().SetObj(transform);
            go.GetComponent<LeandreRocketPart>().AttachShipPieceScript(AddShipPieceToAttach);
            lastPositionX = transform.position.x;
            nextPosition = Random.Range(minDistance, maxDistance);
        }
    }
}
