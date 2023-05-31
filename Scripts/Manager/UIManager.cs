using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] TextMeshProUGUI bestDistance;

    private void Start()
    {
        UIEvents.updateDistanceEvent.AddListener(UpdateDistance);
        UIEvents.updateCoinEvent.AddListener(UpdateCoin);
        UIEvents.updateBestDistanceEvent.AddListener(UpdateBestDistance);
        UIEvents.updateTotalCoinEvent.AddListener(UpdateTotalCoin);

    }

    private void OnDestroy()
    {
        UIEvents.updateDistanceEvent.RemoveListener(UpdateDistance);
        UIEvents.updateCoinEvent.RemoveListener(UpdateCoin);
        UIEvents.updateBestDistanceEvent.RemoveListener(UpdateBestDistance);
        UIEvents.updateTotalCoinEvent.RemoveListener(UpdateTotalCoin);
    }


    private void UpdateDistance(int value)
    {
        distance.text = value.ToString() + "m";
    }

    private void UpdateCoin(int value)
    {
        coin.text = "+    " + value.ToString();
    }

    private void UpdateTotalCoin(int value) { 

    }


    private void UpdateBestDistance(int value)
    {
        int bestDist = PlayerPrefs.GetInt(Constant.bestDist);
        if (bestDist < value)
        {
            bestDist = value;
            PlayerPrefs.SetInt(Constant.bestDist, value);
            PlayerPrefs.Save();
            
        }

        bestDistance.text = "Best:" + bestDist + "m";

    }
}
