using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFamily : ADroneBase
{
    [Header("픽스드 패밀리어 설정")]

    [SerializeField] float TurretHP;

    public override void InitSetting()
    {
        data.droneHP = TurretHP;
        //data.droneRange = 5.0f;
    }

}
