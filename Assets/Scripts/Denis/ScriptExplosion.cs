using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExplosion : MonoBehaviour
{
    [SerializeField]
    private float power; //La puissance de l'explosion

    [SerializeField]
    private ParticleSystem explosion; //Toutes les particules

    [SerializeField]
    private AudioSource meltdownSound; //Son du meltdown

    [SerializeField]
    private AudioSource explosionSound; //Son de l'explosion

    [SerializeField]
    private string tagItemAExploser; //Le tag des items à exploser

    private List<GameObject> InTrigger;

    private float totalDuration; //La durée de l'animation principale. A la fin, l'explosion se joue et c'est là que l'on doit repousser les items

    private bool explosionEnabled = false; //L'explosion est-elle active ?

    // Start is called before the first frame update
    void Start()
    {
        InTrigger = new List<GameObject>();

        //On récupère la durée de vie de l'animation
        totalDuration = explosion.main.duration/* + attraction.main.startLifetimeMultiplier*/;

        //On lance l'explosion
        Explode();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {
        if (!explosionEnabled)
        {
            //Jouer le son du meltdown
            meltdownSound.Stop();
            meltdownSound.Play();

            //Particules de l'explosion (contient le meltdown, les particules annexes, puis l'explosion)
            explosion.Stop();
            explosion.Play();

            //Jouer le son de l'explosion
            explosionSound.Stop();
            explosionSound.Play();

            //On lance la coroutine
            StartCoroutine(WaitExplosionSeconds(totalDuration));
        }
    }

    //Coroutine qui indique quand commence et termine l'animation
    IEnumerator WaitExplosionSeconds(float duration)
    {
        //On indique que l'attraction commence
        explosionEnabled = true;

        //On attend la durée de vie de l'animation
        yield return new WaitForSeconds(duration);
        
        //A la fin de l'explosion, les objets sont repoussés
        foreach (GameObject g in InTrigger)
        {
            //Todo
            g.GetComponent<Rigidbody2D>().AddForce((g.transform.position - transform.position + new Vector3(0, 1, 0)) * 30f, ForceMode2D.Impulse);
            g.transform.Rotate(0.75f, 0.0f, 0.0f);
            g.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        }

        //On indique que l'attraction se termine
        explosionEnabled = false;
    }

    //A chaque objet qui entre dans le rayon de l'explosion
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Foule")
        {
            InTrigger.Add(collision.gameObject);
        }

    }

    //A chaque objet qui sort du rayon de l'explosion
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Foule")
        {
            InTrigger.Remove(collision.gameObject);
        }
    }
}
