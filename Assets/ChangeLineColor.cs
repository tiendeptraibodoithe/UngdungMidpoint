using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLineColor : MonoBehaviour
{
    public Color color = Color.red; // Màu mới bạn muốn đặt cho đường

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        // Đặt màu cho toàn bộ đường
        lineRenderer.material.color = color;
    }
}
