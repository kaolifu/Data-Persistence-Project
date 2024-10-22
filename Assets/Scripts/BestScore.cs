using System;

[Serializable]
public class BestScore
{
  public string scoreHolderName;
  public int score;

  public BestScore(string scoreHolderName, int score)
  {
    this.scoreHolderName = scoreHolderName;
    this.score = score;
  }
}