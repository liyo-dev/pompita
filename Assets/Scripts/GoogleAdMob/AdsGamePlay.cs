using System;
using UnityEngine;

public class AdsGamePlay : MonoBehaviour
{
    void Start()
    {
        AdsManager.Instance.LoadBanner(); 
        AdsManager.Instance.ShowBanner();
    }

    private void OnDestroy()
    {
        AdsManager.Instance.HideBanner();
    }
}
