using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{

    private float timer = 0.5f;
    private SpriteRenderer ghostSpriteRenderer;
    public GameObject playerGameObject;


    // Start is called before the first frame update
    void Start()
    {
        ghostSpriteRenderer = GetComponent<SpriteRenderer>();
        ghostSpriteRenderer.color = new Vector4(50, 50, 50, 0.2f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
