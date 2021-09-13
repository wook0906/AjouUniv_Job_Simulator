using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokyoHologramWall : MonoBehaviour
{
    public bool isShowing = true;

    public void Move(bool moveDown)
    {
        StartCoroutine(CorMove(moveDown));    
    }
    IEnumerator CorMove(bool moveDown)
    {
        if (moveDown)
        {
            Vector3 targetPos = transform.localPosition;
            targetPos.y -= 1.5f;
            float timer = 0f;
            while (timer <= 1.5f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
            isShowing = false;
        }
        else
        {
            Vector3 targetPos = transform.localPosition;
            targetPos.y += 1.5f;
            float timer = 0f;
            while (timer <= 1.5f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
            isShowing = true;
        }
    }
}
