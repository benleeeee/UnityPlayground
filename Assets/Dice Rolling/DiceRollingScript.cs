using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

[ExecuteInEditMode]
public class DiceRollingScript : MonoBehaviour
{
    public enum E_direction
    {
        up,
        down,
        left,
        right,
        none
    }
    public Transform _Pivot;
    private E_direction _MovingDir = E_direction.none;
    public float _RollSpeed;
    private float _DegreesRotated = 0;
    private bool _PivotSet = false;
    private void Start()
    {
        if (_Pivot == null)
            _Pivot = transform.Find("Pivot");
    }
    private void Update()
    {
        CheckForInput();
    }    
    void CheckForInput()
    {        
        if (_MovingDir == E_direction.none)
        {
            if (Input.GetKey(KeyCode.W))
            {
                _MovingDir = E_direction.up;
                SetPivot(_Pivot.parent.position + new Vector3(0, -0.38f, 0.38f));
            }
            if (Input.GetKey(KeyCode.A))
            {
                _MovingDir = E_direction.left;
                SetPivot(_Pivot.parent.position + new Vector3(-0.38f, -0.38f, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                _MovingDir = E_direction.down;
                SetPivot(_Pivot.parent.position + new Vector3(0, -0.38f, -0.38f));
            }
            if (Input.GetKey(KeyCode.D))
            {
                _MovingDir = E_direction.right;
                SetPivot(_Pivot.parent.position + new Vector3(0.38f, -0.38f, 0));
            }
        }
    }
    private void SetPivot(Vector3 worldPos)
    {
        _Pivot.position = worldPos;
        _PivotSet = true;
    }


    [ExecuteAlways]
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_Pivot.position, 0.05f);
    }




    private void FixedUpdate()
    {
        if (_MovingDir != E_direction.none)
        {
            if (_PivotSet)            
                Roll();            
            else
                Debug.LogError("Pivot not set but move direction is set to " + _MovingDir);
        }
    }
    private void Roll()
    {
        float rollAmount = _RollSpeed * Time.deltaTime;
        _DegreesRotated += rollAmount;

        //Then roll the player in that direction 90 degrees
        switch (_MovingDir)
        {
            case E_direction.up:    //+x
                transform.RotateAround(_Pivot.position, Vector3.right, rollAmount);
                break;
            case E_direction.down:  //-x
                transform.RotateAround(_Pivot.position, Vector3.right, -rollAmount);
                break;
            case E_direction.left:  //+z
                transform.RotateAround(_Pivot.position, Vector3.forward, rollAmount);
                break;
            case E_direction.right: //-z
                transform.RotateAround(_Pivot.position, Vector3.forward, -rollAmount);
                break;
        }

        //Check player has finished rolling
        if (_DegreesRotated >= 90)
        {
            //round each rotation to a multiple of 90
            float exactRotationX = FixToNearestAngle(transform.rotation.x);
            float exactRotationZ = FixToNearestAngle(transform.rotation.z);
            transform.rotation.eulerAngles.Set(exactRotationX, 0, exactRotationZ);

            //Reset for next movement input
            ResetForNewMovement();
            //Debug.Log("up face = " + GetComponent<FaceChecker>().GetPosYFace());
            //Debug.Log("down face = " + GetComponent<FaceChecker>().GetNegYFace());
            //Debug.Log("right face = " + GetComponent<FaceChecker>().GetPosXFace());
            //Debug.Log("left face = " + GetComponent<FaceChecker>().GetNegXFace());
            //Debug.Log("forward face = " + GetComponent<FaceChecker>().GetPosZFace());
            //Debug.Log("backward face = " + GetComponent<FaceChecker>().GetNegZFace());
        }
    }
    private int FixToNearestAngle(float angle)
    {
        return (int)Math.Round(angle / (double)90, MidpointRounding.ToEven) * 90;
    }

    private void ResetForNewMovement()
    {
        //_PivotSet = false;
        _DegreesRotated = 0;
        _MovingDir = E_direction.none;
    }
}
