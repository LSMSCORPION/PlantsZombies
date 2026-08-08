using UnityEditor;
using UnityEngine;

public class CellSystem : MonoBehaviour
{
    public int GridSize = 2;

    public GameObject t = null;

    [SerializeField] private int _debugWidth;
    [SerializeField] private int _debugHeight;

    public Vector3 PointToCell(Vector3 point)
    {
        return new Vector3(
            Mathf.RoundToInt(point.x / GridSize) * GridSize,
            1.5f,
            Mathf.RoundToInt(point.z / GridSize) * GridSize
        );
    }

    private void DebugGrid()
    {
        for (int x = -50; x < 50; x += GridSize)
        {
            Debug.DrawLine(new Vector3(x, 1, 0), new Vector3(x, 1, 100), Color.red);
        }

        for (int z = -50; z < 50; z += GridSize)
        {
            Debug.DrawLine(new Vector3(0, 1, z), new Vector3(100, 1, z), Color.red);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        DebugGrid();
    }
}
