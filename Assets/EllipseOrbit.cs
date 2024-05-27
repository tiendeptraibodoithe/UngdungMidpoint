using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class EllipseOrbit : MonoBehaviour
{
    public float semiMajorAxis = 1400f; // Trục bán chính của elip
    public float semiMinorAxis = 2000f; // Trục bán phụ của elip
    public float orbitSpeed = 100f; // Tốc độ quỹ đạo

    private LineRenderer lineRenderer;
    private List<Vector3> ellipsePoints = new List<Vector3>();
    private int currentPointIndex = 0;
    private bool isMovingAlongEllipse = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GenerateEllipsePoints();
        UpdateLineRenderer();
    }

    void Update()
    {
        MoveSunAlongEllipse();
    }

    void GenerateEllipsePoints()
    {
        float xc = transform.position.x;
        float yc = transform.position.y;

        int a = (int)semiMajorAxis;
        int b = (int)semiMinorAxis;

        int x = 0;
        int y = b;

        double a2 = a * a;
        double b2 = b * b;
        double twoa2 = 2 * a2;
        double twob2 = 2 * b2;
        double P = b2 - a2 * b + a2 / 4.0;

        // Vùng 1
        while (b2 * x <= a2 * y)
        {
            ellipsePoints.Add(new Vector3(x + xc, 0, y + yc));
            ellipsePoints.Add(new Vector3(-x + xc, 0, y + yc));
            ellipsePoints.Add(new Vector3(x + xc, 0, -y + yc));
            ellipsePoints.Add(new Vector3(-x + xc, 0, -y + yc));

            x++;
            if (P < 0)
            {
                P += b2 * (2 * x + 1);
            }
            else
            {
                y--;
                P += b2 * (2 * x + 1) - twoa2 * y;
            }
        }

        P = b2 * (x + 0.5) * (x + 0.5) + a2 * (y - 1) * (y - 1) - a2 * b2;

        // Vùng 2
        while (y > 0)
        {
            ellipsePoints.Add(new Vector3(x + xc, 0, y + yc));
            ellipsePoints.Add(new Vector3(-x + xc, 0, y + yc));
            ellipsePoints.Add(new Vector3(x + xc, 0, -y + yc));
            ellipsePoints.Add(new Vector3(-x + xc, 0, -y + yc));

            y--;
            if (P > 0)
            {
                P += a2 * (1 - 2 * y);
            }
            else
            {
                x++;
                P += a2 * (1 - 2 * y) + twob2 * x;
            }
        }

        // Sắp xếp các điểm theo thứ tự tuần tự quanh elip
        ellipsePoints.Sort((p1, p2) => {
            float angle1 = Mathf.Atan2(p1.z, p1.x);
            float angle2 = Mathf.Atan2(p2.z, p2.x);
            return angle1.CompareTo(angle2);
        });
    }

    void MoveSunAlongEllipse()
    {
        if (!isMovingAlongEllipse && ellipsePoints.Count > 1)
        {
            isMovingAlongEllipse = true;
            currentPointIndex = 1; // Bắt đầu từ điểm thứ 2 trên đường elip
        }

        if (isMovingAlongEllipse)
        {
            transform.position = Vector3.MoveTowards(transform.position, ellipsePoints[currentPointIndex], orbitSpeed * Time.deltaTime * 10);

            if (currentPointIndex == ellipsePoints.Count - 1 && Vector3.Distance(transform.position, ellipsePoints[currentPointIndex]) < 0.1f)
            {
                currentPointIndex = 0;
            }
            else if (Vector3.Distance(transform.position, ellipsePoints[currentPointIndex]) < 0.1f)
            {
                currentPointIndex++;
            }
        }
    }
    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = ellipsePoints.Count;
        lineRenderer.SetPositions(ellipsePoints.ToArray());
    }
}
