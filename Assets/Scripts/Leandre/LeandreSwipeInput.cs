using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeandreSwipeInput : MonoBehaviour
{
    [SerializeField]
    private float threshold = 100f;
    private float timeBeforeTouch = 0.05f;
    private LeandrePlayerControl control = null;

    private Vector2 startPosition;
    private Vector2 direction;

    float MaxTimeWait = 1;
    float VariancePosition = 1;

    private float lastTouch = 0f;

    private bool isSliding = false;

    private float startTouching = 0f;
    private void Start()
    {
        control = GetComponent<LeandrePlayerControl>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            // touch start
            Touch t = Input.GetTouch(0);

            switch (t.phase)
            {
                //Commence à toucher l'écran
                case TouchPhase.Began:
                    startPosition = t.position;
                    startTouching = Time.time;
                    break;
                //case TouchPhase.Stationary:
                //    //if (Time.time - startTouching >= timeBeforeTouch)
                //    //{
                //        control.Jump();
                //    //}
                //    break;
                //En train de swiper
                case TouchPhase.Moved:
                    if (isSliding) break;
                    direction = t.position - startPosition;

                    //Déplacement vertical
                    if (Mathf.Abs(direction.y) > threshold)
                    {
                        isSliding = true;
                        if (direction.y < 0)
                        {
                            //Se mettre en boule / se décrocher
                            control.Slide();
                        }
                        else
                        {
                            //Sauter / s'accrocher
                            control.Jump();
                        }
                    }
                    else if (Mathf.Abs(direction.x) > threshold) //Déplacement horizontal
                    {
                        isSliding = true;
                        //if (direction.x < 0)
                        //{
                        //    //Aller à gauche
                        //    control.GoLeft();
                        //}
                        if (direction.x > 0)
                        {
                            //Aller à droite
                            control.GoRight();
                        }
                    }

                    break;

                //Fin du swipe
                case TouchPhase.Ended:
                    isSliding = false;
                    lastTouch = Time.time;
                    break;
            }
        }
    }

    void SetPosX(float x)
    {
        transform.localPosition = new Vector3(x, 0, 0);
    }

    void SetPosY(float y)
    {
        transform.localPosition = new Vector3(0, y, 0);
    }
}
