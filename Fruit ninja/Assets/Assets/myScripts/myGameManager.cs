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
    public Text comboScore;
    private int score ;
    private float comboTimeRemaining = 0f;
    private bool iscomboActive = false; 
    [SerializeField] float comboTimer = 7f;
    private myBlade blade;
    private mySpawner spawner;
    public Image fadeImage;
    private int combo = 0;
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
        comboScore.text = "Combo:0";
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
        GameMenu();
        
        
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("GameOver");
    }
    
    //Combo Logic Starts Here
    public void StartComboTimer()
    {
        if (!iscomboActive)
        {
            iscomboActive = true;
            comboTimeRemaining = comboTimer;
        }
        else
        {
            Incrementcombo();
        }
    }
    public void Incrementcombo()
    {
        combo++;
        UpdateComboText();
    }
    public void UpdateComboText()
    {
        comboScore.text = "Combo:" + combo.ToString();
    }
    public void ResetCombo()
    {
        combo = 0;
        comboTimeRemaining = comboTimer;
        iscomboActive = false;  
        UpdateComboText();
    }
 
    public void Update()
    {
        if (iscomboActive)
        {
            comboTimeRemaining -= Time.deltaTime;

            if (comboTimeRemaining <= 0.0f)
            {
                ResetCombo();
            }
        }
    }
    //Combo logic ends here
}
