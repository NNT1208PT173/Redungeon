using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int playerDistance = 0;
    [SerializeField] Vector3 playerStartPosition;
    [SerializeField] int playerCoin = 0;

    [SerializeField] GameObject[] Characters;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt(Constant.playerIndex));
        Characters[PlayerPrefs.GetInt(Constant.playerIndex)].SetActive(true);
        
    }

    private void Start()
    {
        int bestDist = PlayerPrefs.GetInt(Constant.bestDist);

        playerCoin = 0;
        playerStartPosition = PlayerMoveController.Instant.transform.position;

        InputEvents.exitEvent.AddListener(OnExit);
        UIEvents.updateBestDistanceEvent.Invoke(bestDist);

    }

    private void FixedUpdate()
    {
        CalculateVerticalDistance();
    }

    private void CalculateVerticalDistance()
    {
        playerDistance = Mathf.Max(playerDistance, (int)(PlayerMoveController.Instant.transform.position.y - playerStartPosition.y));
        UIEvents.updateDistanceEvent.Invoke(playerDistance);
        UIEvents.updateBestDistanceEvent.Invoke(playerDistance);
    }

    public void UpdateCoin(int value)
    {
        playerCoin += value;
        UIEvents.updateCoinEvent.Invoke(playerCoin);
        PlayerPrefs.SetInt(Constant.totalCoin, value + PlayerPrefs.GetInt(Constant.totalCoin));
        PlayerPrefs.Save();
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
