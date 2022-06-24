using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour
{
    //Points that form the line segment
    public Transform p1;
    public Transform p2;

    //Control points that dictate the curve
    public Transform c1;
    public Transform c2;

    //Interpolated point that moves along the curve/line
    public Transform p3;

    [Range(0f, 1f)]
    public float _t; 
    [Range(1, 27)]
    public int _ControlPoints;

    [ExecuteAlways]
    void OnDrawGizmos()
    {
        //Draw points
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p1.position, 0.15f);
        Gizmos.DrawSphere(p2.position, 0.15f);
        Gizmos.DrawSphere(c1.position, 0.15f);
        Gizmos.DrawSphere(c2.position, 0.15f);

        //Draw interp point
        p3.position = Lerp(p1.position, p2.position, _t);
        //Gizmos.DrawSpp


        //Draw line
        Gizmos.color = Color.white;
        Gizmos.DrawLine(p1.position, p2.position);

    }

    private void BezierInterp()
    {
        //The gist of bezier is to interpolate between a->cp1->cp2...->cpn->b, where cp is a control point of which there are n
        //then drawing a line to connect each interapolated point between segments and interpolating that with the same T value
        //this winds down until there is on line segment left to interpolate, at which point the linear interp will be on a curve
    }

    private float Lerp(float aA, float aB, float aT)
    {
        // ... = a(t) + (1-t)b
        return (aB * aT) + ((1 - aT) * aA);
    }
    private Vector2 Lerp(Vector2 aA, Vector2 aB, float aT)
    {
        return new Vector2( 
            Lerp(aA.x, aB.x, aT),   //x component
            Lerp(aA.y, aB.y, aT)    //y component
            );
    }
    private Vector3 Lerp(Vector3 aA, Vector3 aB, float aT)
    {
        return new Vector3(
            Lerp(aA.x, aB.x, aT),   //x component
            Lerp(aA.y, aB.y, aT),   //y component
            Lerp(aA.z, aB.z, aT)    //z component
            );
    }
}
