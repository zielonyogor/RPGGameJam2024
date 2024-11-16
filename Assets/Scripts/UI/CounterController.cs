using UnityEngine;
using TMPro;

public class CounterController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText; 
    public int anomalies = 0;
    public int maxAnomalies = 10;

    void Start()
    {
        
    }

    void Update()
    {
        counterText.text = "anomalies: " + anomalies + "/" + maxAnomalies;
    }
}
