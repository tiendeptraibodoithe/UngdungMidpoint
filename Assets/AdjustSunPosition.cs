using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSunPosition : MonoBehaviour
{
    private void Awake()
    {
        // Tìm đối tượng có kích thước của elip (semiMajorAxis)
        GameObject solarSystem = GameObject.FindWithTag("SolarSystem");
        if (solarSystem != null)
        {
            EllipseOrbit ellipseOrbit = solarSystem.GetComponent<EllipseOrbit>();
            if (ellipseOrbit != null)
            {
                // Lấy giá trị semiMajorAxis từ script EllipseOrbit
                float semiMajorAxis = ellipseOrbit.semiMajorAxis;

                // Điều chỉnh vị trí x của Sun
                transform.position -= new Vector3(semiMajorAxis, 0, 0);
            }
            else
            {
                Debug.LogWarning("The SolarSystem object does not have the EllipseOrbit script attached.");
            }
        }
        else
        {
            Debug.LogWarning("No object with the tag 'SolarSystem' found in the scene.");
        }
    }
}
