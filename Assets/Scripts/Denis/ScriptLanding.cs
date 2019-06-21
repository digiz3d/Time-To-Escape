using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptLanding : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem landing; //Particules de fumée

    [SerializeField]
    private AudioSource landingSound; //Son d'atterrissage

    //private bool landingEnabled = false; //L'atterrissage est-il actif ?

    private float totalDuration; //La durée de l'animation

    // Start is called before the first frame update
    void Start()
    {
        //On récupère la durée de vie de l'animation
        totalDuration = landing.main.duration;

        Landing();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Atterrisage : effet de fumée et son
    public void Landing()
    {
        //if (!landingEnabled)
        //{
        //Son d'atterrissage
        landingSound.Stop();
        landingSound.Play();

        //Particules à l'atterrisage
        landing.Stop();
        landing.Play();

        //On lance la coroutine
        //StartCoroutine(WaitLandingSeconds(totalDuration));
        //}
    }

    //Coroutine qui indique quand commence et termine l'animation
    IEnumerator WaitLandingSeconds(float duration)
    {
        //On indique que l'atterrissage commence
        //landingEnabled = true;

        //On attend la durée de vie de l'animation
        yield return new WaitForSeconds(duration);

        //On indique que l'atterrissage se termine
        //landingEnabled = false;
    }
}
