using System;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    public void PlayMenu()
    {
        AudioManager.Instance.PlayMenu();
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlayGame();
    }

    public void PlayEpic()
    {
        AudioManager.Instance.PlayEpic();
    }

}
