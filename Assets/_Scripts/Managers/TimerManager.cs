using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static TimerManager _instance;
    public  static TimerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Init();
            }
            return _instance;
        }
    }

    private Dictionary<int, Timer> _timers = new Dictionary<int, Timer>();

    private static void Init()
    {
        GameObject timerMgrGo = GameObject.Find("TimerManager");
        if(timerMgrGo == null)
        {
            timerMgrGo = new GameObject() { name = "TimerManager" };
            timerMgrGo.AddComponent<TimerManager>();
        }
        _instance = timerMgrGo.GetComponent<TimerManager>();
        DontDestroyOnLoad(_instance.gameObject);
    }

    private void Update()
    {
        // 시간이 다된 타이머들
        List<Timer> completedTimers = new List<Timer>();
        foreach(var timer in _timers.Values)
        {
            timer.Tick();
            if (timer.IsDone)
                completedTimers.Add(timer);
        }

        foreach (var timer in completedTimers)
        {
            timer.OnTimer();
            _timers.Remove(timer.Uid);
        }
    }

    public void RegisterTimer(Timer timer, float second)
    {
        timer.SetTime(second);
        _timers.Add(timer.Uid, timer);
    }

    public void RemoveTimer(int Uid)
    {
        if (!_timers.ContainsKey(Uid))
            return;
        _timers.Remove(Uid);
    }

    public void RemoveTimer(Timer timer)
    {
        int targetUid = -1;
        foreach (int uid in _timers.Keys)
        {
            if (_timers[uid] != timer)
                continue;
            targetUid = uid;
            break;
        }

        if (targetUid == -1)
            return;

        RemoveTimer(targetUid);
    }

    public void Clear()
    {
        _timers.Clear();
    }
}
