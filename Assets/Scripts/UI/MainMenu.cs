using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(LoadGame);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
