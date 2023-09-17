using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class myGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText ;
    private int score ;
    private myBlade blade;
    private mySpawner spawner;
    public Image fadeImage;
     private void Awake()
    {
       blade = FindObjectOfType<myBlade>();
       spawner = FindObjectOfType<mySpawner>();
    }
    private void Start()
    {
        newGame();
    }
     void newGame()
    {
        Time.timeScale = 1f;
        blade.enabled = true;
        spawner.enabled = true;
        score = 0;
        scoreText.text = "Score:0";
        Time.timeScale = 1f;
        clearScene();
    }
    private void clearScene()
    {
        myFruit[] fruits = FindObjectsOfType<myFruit>();
        foreach(myFruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
        myBomb[] bombs = FindObjectsOfType<myBomb>();
        foreach (myBomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score:" + score;
    }
    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;
        StartCoroutine(ExplodeSequence());
       
        
    }
    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Fade to white
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);


        elapsed = 0f;
        GameMenu();
        // Fade back in
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("GameOver");
    }
   
}
