using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreCameraFollowsPlayer : MonoBehaviour
{
    [SerializeField]
    private LeandrePlayerControl control;
    [SerializeField]
    private float aheadCameraDistance = 15f;
    [SerializeField]
    private float verticalOffset = 0f;

    [SerializeField]
    private float smoothTimeX = 0.2f;
    [SerializeField]
    private float smoothTimeY = 0.2f;

    [SerializeField]
    private LayerMask groundLayer;

    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;


    private Vector2 target;

    private bool stopped = false;
    public void StopFollowing()
    {
        stopped = true;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (stopped) return;

        RaycastHit2D hit;
        float offsetY = 0f;
        float playerY = 0f;
        switch (control.GetDirection())
        {
            case Direction.right:
                hit = Physics2D.Raycast(new Vector2(control.transform.position.x, control.transform.position.y + 10f), Vector3.down, 99f, groundLayer);
                if (hit.collider != null)
                {
                    playerY = hit.point.y;
                }


                hit = Physics2D.Raycast(new Vector2(control.transform.position.x + aheadCameraDistance, playerY + 10f), Vector3.down, 15f, groundLayer);
                Debug.DrawLine(
                    new Vector2(control.transform.position.x + aheadCameraDistance, playerY + 10f),
                    new Vector2(control.transform.position.x + aheadCameraDistance, playerY + 10f - 15f),
                    Color.red);

                if (hit.collider == null)
                {
                    offsetY = -7f;
                }
                target = new Vector2(control.transform.position.x + aheadCameraDistance, playerY + offsetY);
                break;

            default:
                target = new Vector2(control.gameObject.transform.position.x, playerY + offsetY);
                break;
        }

        float posX = Mathf.SmoothDamp(transform.position.x, target.x, ref xVelocity, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, target.y + verticalOffset, ref yVelocity, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(control.transform.position.x + aheadCameraDistance, control.transform.position.y + 10f), 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(control.transform.position.x - aheadCameraDistance, control.transform.position.y + 10f), 0.5f);

        Gizmos.color = Color.magenta;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(new Vector2(control.transform.position.x + aheadCameraDistance, control.transform.position.y), Vector3.down, 40f, groundLayer);
        if (hit.point != null)
        {
            Gizmos.DrawLine(
                new Vector3(control.transform.position.x + aheadCameraDistance, control.transform.position.y, transform.position.z),
                new Vector3(control.transform.position.x + aheadCameraDistance, control.transform.position.y - hit.distance, transform.position.z)
            );
        }

    }


}
