using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAN : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float fly = 0f;
    [SerializeField] private AudioSource JumpSoundEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            JumpSoundEffect.Play();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fly, ForceMode2D.Impulse);
        }
    }

}
