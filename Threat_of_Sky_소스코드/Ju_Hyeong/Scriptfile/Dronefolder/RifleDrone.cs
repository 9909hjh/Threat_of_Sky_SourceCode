using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleDrone : ATargetBase
{
    //여기에 채력이랑 수치를 퍼블릭으로 넣자
    [Header("라이플 드론 설정")]

    [SerializeField] float FireRate; // 1기본
    [SerializeField] float CurrentFireRate; // 1기본
    [SerializeField] float DeliveryValue; // 10.0f 기본
    [SerializeField] float RifleRange; // 10 기본
    [SerializeField] float SpinSpeed; // 100~1000유동적으로

    public override void InitSetting()
    {
        //Tdata.droneHP = 10.0f;
        Tdata.m_fireRate = FireRate;
        Tdata.m_currentFireRate = CurrentFireRate;
        Tdata.DeliveryValue = DeliveryValue; // 데미지
        Tdata.droneRange = RifleRange;
        Tdata.spinSpeed = SpinSpeed;
    }

    public override void TargetSearch(LayerMask layerMask)
    {
        base.TargetSearch(layerMask);
    }

    public override void TargetUpdate(Transform m_tfbarrelBody, LayerMask layerMask)
    {
        base.TargetUpdate(m_tfbarrelBody, layerMask);
    }
}
