using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Obstacle obstaclePrefab;
    public Transform obstacleNode;
    [HideInInspector] public int score;
    private float timeForNext;
    private readonly List<int> scoreTimeReduction = new List<int> { 5, 10, 20, 40, 60, 80 };


    private void Start()
    {
        Restart();
    }

    public void GameOver()
    {
        StopAllCoroutines();
        PlayerControl.Instance.gameObject.SetActive(false);
        UIManager.Instance.ShowRestartButton(true);
    }

    public void Restart()
    {
        score = 0;
        timeForNext = 4f;

        PlayerControl.Instance.gameObject.SetActive(true);

        ObstaclePool.ClearPool(obstacleNode);
        Ground.Instance.Restart();
        UIManager.Instance.Restart();

        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForNext);

            ObstaclePool.PlaceObstacle(obstaclePrefab);
        }
    }

    public void AddPoint()
    {
        ++score;
        if (scoreTimeReduction.Contains(score))
        {
            timeForNext -= 0.5f;
        }
        UIManager.Instance.UpdateScore(score);
    }
}
