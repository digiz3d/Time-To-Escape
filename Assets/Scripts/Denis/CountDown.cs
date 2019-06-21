using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    float tempsTotal = 300.0f;
    bool isFinished = false;
    public GameObject GameOverPanel;
    [SerializeField]
    private int levelToLoad;
    //[SerializeField]
    //TextMesh textTemps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished) return;

        tempsTotal -= Time.deltaTime;
        GetComponent<TextMeshProUGUI>().SetText(Mathf.Round(tempsTotal).ToString());

        if (tempsTotal < 0)
        {
            isFinished = true;

            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //Appelé par le bouton "rejouer". Charge le 1er level
    public void ReturnToLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelToLoad);
    }
}
