using UnityEngine;

public static class Extension
{
    public static float GetCurveDerivative(this AnimationCurve curve, float time)
    {
        const float delta = 0.001f;

        float valueAtTimePlusDelta = curve.Evaluate(time + delta);
        float valueAtTimeMinusDelta = curve.Evaluate(time - delta);

        float derivative = (valueAtTimePlusDelta - valueAtTimeMinusDelta) / (2f * delta);

        return derivative;
    }
}
