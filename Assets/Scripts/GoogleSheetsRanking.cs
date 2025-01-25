using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class RankingWrapper
{
    public List<PlayerScore> ranking;
}

public class GoogleSheetsRanking : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankingText;
    
    public UnityEvent OnRankingUpdated;
    
    private string apiUrl = "https://script.google.com/macros/s/AKfycbzG7n8NBewXTedQXRe6qKVGvKAAik4n_IoRjEhXoCeWANHgDxkLOSt778B_0s4O_JphVQ/exec";

    private void Start()
    {
        ObtenerRanking();
    }

    public void EnviarPuntuacion(string nombre, int puntuacion)
    {
        StartCoroutine(EnviarPuntuacionCoroutine(nombre, puntuacion));
    }

    IEnumerator EnviarPuntuacionCoroutine(string nombre, int puntuacion)
    {
        WWWForm form = new WWWForm();
        form.AddField("nombre", nombre);
        form.AddField("puntuacion", puntuacion.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(apiUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                Debug.Log("Puntuación enviada correctamente");
            else
                Debug.LogError("Error al enviar: " + www.error);
        }
    }
    
    public void ObtenerRanking()
    {
        StartCoroutine(ObtenerRankingCoroutine());
    }

    IEnumerator ObtenerRankingCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonData = www.downloadHandler.text;
                Debug.Log("📥 Datos Recibidos: " + jsonData);

                // Deserializar usando el wrapper
                RankingWrapper rankingData = JsonUtility.FromJson<RankingWrapper>("{\"ranking\":" + jsonData + "}");

                if (rankingData != null && rankingData.ranking != null)
                {
                    foreach (var entry in rankingData.ranking)
                    {
                        Debug.Log($"🏆 Jugador: {entry.nombre} - Puntos: {entry.puntuacion}");
                        rankingText.text += $"{entry.nombre} - {entry.puntuacion}\n";
                    }
                    
                    OnRankingUpdated.Invoke();
                }
                else
                {
                    Debug.LogError("⚠ Error al deserializar JSON. Verifica el formato.");
                }
            }
            else
            {
                Debug.LogError("❌ Error al obtener ranking: " + www.error);
            }
        }
    }

}
