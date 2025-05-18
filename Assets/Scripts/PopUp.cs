using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopUp : MonoBehaviour
{
    public static PopUp Instance; // Singleton para acceso global

    [Header("Popup Components")]
    [SerializeField] private TextMeshProUGUI titleText; // T�tulo del popup
    [SerializeField] private TextMeshProUGUI subtitleText; // Subt�tulo del popup
    [SerializeField] private Button yesButton; // Bot�n "S�"
    [SerializeField] private Button noButton; // Bot�n "No"

    private Action onYes; // Acci�n a ejecutar cuando el jugador presione "S�"
    private Action onNo; // Acci�n a ejecutar cuando el jugador presione "No"

    private void Awake()
    {
        // Configurar Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Ocultar popup al inicio
        gameObject.SetActive(false);

        // Configurar listeners iniciales
        yesButton.onClick.AddListener(() => OnYesButtonClicked());
        noButton.onClick.AddListener(() => OnNoButtonClicked());
    }

    /// <summary>
    /// Configura el popup con t�tulo, subt�tulo y acciones para los botones.
    /// </summary>
    /// <param name="title">T�tulo del popup</param>
    /// <param name="subtitle">Subt�tulo del popup</param>
    /// <param name="onYesAction">Acci�n para el bot�n "S�"</param>
    /// <param name="onNoAction">Acci�n para el bot�n "No"</param>
    public void Show(string title, string subtitle, Action onYesAction, Action onNoAction)
    {
        // Configurar texto
        titleText.text = title;
        subtitleText.text = subtitle;

        // Configurar acciones
        onYes = onYesAction;
        onNo = onNoAction;

        // Mostrar popup
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Oculta el popup.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnYesButtonClicked()
    {
        onYes?.Invoke(); // Ejecutar acci�n asociada
        Hide(); // Ocultar popup
    }

    private void OnNoButtonClicked()
    {
        onNo?.Invoke(); // Ejecutar acci�n asociada
        Hide(); // Ocultar popup
    }
}
