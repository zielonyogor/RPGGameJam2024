using System;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] TextMeshProUGUI equipmentText;
    [SerializeField] GameObject gaeOverHUD;
    [SerializeField] Volume screwUpVolume;

    public int anomalies = 0;
    public int maxAnomalies = 10;
    public int equipment = 0;
    public int maxEquipment = 5;

    private float timeSinceStart = 0;
    private bool isPlaying = true;

    void Start()
    {
        gaeOverHUD.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlaying) timeSinceStart += Time.deltaTime;
        counterText.text = "anomalies: " + Math.Min(anomalies, maxAnomalies) + "/" + maxAnomalies;
        float screwUpPercentage = (float)(anomalies - 1) / maxAnomalies;
        screwUpVolume.weight = screwUpPercentage;
        if (anomalies >= maxAnomalies)
        {
            isPlaying = false;
            gaeOverHUD.gameObject.SetActive(true);
            gaeOverHUD.GetComponent<GameOver>().ChangeScore(timeSinceStart); //tu dac jakis score, czas??
        }

        equipmentText.text = "inventory: " + Math.Min(equipment, maxEquipment) + "/" + maxEquipment;
    }
}
