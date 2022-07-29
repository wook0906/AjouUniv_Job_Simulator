using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 로그인 시도 후 일정 시간동안 응답 패킷이 오지 않으면
 * 로그인 실패 처리를 하는 타이머
 */
public class LoginTimer : Timer
{
    public LoginTimer()
        : base()
    {

    }
    public override void OnTimer()
    {
        // 로그인 실패!

        // 로그인 관련 캐쉬 데이터 제거
        PlayerPrefs.DeleteKey("Volt_LoginType");
#if UNITY_IOS
        PlayerPrefs.DeleteKey("APPLE_SIGNIN");
#endif

        // 안내 팝업 오
        Managers.UI.ShowPopupUIAsync<SigninFailTimeOut_Popup>();
    }
}
