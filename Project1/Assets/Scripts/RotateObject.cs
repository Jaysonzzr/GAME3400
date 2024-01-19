using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);
    public float orbitSpeed = 30f;
    public float orbitDistance = 5f;
    public float moveSpeed = 5f;
    public float moveDistance = 2f;

    private Vector3 orbitCenter;
    private float orbitAngle = 0f;
    private float moveDirection = 1f;

    // Start is called before the first frame update
    void Start()
    {
        orbitCenter = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Move the object left and right
        float moveAmount = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        Vector3 movePosition = transform.right * moveAmount;

        // Orbit around a point
        orbitAngle += orbitSpeed * Time.deltaTime;
        float radian = orbitAngle * Mathf.Deg2Rad;
        Vector3 orbitPosition = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)) * orbitDistance;

        // Update position
        transform.position = orbitCenter + orbitPosition + movePosition;
    }
}
