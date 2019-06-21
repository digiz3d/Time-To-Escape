using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    AudioSource ButtonClickSound;
    public GameObject HelpPanel;
    public GameObject ButJeuPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickPlay()
    {
        ButtonClickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClickAide()
    {
        ButtonClickSound.Play();
        HelpPanel.SetActive(true);
    }

    public void RetourClick()
    {
        ButtonClickSound.Play();
        DisablePanels();
    }

    public void ClickButJeu()
    {
        ButtonClickSound.Play();
        ButJeuPanel.SetActive(true);
    }

    private void buttonClickPlaysound()
    {
        ButtonClickSound.Play();
    }

    private void DisablePanels()
    {
        HelpPanel.SetActive(false);
        ButJeuPanel.SetActive(false);
    }
}
