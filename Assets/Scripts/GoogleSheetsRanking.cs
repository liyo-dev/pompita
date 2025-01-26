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
    public static GoogleSheetsRanking Instance { get; private set; }
    private TextMeshProUGUI rankingTextPrefab;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject ScrollView;
    public UnityAction OnRankingUpdated;

    private string apiUrl = "https://script.google.com/macros/s/AKfycbyMK-ZvWhvDgxf681dUt4m9n1dFBnAvdf9_G3ss49zR0LH8jIfPizgasQPz9YmnTbHjKw/exec";

    private List<string> top5List = new List<string>();
    public int currentScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rankingTextPrefab = GameObject.FindGameObjectWithTag("TextRanking").GetComponent<TextMeshProUGUI>();
    }

    public void Reset()
    {
        ScrollView.SetActive(false);
        rankingTextPrefab.text = "";
        top5List.Clear();
        currentScore = 0;

        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void GetTop5()
    {
        Reset(); // Asegura un estado limpio antes de la petición
        StartCoroutine(GetTop5Coroutine());
    }

    private IEnumerator GetTop5Coroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonData = www.downloadHandler.text;
                Debug.Log($"📥 JSON recibido: {jsonData}");

                RankingWrapper rankingData = JsonUtility.FromJson<RankingWrapper>("{\"ranking\":" + jsonData + "}");

                if (rankingData != null && rankingData.ranking != null && rankingData.ranking.Count > 0)
                {
                    rankingData.ranking.Sort((a, b) => b.puntuacion.CompareTo(a.puntuacion));
                    top5List.Clear();

                    int count = 0;
                    foreach (var entry in rankingData.ranking)
                    {
                        if (count >= 5) break;
                        Debug.Log($"🏆 Jugador: {entry.nombre} - Puntos: {entry.puntuacion}");
                        top5List.Add($"{entry.nombre} - {entry.puntuacion}");
                        count++;
                    }

                    if (OnRankingUpdated != null) OnRankingUpdated.Invoke();
                }
                else
                {
                    Debug.LogError("⚠ Error al deserializar JSON. Verifica el formato.");
                }
            }
            else
            {
                Debug.LogError($"❌ Error HTTP: {www.responseCode}, Detalle: {www.error}");
            }
        }
    }

    public List<string> GetTop5String()
    {
        return top5List;
    }

    public void EnviarPuntuacion(string nombre, int puntuacion)
    {
        StartCoroutine(EnviarPuntuacionCoroutine(nombre, puntuacion));
    }

    private IEnumerator EnviarPuntuacionCoroutine(string nombre, int puntuacion)
    {
        PlayerScore data = new PlayerScore { nombre = nombre, puntuacion = puntuacion };
        string jsonData = JsonUtility.ToJson(data);

        Debug.Log($"🛠 JSON que se enviará: {jsonData}");

        using (UnityWebRequest www = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"📨 Respuesta del servidor: {www.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"❌ Error al enviar puntuación: {www.error}");
            }
        }
    }

    public void ObtenerRanking()
    {
        Reset(); // Asegura que no haya datos duplicados
        ScrollView.SetActive(true);
        StartCoroutine(ObtenerRankingCoroutine());
    }

    private IEnumerator ObtenerRankingCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonData = www.downloadHandler.text;
                Debug.Log($"📥 Datos recibidos del servidor: {jsonData}");

                RankingWrapper rankingData = JsonUtility.FromJson<RankingWrapper>("{\"ranking\":" + jsonData + "}");

                if (rankingData == null || rankingData.ranking == null || rankingData.ranking.Count == 0)
                {
                    Debug.LogError("⚠ Error: La lista de ranking está vacía o no se pudo deserializar correctamente.");
                    yield break;
                }

                foreach (Transform child in contentPanel)
                {
                    Destroy(child.gameObject);
                }

                rankingData.ranking.Sort((a, b) => b.puntuacion.CompareTo(a.puntuacion));
                int i = 1;

                foreach (var entry in rankingData.ranking)
                {
                    Debug.Log($"🏆 Jugador: {entry.nombre} - Puntos: {entry.puntuacion}");

                    if (contentPanel != null && rankingTextPrefab != null)
                    {
                        TextMeshProUGUI newText = Instantiate(rankingTextPrefab, contentPanel);
                        newText.text = $"{i}.- {entry.nombre} - {entry.puntuacion}";
                        newText.enabled = true;
                        i++;
                    }
                    else
                    {
                        Debug.LogError("❌ Error: contentPanel o rankingTextPrefab es nulo.");
                    }
                }

                if (OnRankingUpdated != null) OnRankingUpdated.Invoke();
            }
            else
            {
                Debug.LogError($"❌ Error al obtener ranking: {www.error}");
            }
        }
    }
}
