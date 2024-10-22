using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataManager : MonoBehaviour
{
  public static DataManager instance;

  public TMP_InputField playerNameInput;
  public TextMeshProUGUI emptyNameWarning;

  public string playerName;
  public List<BestScore> bestScores;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
      DontDestroyOnLoad(gameObject);

      LoadGame();
      playerNameInput.text = playerName;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void StartGame()
  {
    playerName = playerNameInput.text;

    if (playerName != "")
    {
      SceneManager.LoadScene("main");
    }
    else
    {
      emptyNameWarning.gameObject.SetActive(true);
      StartCoroutine(HideEmptyNameWarning());
    }
  }

  private IEnumerator HideEmptyNameWarning()
  {
    yield return new WaitForSeconds(2.0f);
    emptyNameWarning.gameObject.SetActive(false);
  }

  public void ExitGame()
  {
    SaveGame();

#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
  }

  public void AddScore(string scoreHolder, int score)
  {
    BestScore bestScore = new BestScore(scoreHolder, score);
    bestScores.Add(bestScore);
    bestScores.Sort((a, b) => b.score.CompareTo(a.score));
    if (bestScores.Count > 3)
    {
      bestScores.RemoveAt(bestScores.Count - 1);
    }

    Debug.Log(bestScores);
  }


  [Serializable]
  class SaveData
  {
    public string playerName;
    public List<BestScore> bestScores;
  }

  public void SaveGame()
  {
    SaveData data = new SaveData();
    data.playerName = playerName;
    data.bestScores = bestScores;
    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
  }

  public void LoadGame()
  {
    string path = Application.persistentDataPath + "/playerData.json";
    if (File.Exists(path))
    {
      string json = File.ReadAllText(path);
      SaveData data = JsonUtility.FromJson<SaveData>(json);

      playerName = data.playerName;
      bestScores = data.bestScores ?? new List<BestScore>();
    }
  }
}