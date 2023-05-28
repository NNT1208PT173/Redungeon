using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        InputEvents.exit.AddListener(OnExit);
    }

    private void OnDestroy()
    {
        InputEvents.exit.RemoveListener(OnExit);
    }


    public void OnExit()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
                    Application.Quit();
        #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("QuitScene");
        #endif
    }
}
