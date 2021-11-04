using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volt_DontDestroyPanel : MonoBehaviour
{
    public NetworkErrorType NetErrorType = NetworkErrorType.Resolved;
    public static Volt_DontDestroyPanel S;
    public Volt_NetworkErrorPanel ErrorPanel;
    float timer = 0;
    private void Awake()
    {
        if (S == null)
        {
            DontDestroyOnLoad(this.gameObject);
            S = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (NetErrorType == NetworkErrorType.InternetNonReachable)
            return;

        if (NetErrorType == NetworkErrorType.InternetInstable)
        {
            TimeCheck();
            return;
        }
        else if (NetErrorType == NetworkErrorType.Resolved)
            TimeReset();

    }
    void TimeCheck()
    {
        timer += Time.deltaTime;
        if (timer >= 10f)
        {
            NetErrorType = NetworkErrorType.InternetNonReachable;
        }
    }
    void TimeReset()
    {
        timer = 0f;
    }
    public void NetworkErrorHandle(NetworkErrorType errorType)
    {
        Debug.Log($"ErrorType@@@@@@@@@@@@@@@@@@@@ : {errorType}");
        if (this.NetErrorType == errorType) return;

        Debug.Log($"ErrorType!!!!!!!!!!!!!!!!!!!!! : {errorType}");
        ErrorPanel.gameObject.SetActive(true);
        ErrorPanel.ShowErrorMsg(errorType);
        this.NetErrorType = errorType;
    }

    public void OnDisconnected()
    {
        Volt_NetworkErrorPanel.S.ShowErrorMsg(NetworkErrorType.InternetNonReachable);
    }
    public EventDelegate OnPressDownDisconnectConfirmBtn()
    {
        Application.Quit();
        return null;
    }
}
