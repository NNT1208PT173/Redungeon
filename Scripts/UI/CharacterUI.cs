using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] RectTransform content;

    [SerializeField] RectTransform[] rectTransforms;

    int playerIndex;

    private void Start()
    {
        playerIndex = 0;
    }

    public void OnPlayGame()
    {
        Debug.Log(1);
        playerIndex =Mathf.Abs((int)( content.anchoredPosition.x / rectTransforms[0].rect.width));
        PlayerPrefs.SetInt(Constant.playerIndex,playerIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(2);
    }

    public void OnPrev()
    {
        if (content.anchoredPosition.x == 0) {
            content.anchoredPosition = new Vector3( -content.rect.width + rectTransforms[1].rect.width, content.anchoredPosition.y);
        }
        else
        {
            content.anchoredPosition = new Vector3(content.anchoredPosition.x + rectTransforms[1].rect.width, content.anchoredPosition.y);
        }
    }
    public void OnNext()
    {
        if (content.anchoredPosition.x == -content.rect.width + rectTransforms[1].rect.width)
        {
            content.anchoredPosition = new Vector3(0, content.anchoredPosition.y);
        }
        else
        {
            content.anchoredPosition = new Vector3(content.anchoredPosition.x - rectTransforms[1].rect.width, content.anchoredPosition.y);
        }
    }

    public void OnReturn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
