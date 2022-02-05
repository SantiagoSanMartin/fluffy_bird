using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ObstaclePool
{
    private static List<Obstacle> obstacleInstances = new List<Obstacle>();
    private static int index = 0;
    private static readonly Vector3 instancePosition = new Vector3(0f, -20f, 0f);

    public static Obstacle PlaceObstacle(Obstacle obstacle)
    {
        Obstacle obstacleInstance = obstacleInstances.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obstacleInstance is null)
        {

            obstacleInstance = Object.Instantiate(obstacle, instancePosition, Quaternion.identity).GetComponent<Obstacle>();
            obstacleInstances.Add(obstacleInstance);
            ++index;
            obstacleInstance.name = obstacleInstance.name + " - " + index;
            obstacleInstance.transform.SetParent(GameManager.Instance.obstacleNode);
        }

        obstacleInstance.Recycle();

        return obstacleInstance;
    }

    public static void ClearPool(Transform obstacleNode)
    {
        index = 0;
        obstacleInstances.Clear();
        obstacleNode.DestroyChildren();
    }
}
