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
    public int combo = 0;
    public GameObject comboTextPrefab;
  
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
       
        if (combo > 1)
        {
            score += combo;
            scoreText.text = "Score:" + score;
        }
        else
        {
            score++;
            scoreText.text = "Score:" + score;
        }
        
    }
    public void Explode(AudioSource bombSource)
    {
        blade.enabled = false;
        spawner.enabled = false;
        bombSource.Play();
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
        if (combo >= 5)
        {
            LeanTween.moveLocal(comboTextPrefab, new Vector3(-54.8f, 5.6f, 0f), 1.5f).setDelay(0.1f).setEase(LeanTweenType.easeOutQuart);
            LeanTween.scale(comboTextPrefab, new Vector3(1f, 1f, 1f), 1.5f).setDelay(0.1f).setEase(LeanTweenType.easeOutCubic);
            LeanTween.moveLocal(comboTextPrefab, new Vector3(-54.8f, 202f, 0f), 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutQuart);
        }
        else if (combo == 0) 
        {
            LeanTween.scale(comboTextPrefab, new Vector3(1f, 1f, 1f), 0f).setDelay(0.1f).setEase(LeanTweenType.easeOutCubic);
        }
        UpdateComboText();
    }
    public void UpdateComboText()
    {
        comboScore.text = "+"+ combo.ToString() + " Combo"  ;
    }
    public void ResetCombo()
    {
        combo = 0;
        comboTimeRemaining = comboTimer;
        iscomboActive = false;
       
        UpdateComboText();
        LeanTween.scale(comboTextPrefab, Vector3.zero, 1.5f).setEase(LeanTweenType.easeOutCubic);
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
