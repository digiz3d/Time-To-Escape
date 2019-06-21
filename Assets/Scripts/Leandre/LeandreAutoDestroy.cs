using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreAutoDestroy : MonoBehaviour
{
    private float xToDestroy = -50f;

    private Transform obj;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - obj.transform.position.x < xToDestroy)
        {
            Destroy(gameObject);
        }
    }

    public void SetObj(Transform obj)
    {
        this.obj = obj;
    }
}
