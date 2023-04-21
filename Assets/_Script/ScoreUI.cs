using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text ScoreText;
    public Text Distance;
    public GameObject EndPanel;
    public GameObject StartPanel;
    private void Start()
    {
        GameManager.instance.ScoreIncrement += WriteScore;
        GameManager.instance.WriteDistance += WriteDistance;
        GameManager.instance.OnGameEnd += DisplayEndPanel;
    }
    public void Play()
    {
        GameManager.instance.OnGameStart?.Invoke();
        StartPanel.SetActive(false);
    }
    void WriteScore()
    {
        GameManager.instance.Score++;
        if (GameManager.instance.Score % 20 == 0)
        {
            GameManager.Instance.Speed++;
        }
        ScoreText.text = "Coins=" + ((int)GameManager.instance.Score).ToString();
       
    }
    void WriteDistance()
    {
        Distance.text = "Distance=\n"+((int)GameManager.Instance.transform.position.z);
    }
    void DisplayEndPanel()
    {
        EndPanel.SetActive(true);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
