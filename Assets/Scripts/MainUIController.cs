using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
  private DataManager dataManager;
  public Text bestScoreText;

  void Start()
  {
    dataManager = GameObject.Find("DataManager")?.GetComponent<DataManager>();

    if (dataManager != null && dataManager.bestScores.Count > 0)
    {
      bestScoreText.text = $"Best Score:{dataManager.bestScores[0].score} Name:{dataManager.playerName}";
    }
    else if (dataManager != null)
    {
      bestScoreText.text = $"Best Score: Name:{dataManager.playerName}";
    }
  }

  void Update()
  {
  }
}