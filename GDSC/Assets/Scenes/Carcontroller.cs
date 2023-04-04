using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Carcontroller : MonoBehaviour
{
    public Vector3 carPosition;
    public float carSpeed = 5f;
    public float maxLeft,maxRight;
    public UIManager ui;
    // Start is called before the first frame update
    void Start()
    {
        carPosition = transform.position;

    
    }

    // Update is called once per frame
    void Update()
    {
        carPosition.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        carPosition.x = Mathf.Clamp(carPosition.x, -1.9f, +2.5f);
        transform.position = carPosition;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            ui.gameOverActivated();
        }
    }
};