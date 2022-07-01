using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
public class BezierCurve : MonoBehaviour
{
    //Points that form the line segment
    public Transform point1;
    public Transform point2;

    //Control points that dictate the curve
    public Transform control1;
    public Transform control2;

    //Interpolated point that moves along the curve/line
    public Transform point3;

    //Draw path of bezier curve?
    public bool drawBezierCurve;

    

    public Transform[] VisualisationPoints;

    [Range(0f, 1f)]
    public float _t; 
    [Range(1, 27)]
    public int _ControlPoints;

    [ContextMenu("fill variables")]
    private void FillVariables()
    {
        //Look for points & control points, auto fill every time application is selected (to avoid running every tick)
        if (point1 == null)
        {
            if (transform.Find("p1"))
                point1 = transform.Find("p1");
        }
        if (point2 == null)
        {
            if (transform.Find("p2"))
                point2 = transform.Find("p2");
        }
        if (point3 == null)
        {
            if (transform.Find("p3"))
                point3 = transform.Find("p3");
        }
        if (control1 == null)
        {
            if (transform.Find("c1"))
                control1 = transform.Find("c1");
        }
        if (control2 == null)
        {
            if (transform.Find("c2"))
                control2 = transform.Find("c2");
        }

        //Fill array of transform references
        if (VisualisationPoints.Length != 6)       
            System.Array.Resize(ref VisualisationPoints, 6);       
        if (transform.Find("VisualisationPoints").Find("layer1p1"))
            VisualisationPoints[0] = transform.Find("VisualisationPoints").Find("layer1p1");
        if (transform.Find("VisualisationPoints").Find("layer1p2"))
            VisualisationPoints[1] = transform.Find("VisualisationPoints").Find("layer1p2");
        if (transform.Find("VisualisationPoints").Find("layer1p3"))
            VisualisationPoints[2] = transform.Find("VisualisationPoints").Find("layer1p3");
        if (transform.Find("VisualisationPoints").Find("layer2p1"))
            VisualisationPoints[3] = transform.Find("VisualisationPoints").Find("layer2p1");
        if (transform.Find("VisualisationPoints").Find("layer2p2"))
            VisualisationPoints[4] = transform.Find("VisualisationPoints").Find("layer2p2");
        if (transform.Find("VisualisationPoints").Find("layer3p1"))
            VisualisationPoints[5] = transform.Find("VisualisationPoints").Find("layer3p1");        
    }

    [ExecuteAlways]
    void OnDrawGizmos()
    {
        if (CheckForRefs() == false) return;

        Vector3 p1 = point1.position;
        Vector3 p2 = point2.position;
        Vector3 c1 = control1.position;
        Vector3 c2 = control2.position;

        //Draw points
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(p1, 0.15f);
        Gizmos.DrawSphere(p2, 0.15f);
        Gizmos.DrawSphere(c1, 0.15f);
        Gizmos.DrawSphere(c2, 0.15f);

        //Draw interp point
        //point3.position = Lerp(p1, p2, _t);
        point3.position = BezierInterp(p1, p2, c1, c2, _t);

        //Draw line
        Gizmos.color = Color.white;
        Gizmos.DrawLine(p1, p2);        
    }


    private bool CheckForRefs()
    {
        if (point1 == null || point2 == null || point3 == null || control1 == null || control2 == null)
            return false;
        return true;
    }
    private Vector3 BezierInterp(Vector3 aA, Vector3 aB, Vector3 aC1, Vector3 aC2, float aT)
    {
        //The gist of bezier is to interpolate between a->cp1, cp1->cp2... cpn->b, where cp is a control point of which there are n
        //then drawing a line to connect each interpolated point between segments and interpolating that with the same T value
        //this winds down until there is one line segment left to interpolate, at which point the linear interp will be on a curve
        Vector3 layer1p1 = Lerp(aA, aC1, aT);
        Vector3 layer1p2 = Lerp(aC1, aC2, aT);
        Vector3 layer1p3 = Lerp(aC2, aB, aT);

        Vector3 layer2p1 = Lerp(layer1p1, layer1p2, aT);
        Vector3 layer2p2 = Lerp(layer1p2, layer1p3, aT);

        Vector3 layer3p1 = Lerp(layer2p1, layer2p2, aT);

        if (drawBezierCurve)
        {
            //Lines between points
            Gizmos.DrawLine(aA, aC1);
            Gizmos.DrawLine(aC1, aC2);
            Gizmos.DrawLine(aC2, aB);

            //Interpolated points between line layers
            VisualisationPoints[0].position = layer1p1;
            VisualisationPoints[1].position = layer1p2;
            VisualisationPoints[2].position = layer1p3;
            Gizmos.DrawLine(layer1p1, layer1p2);
            Gizmos.DrawLine(layer1p2, layer1p3);

            VisualisationPoints[3].position = layer2p1;
            VisualisationPoints[4].position = layer2p2;
            Gizmos.DrawLine(layer2p1, layer2p2);            
        }
        

        return layer3p1;        
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
