using UnityEngine;

public class AdsGameOver : MonoBehaviour
{
    public GameObject LoadGame;
    public GameObject LoadMenu;

    public void ShowInterstitialAdAndGoToGame()
    {
        AdsManager.Instance.ShowInterstitial(() =>
        {
            LoadGame.SetActive(true);
        });
    }
    
    public void ShowInterstitialAdAndGoToMenu()
    {
        AdsManager.Instance.ShowInterstitial(() =>
        {
            LoadMenu.SetActive(true);
        });
    }
}
