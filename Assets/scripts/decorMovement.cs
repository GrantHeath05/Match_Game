using UnityEngine;

public class decorMovement : MonoBehaviour
{
    public float amplitude = 10f;     // How far it moves up/down
    public float frequency = 1f;      // How fast it moves

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0f, offset, 0f);
    }

}
