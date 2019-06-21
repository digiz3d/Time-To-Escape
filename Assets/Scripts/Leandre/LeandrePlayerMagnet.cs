using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandrePlayerMagnet : MonoBehaviour
{
    [SerializeField]
    private GameObject magnetEffect = null;

    [SerializeField]
    private float magneticFieldDuration = 20f;

    private float remainingTime = 0f;

    private ScriptMagnet scriptMagnet = null;

    private List<GameObject> inTrigger = null;

    private void Update()
    {
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0f)
            {
                DisableField();
            }
        }

    }

    public void EnableField()
    {
        remainingTime = magneticFieldDuration;
        Instantiate(magnetEffect, transform.localPosition, Quaternion.identity, transform);
    }

    public void DisableField()
    {
        
    } 
}
