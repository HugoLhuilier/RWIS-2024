using UnityEngine;


[RequireComponent(typeof(GyroscopeReader))]
public class RotationCircleGyroVisualizer : MonoBehaviour
{
    [SerializeField] private Transform baseCircle;
    private float rotRadius;

    private GyroscopeReader reader;

    // Start is called before the first frame update
    void Start()
    {
        reader = GetComponent<GyroscopeReader>();

        rotRadius = baseCircle.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = reader.getTilt() * rotRadius;
        transform.localPosition = new Vector3(newPos.x, newPos.y, 0);
    }
}
