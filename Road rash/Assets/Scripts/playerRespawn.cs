using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerRespawn : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource DeathSoundEffect;
    [SerializeField] Transform checkPoint;
    private int timesRespawn = 2;
    [SerializeField] private Text respawnText;
    private void Start()
    {
        respawnText.text = "Respawn: " + timesRespawn;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("traps"))
        {
            Die();
        }
    }
    public void Die()
    {

        DeathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }
    private void RestartLeve()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Respawn()
    {
        if (timesRespawn <= 0)
        {
            RestartLeve();
        }
        transform.position = checkPoint.position;
        timesRespawn--;
        respawnText.text = "Respawn: " + timesRespawn;
        anim.SetTrigger("Respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
