using TMPro;
using UnityEngine;

public class VersionDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    void Start()
    {
        versionText.text = "Versión: " + Application.version;
    }
}