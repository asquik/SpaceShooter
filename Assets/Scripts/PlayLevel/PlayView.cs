using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : MonoBehaviour
{
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text loseWinText;
    [SerializeField]
    private Text timerText;
    private float timerTime;
    private float beginnignTime;

    public void SetTimer(int time)
    {
        timerTime = time;
        beginnignTime = Time.time;
    }

    private void Update()
    {
        float time = Time.time - beginnignTime;
        if (time <= timerTime)
        {
            //timerText.text = "To win: " + (10 - time).ToString();
            timerText.text = "To victory:\n" + string.Format("{0:0.00}", (10 - time));
        }
        else
        {
            timerText.text = "To victory:\n " + "0";
        }
    }

    public void UpdateView(int health)
    {
        livesText.text = "Lives: " + health.ToString();
    }

    public void ShowLoseText()
    {
        loseWinText.text = "You lost";
        loseWinText.gameObject.SetActive(true);
    }

    public void ShowVictoryText()
    {
        loseWinText.text = "You won";
        loseWinText.gameObject.SetActive(true);
    }
}
