using UnityEngine;

public class Ground : MonoSingleton<Ground>
{
    [SerializeField] private Transform Ground1;
    [SerializeField] private Transform Ground2;

    private float lastPosition;

    public void Restart()
    {
        lastPosition = 0f;
        Ground1.localPosition = new Vector3(16f, 0f, 0f);
        Ground2.localPosition = new Vector3(80f, 0f, 0f);
    }

    private void Update()
    {
        float playerPos = PlayerControl.Instance.transform.position.x;

        if(playerPos > lastPosition + 64f)
        {
            Vector3 newPos = Ground1.localPosition;
            newPos.x += 64;
            Ground1.localPosition = newPos;

            newPos = Ground2.localPosition;
            newPos.x += 64;
            Ground2.localPosition = newPos;
            
            lastPosition = playerPos;
        }
    }
}
