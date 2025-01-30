using UnityEngine;
using System;

[Serializable]

public class CoefficientGradient2D
{
    public CoefficientGradient[] gradients;

    [SerializeField]
    public CoefficientGradient2D(CoefficientGradient[] newApsl)
    {
        gradients = newApsl;
    }

    public float GetZ(float x, float y)
    {
        int len = gradients.Length;
        if (x < gradients[0].x)
        {
            return Mathf.Lerp(gradients[len - 1].GetZ(y), gradients[0].GetZ(y), (x - (gradients[len - 1].x - Mathf.PI * 2)) / (gradients[0].x - (gradients[len - 1].x - Mathf.PI * 2)));
        }
        if (x >= gradients[0].x && x < gradients[len - 1].x)
        {
            for (int i = 0; i < len - 1; i++)
            {
                if (x > gradients[i + 1].x)
                {
                    continue;
                }
                if (x >= gradients[i].x && x < gradients[i + 1].x)
                {
                    return Mathf.Lerp(gradients[i].GetZ(y), gradients[i + 1].GetZ(y), (x - gradients[i].x) / (gradients[i + 1].x - gradients[i].x));
                }
            }
        }
        if (x >= gradients[len - 1].x)
        {
            return Mathf.Lerp(gradients[len - 1].GetZ(y), gradients[0].GetZ(y), (x - gradients[len - 1].x) / (gradients[0].x + Mathf.PI * 2 - gradients[len - 1].x));
        }
        Debug.LogWarning("Didn't return a proper Coefficient");
        return 0;
    }
}
