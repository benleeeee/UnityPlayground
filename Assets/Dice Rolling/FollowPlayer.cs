using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform _Player;
    private Vector3 _Offset;

    private void Start()
    {
        if (_Player == null)
        {
            _Player = Transform.FindObjectOfType<DiceRollingScript>().transform;
        }

        _Offset = transform.position - _Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _Player.transform.position + _Offset;
    }
}
