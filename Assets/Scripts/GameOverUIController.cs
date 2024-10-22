using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
  public GameObject scoreList;
  public GameObject scoreTextPrefab;

  private DataManager dataManager;

  void Start()
  {
    dataManager = GameObject.Find("DataManager")?.GetComponent<DataManager>();

    if (dataManager != null && dataManager.bestScores.Count > 0)
    {
      for (int i = 0; i < dataManager.bestScores.Count; i++)
      {
        var scoreTextGo = Instantiate(scoreTextPrefab, scoreList.transform);
        var name = dataManager.bestScores[i].scoreHolderName;
        var score = dataManager.bestScores[i].score;
        scoreTextGo.GetComponent<TextMeshProUGUI>().text = $"{i + 1} Score: {score} Name: {name}";
      }
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      SceneManager.LoadScene("main");
    }
    else if (Input.GetKeyDown(KeyCode.Escape))
    {
      dataManager.ExitGame();
    }
  }
}