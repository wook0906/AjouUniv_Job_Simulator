using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwincityMonsterStruggle : MonoBehaviour
{
    Animator animator;

    float timer;
    float timeStamp;

    private void Start()
    {
        animator = gameObject.GetOrAddComponent<Animator>();
        animator.Play("None");
        timeStamp = Random.Range(60f, 180f);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timeStamp < timer)
        {
            timer = 0f;
            timeStamp = Random.Range(60f, 180f);
            animator.Play("Struggle");
        }
    }
}
