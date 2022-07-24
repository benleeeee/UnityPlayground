using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All function names with directions refer to world space
/// </summary>
public class FaceChecker : MonoBehaviour
{
    private int _layermask;
    private void Start()
    {
        _layermask = 1 << LayerMask.NameToLayer("DiceFace");
    }


    public int GetPosYFace()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, out hit, 1, _layermask);
        if (hit.collider != null)
        {
            //Return first character of obj name (as int)
            return (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
        }


        //Nothing found
        Debug.LogError("No dice face found!");
        return -1;
    }
    public int GetNegYFace()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position - new Vector3(0, 1, 0), Vector3.up, out hit, 1, _layermask);
        if (hit.collider != null)
        {
            //Return first character of obj name (as int)
            return (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
        }


        //Nothing found
        Debug.LogError("No dice face found!");
        return -1;
    }


    public int GetPosXFace()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(1, 0, 0), Vector3.left, out hit, 1, _layermask);
        if (hit.collider != null)
        {
            //Return first character of obj name (as int)
            return (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
        }


        //Nothing found
        Debug.LogError("No dice face found!");
        return -1;
    }
    public int GetNegXFace()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position - new Vector3(1, 0, 0), Vector3.right, out hit, 1, _layermask);
        if (hit.collider != null)
        {
            //Return first character of obj name (as int)
            return (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
        }


        //Nothing found
        Debug.LogError("No dice face found!");
        return -1;
    }


    public int GetPosZFace()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 0, 1), Vector3.back, out hit, 1, _layermask);
        if (hit.collider != null)
        {
            //Return first character of obj name (as int)
            return (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
        }


        //Nothing found
        Debug.LogError("No dice face found!");
        return -1;
    }
    public int GetNegZFace()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position - new Vector3(0, 0, 1), Vector3.forward, out hit, 1, _layermask);
        if (hit.collider != null)
        {
            //Return first character of obj name (as int)
            return (int)char.GetNumericValue(hit.collider.gameObject.name[0]);
        }


        //Nothing found
        Debug.LogError("No dice face found!");
        return -1;
    }
}
