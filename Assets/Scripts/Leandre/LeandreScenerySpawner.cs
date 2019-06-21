using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreScenerySpawner : MonoBehaviour
{
    public float minDistance = 5f;
    public float maxDistance = 10f;

    public GameObject[] objects;
    public LayerMask groundLayer;

    private float lastPositionX;
    private float nextPosition = 10f;


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
        if (hit.point != null)
        {
            GameObject go = Instantiate(GetRandomObject(), new Vector3(hit.point.x, hit.point.y, 12f) /*hit.point*/, Quaternion.identity);
            go.GetComponent<LeandreAutoDestroy>().SetObj(transform);
            lastPositionX = transform.position.x;
            nextPosition = Random.Range(minDistance, maxDistance);
        }
    }
}
