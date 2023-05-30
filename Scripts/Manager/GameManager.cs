using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float playerDistance = 0;
    [SerializeField] Vector3 playerStartPosition;
    [SerializeField] int coin = 0;

    private void Start()
    {
        InputEvents.exitEvent.AddListener(OnExit);
        playerStartPosition = PlayerMoveController.Instant.transform.position;
    }

    private void FixedUpdate()
    {
        playerDistance = PlayerMoveController.Instant.transform.position.y - playerStartPosition.y;
    }

    private void OnDestroy()
    {
        InputEvents.exitEvent.RemoveListener(OnExit);
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
