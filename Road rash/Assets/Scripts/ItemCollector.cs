using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int banana = 0;
    [SerializeField] private Text bananasText;
    [SerializeField] private AudioSource collectSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("banana"))
        {
            Destroy(collision.gameObject);
            collectSoundEffect.Play();
            banana++;
            bananasText.text = "Bananas: " + banana;
        }
    }
}
