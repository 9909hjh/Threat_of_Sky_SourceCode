using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleDrone : ATargetBase
{
    //���⿡ ä���̶� ��ġ�� �ۺ����� ����
    [Header("������ ��� ����")]

    [SerializeField] float FireRate; // 1�⺻
    [SerializeField] float CurrentFireRate; // 1�⺻
    [SerializeField] float DeliveryValue; // 10.0f �⺻
    [SerializeField] float RifleRange; // 10 �⺻
    [SerializeField] float SpinSpeed; // 100~1000����������

    public override void InitSetting()
    {
        //Tdata.droneHP = 10.0f;
        Tdata.m_fireRate = FireRate;
        Tdata.m_currentFireRate = CurrentFireRate;
        Tdata.DeliveryValue = DeliveryValue; // ������
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
