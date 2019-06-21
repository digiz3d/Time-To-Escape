using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreEndBlock : MonoBehaviour
{
    [SerializeField]
    private LeandreCameraFollowsPlayer cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.tag == "End")
        {
            cam.StopFollowing();
        }
    }

    public void AttachCamera(LeandreCameraFollowsPlayer cam)
    {
        this.cam = cam;
    }
}
