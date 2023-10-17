using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingFamily : ADroneBase
{
    [Header("�ö��� �йи��� ����")]

    [SerializeField] float DroneHP;
    [SerializeField] float DroneSpeed;
    [SerializeField] float RotationModifier;

    [Header("�÷��̾� �߰�")]
    [SerializeField] float SenseDistance;
    [SerializeField] float DistanceMultSpeed;


    public override void InitSetting()
    {
        data.droneHP = DroneHP;
        data.droneSpeed = DroneSpeed;
        data.rotationModifier = RotationModifier;
        data.SenseDistance = SenseDistance;
        data.DistanceDiffSpeed = DistanceMultSpeed;
        //data.droneRange = 5.0f;
    }

    public override void FlyingDrone(Transform targetGO, Rigidbody rb)
    {
        base.FlyingDrone(targetGO, rb);
    }
}
