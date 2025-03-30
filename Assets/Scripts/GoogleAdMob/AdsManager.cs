using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { get; private set; }

    private BannerView bannerView;
    private InterstitialAd interstitialAd;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob inicializado");
            LoadInterstitial();
        });
    }

    // === BANNER ===
    public void LoadBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // ID de prueba
        //string adUnitId = "ca-app-pub-4811061154023087/3727425771"; // ID REAL

        bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.BottomLeft);
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
    }

    public void ShowBanner() => bannerView?.Show();
    public void HideBanner() => bannerView?.Hide();

    // === INTERSTITIAL ===
    public void LoadInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712"; // ID de prueba
        //string adUnitId = "ca-app-pub-4811061154023087/7783787524"; // ID REAL

        var adRequest = new AdRequest();

        InterstitialAd.Load(adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.LogError("Error al cargar el interstitial: " + error);
                return;
            }

            interstitialAd = ad;
            Debug.Log("Interstitial cargado correctamente");
        });
    }

    public void ShowInterstitial(System.Action onAdClosed = null)
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial cerrado");
                onAdClosed?.Invoke();
                LoadInterstitial(); 
            };

            interstitialAd.Show();
            interstitialAd = null;
        }
        else
        {
            Debug.Log("Interstitial no listo, continuando sin anuncio");
            onAdClosed?.Invoke();
        }
    }


}
