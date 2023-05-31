using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEvents
{
    public static UnityEvent<int> updateDistanceEvent = new UnityEvent<int>();
    public static UnityEvent<int> updateCoinEvent = new UnityEvent<int>();
    public static UnityEvent<int> updateBestDistanceEvent = new UnityEvent<int>();
    public static UnityEvent<int> updateTotalCoinEvent = new UnityEvent<int>();
}
