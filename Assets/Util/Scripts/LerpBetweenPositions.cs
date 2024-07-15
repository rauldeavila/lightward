using UnityEngine;

public class LerpBetweenPositions : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float duration = 1.0f;
    public bool loop = true;

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, Mathf.SmoothStep(0f, 1f, t));
        transform.rotation = Quaternion.Lerp(startPos.rotation, endPos.rotation, Mathf.SmoothStep(0f, 1f, t));

        if (t >= 1.0f)
        {
            if (loop)
            {
                startTime = Time.time;
                Vector3 temp = startPos.position;
                startPos.position = endPos.position;
                endPos.position = temp;
                Quaternion tempRot = startPos.rotation;
                startPos.rotation = endPos.rotation;
                endPos.rotation = tempRot;
            }
        }
    }
}
