using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFamily : ADroneBase
{
    [Header("�Ƚ��� �йи��� ����")]

    [SerializeField] float TurretHP;

    public override void InitSetting()
    {
        data.droneHP = TurretHP;
        //data.droneRange = 5.0f;
    }

}
