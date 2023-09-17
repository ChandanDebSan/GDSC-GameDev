using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    int score;
    public Button[] buttons;
    public bool gameOver;
    Scene currentScene;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
        if (sceneName != "menu")
        {
            score = 0;
            InvokeRepeating("scoreUpdate", 1.0f, 1f);
        }
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName != "menu") {
            scoreText.text = "score: " + score;
        }
        
    }
    void scoreUpdate()
    {
        if(!gameOver)
        {
            score += 1;
        }
        
    }
    public void gameOverActivated()
    {
        gameOver = true;
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);

        }
     }
    public void play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if(Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }
    public void replay()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void menu()
    {
        SceneManager.LoadScene("menu");
    }
    public void exit()
    {
        Application.Quit();
    }
}
