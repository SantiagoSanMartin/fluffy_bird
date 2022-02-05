using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject star;
    private const float SPAWN_DISTANCE = 16f;
    private const float SPAWN_MAX_UP = 3f;
    private const float SPAWN_MAX_DOWN = -1.5f;

    public void Recycle()
    {
        Vector3 newPos = PlayerControl.Instance.transform.position + Vector3.right * SPAWN_DISTANCE;
        newPos.y = Random.Range(SPAWN_MAX_DOWN, SPAWN_MAX_UP);
        transform.position = newPos;
        gameObject.SetActive(true);

        if(Random.value > 0.93f)
        {
            star.SetActive(true);
            star.transform.localPosition += Vector3.up * Mathf.Sign(Random.Range(-1f, 1f));
        }
        else
        {
            star.SetActive(false);
        }
    }

    private void Update()
    {
        if(transform.position.x - PlayerControl.Instance.transform.position.x < -SPAWN_DISTANCE)
        {
            gameObject.SetActive(false);
        }
    }
}
