using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    // Start is called before the first frame update
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [Header("Movent Parameters")]
    [SerializeField] private float speed;

    private bool movingLeft;
    [Header("Animation")]
    [SerializeField]private Animator anim;
    private Vector3 initScale;
    [Header("Idle Behaviour")]
    [SerializeField]private float idleDuration;
    private float idletimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x )
            MoveInDirection(-1);
            else
            {
                //change dir
                DirectionChange();
            }

        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)
            MoveInDirection(1);
            else
            {
                DirectionChange();
            }

        }
    }
    private void MoveInDirection(int direction)
    {
        idletimer = 0;
        anim.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);


        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed
            ,enemy.position.y, enemy.position.z);
    }
    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idletimer += Time.deltaTime;

        if(idletimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
      
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

}
