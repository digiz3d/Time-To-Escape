using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldSwipeDetector : MonoBehaviour
{
    [SerializeField] private float threshold = 300f;
    private LeandrePlayerControl control;

    private Vector2 startPosition;
    private Vector2 direction;
    /// <summary> Temps d'attente pour les doubles sauts </summary>
    float MaxTimeWait = 1;
    float VariancePosition = 1;

    private float lastTouch = 0f;

    private bool isSliding = false;

    private void Start()
    {
        control = GetComponent<LeandrePlayerControl>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            //On récupère le mouvement
            Touch t = Input.GetTouch(0);

            switch (t.phase)
            {
                //Commence à toucher l'écran
                case TouchPhase.Began:
                    startPosition = t.position;

                    //Double tap
                    if (t.deltaTime > 0 && t.deltaTime < MaxTimeWait && t.deltaPosition.magnitude < VariancePosition)
                    {
                        //Répulsion si cooldown
                        control.Slide();
                    }
                    break;

                //En train de swiper
                case TouchPhase.Moved:
                    if (isSliding) break;
                    direction = t.position - startPosition;

                    //Déplacement horizontal
                    if (Mathf.Abs(direction.x) > threshold)
                    {
                        isSliding = true;
                        if (direction.x < 0)
                        {
                            //Aller à gauche
                            control.GoLeft();
                        }
                        else
                        {
                            //Aller à droite
                            control.GoRight();
                        }
                    }
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
