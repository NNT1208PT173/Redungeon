using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalCoinDisplay : MonoBehaviour
{

    TextMeshProUGUI text;

    private void Awake()
    {
        text= GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        int totalCoin = PlayerPrefs.GetInt(Constant.totalCoin);
        text.text = "total: " + totalCoin.ToString("D4");
    }
}

