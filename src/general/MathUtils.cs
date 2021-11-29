﻿using System;
using Godot;

/// <summary>
///   Math related utility functions for Thrive
/// </summary>
public static class MathUtils
{
    public const float EPSILON = 0.00000001f;
    public const float DEGREES_TO_RADIANS = Mathf.Pi / 180;

    public static T Clamp<T>(this T val, T min, T max)
        where T : IComparable<T>
    {
        if (val.CompareTo(min) < 0)
        {
            return min;
        }

        if (val.CompareTo(max) > 0)
        {
            return max;
        }

        return val;
    }

    public static double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

    /// <summary>
    ///   Creates a rotation for an organelle. This is used by the editor, but PlacedOrganelle uses RotateY as this
    ///   didn't work there for some reason.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Rotation is now the number of 60 degree rotations
    ///   </para>
    /// </remarks>
    public static Quat CreateRotationForOrganelle(float rotation)
    {
        return new Quat(new Vector3(0, -1, 0), rotation * 60 * DEGREES_TO_RADIANS);
    }

    /// <summary>
    ///   This still takes the angle in degrees as this is used from
    ///   places that calculate the angle in degrees.
    /// </summary>
    public static Quat CreateRotationForExternal(float angle)
    {
        return new Quat(new Vector3(0, 1, 0), 180 * DEGREES_TO_RADIANS) *
            new Quat(new Vector3(0, 1, 0), angle * DEGREES_TO_RADIANS);
    }

    /// <summary>
    ///   Rotation for the pilus physics cone
    /// </summary>
    public static Quat CreateRotationForPhysicsOrganelle(float angle)
    {
        return new Quat(new Vector3(-1, 0, 0), 90 * DEGREES_TO_RADIANS) *
            new Quat(new Vector3(0, 0, -1), (180 - angle) * DEGREES_TO_RADIANS);
    }

    /// <summary>
    ///   Returns a Lerped value, and snaps to the target value if current and target
    ///   value is approximately equal by the specified tolerance value.
    /// </summary>
    public static float Lerp(float from, float to, float weight, float tolerance = Mathf.Epsilon)
    {
        if (Mathf.IsEqualApprox(from, to, tolerance))
            return to;

        return Mathf.Lerp(from, to, weight);
    }

    public static int Factorial(int f)
    {
        if (f == 0)
            return 1;

        return f * Factorial(f - 1);
    }

    public static int NCr(int n, int r)
    {
        return NCr(n, Factorial(n), r);
    }

    public static int NCr(int n, int nFactorial, int r)
    {
        return nFactorial / (Factorial(r) * Factorial(n - r));
    }

    public static Vector3 ToVector3(this Vector2 vector, float y = 0)
    {
        return new Vector3(vector.x, y, vector.y);
    }

    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
}
