using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMagnet : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem attraction; //Toutes les particules

    [SerializeField]
    private AudioSource magnetSound; //Son de l'attraction

    [SerializeField]
    private bool attractionEnabled = false; //L'attraction est-elle active ?

    private float totalDuration; //La durée de l'animation

    [SerializeField]
    private bool StartOnSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        //On récupère la durée de vie de l'animation
        totalDuration = attraction.main.duration/* + attraction.main.startLifetimeMultiplier*/;

        //On lance l'attraction
        if (StartOnSpawn) Enable();
    }

    public void Enable()
    {
        //Lancer l'animation des particules
        attraction.Stop();
        attraction.Play();

        //Lancer le son de l'attraction
        magnetSound.Stop();
        magnetSound.Play();

        //On lance la coroutine
        StartCoroutine(WaitMagnetSeconds(totalDuration));
    }

    //Coroutine qui indique quand commence et termine l'animation
    IEnumerator WaitMagnetSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
