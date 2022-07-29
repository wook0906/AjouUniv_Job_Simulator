using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer
{
    private static int nextVaildUid = 0;

    public int      Uid { get; private set; } // 타이머 삭제 시 식별을 위한 정
    public float    Time { get; private set; } // 초 단위
    public bool     IsDone { get; private set; }

    public Timer()
    {
        Uid = Timer.nextVaildUid++;
    }

    public void     Tick()
    {
        Time -= UnityEngine.Time.deltaTime;
        if (Time <= 0)
            IsDone = true;
    }

    public void     SetTime(float second /* =초 단위 */)
    {
        Time = second;
    }

    public abstract void OnTimer();
}
