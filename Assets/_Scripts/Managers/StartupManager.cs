using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour {

    // Use this for initialization
    private IEnumerator Start () 
    {
        while (!Managers.Localization.GetIsReady ()) 
        {
            yield return null;
        }

        //SceneManager.LoadScene ("MenuScreen");
    }

}