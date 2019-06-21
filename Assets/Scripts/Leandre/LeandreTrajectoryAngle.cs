using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreTrajectoryAngle : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private Transform spriteTransform;

    private Rigidbody2D rb;
    private Collider2D col2d;
    private LeandrePlayerControl control;

    private float currentAngle = 0f;
    private float targetAngle = 0f;
    private float timeToRotate = 0.05f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        control = GetComponent<LeandrePlayerControl>();
    }

    private void Update()
    {
        bool isGrounded = Physics2D.IsTouchingLayers(col2d, groundLayer);

        if (isGrounded)
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = groundLayer;
            ContactPoint2D[] contacts = new ContactPoint2D[10];
            int nb = Physics2D.GetContacts(col2d, filter, contacts);
            if (nb == 1)
            {
                targetAngle = GetLandingAngle(contacts[0].normal);
            }
        }
        else
        {
            PlotTrajectory(transform.position, rb.velocity, 0.1f, 0.5f, ref targetAngle);
        }

        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime / timeToRotate);
        SetSpriteAngle(currentAngle);
    }

    public Vector2 PlotTrajectoryAtTime(Vector2 start, Vector2 startVelocity, float time)
    {
        return start + startVelocity * time + Physics2D.gravity * time * time * 0.5f;
    }

    public void PlotTrajectory(Vector2 start, Vector2 startVelocity, float timestep, float maxTime, ref float ret)
    {
        Vector2 prev = start;
        for (int i = 1; ; i++)
        {
            float t = timestep * i;
            if (t > maxTime) break;
            Vector2 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            RaycastHit2D hit;
            hit = Physics2D.Linecast(prev, pos, groundLayer);
            if (hit.collider != null)
            {
                //Gizmos.color = Color.blue;
                //Gizmos.DrawSphere(hit.point, 1f);
                ret = GetLandingAngle(hit.normal);
                return;
            }
            Debug.DrawLine(prev, pos, Color.red);
            prev = pos;
        }

        //ret = 0f;
    }

    private float GetLandingAngle(Vector2 normal)
    {
        return Vector2.SignedAngle(Vector2.up, normal);
    }

    private void SetSpriteAngle(float angle)
    {
        float finalAngle = control.GetDirection() == Direction.left ? -angle : angle;
        spriteTransform.localRotation = Quaternion.Euler(spriteTransform.localRotation.eulerAngles.x, spriteTransform.localRotation.eulerAngles.y, finalAngle);
    }
}
