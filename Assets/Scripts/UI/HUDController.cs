using System;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] TextMeshProUGUI gaeOverText;
    [SerializeField] Volume screwUpVolume;

    public int anomalies = 0;
    public int maxAnomalies = 10;

    void Start()
    {
        gaeOverText.gameObject.SetActive(false);
    }

    void Update()
    {
        counterText.text = "anomalies: " + Math.Min(anomalies, maxAnomalies) + "/" + maxAnomalies;
        float screwUpPercentage = (float) (anomalies - 1) / maxAnomalies;
        screwUpVolume.weight = screwUpPercentage;
        if (anomalies >= maxAnomalies)
        {
            gaeOverText.gameObject.SetActive(true);
        }
    }
}
