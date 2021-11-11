using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionEquationSolver : MonoBehaviour
{

    public static Vector3 SolveForInitialVelocity(Vector3 s, Vector3 a, float t)
    {
        // u = (s -0.5*a*t^2)/t
        return (s - 0.5f * a * t * t) / t;
    }

    public static Vector3 SolveForDisplacement(Vector3 u, Vector3 a, float t)
    {
        // s= u*t + 0.5*a*t^2
        return (u * t) + (0.5f * a * t * t);
    }
        
}
