using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreBackground : MonoBehaviour
{
    [SerializeField]
    private LeandrePlayerControl player;

    [SerializeField]
    private float relativeSpeed = 100f;

    private float currentXOffset = 0f;

    private MeshRenderer mesh;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        rb2d = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        currentXOffset += Time.deltaTime * player.GetCurrentSpeedX() * relativeSpeed;
        */
        if (player.GetCurrentSpeedX() > 5f)
        currentXOffset += Time.deltaTime * rb2d.velocity.x * relativeSpeed;
        mesh.sharedMaterial.SetTextureOffset("_MainTex", new Vector2(currentXOffset, 0));
    }
}
