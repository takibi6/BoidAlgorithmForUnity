﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMove : MonoBehaviour {

    CrystalManager managerClass;
<<<<<<< HEAD
    Vector3 averagePosition = Vector3.zero;
    Vector3 averageVelocity = Vector3.zero;
    //Vector3 acceleration = Vector3.zero;
    Vector3 velocity = Vector3.zero;
=======
    Vector3 averagePosition;
    Vector3 averageVelocity;
    //Vector3 acceleration;
    Vector3 velocity;
    //Vector3 angle;
    Vector3 destination;
    GameObject lastDestObject;
    float randomSpeed;
>>>>>>> sub

    void Start () {
        managerClass = GameObject.Find("Crystals").GetComponent<CrystalManager>();
        averagePosition = Vector3.zero;
        averageVelocity = Vector3.zero;
        //acceleration = Vector3.zero;
        velocity = Vector3.zero;
        //angle = Vector3.zero;
        destination = Vector3.zero;
        lastDestObject = null;
        randomSpeed = Random.Range(1/managerClass.randomRate, managerClass.randomRate);
    }
	
	void Update () {
        InitParam();

        SearchCrystals();
        DoCohesion();
        DoAlignment();

        MoveCrystal();
        RotateCrystal(velocity);
    }

    void InitParam()
    {
        //acceleration = Vector3.zero;
        //velocity = Vector3.zero;
        averagePosition = Vector3.zero;
        averageVelocity = Vector3.zero;
        destination = Vector3.zero;
    }

    //探索
    void SearchCrystals()
    {
        //if (transform.GetSiblingIndex() != 0) return;   //テスト用
        int count = 0;

        for (int i = 0; i < managerClass.crystalNum; i++)
        {
            if (i != transform.GetSiblingIndex())   //自分自身を探索しない
            {
                Vector3 diffVector = this.transform.position - managerClass.crystalObjects[i].transform.position;

                if (InView(diffVector))                      //視界内だったら
                {
                    averagePosition += managerClass.crystalObjects[i].transform.position;
                    averageVelocity += managerClass.crystalClasses[i].velocity;
                    lastDestObject = managerClass.crystalObjects[i];               //最後に視界に入っていた水晶
                    count++;
                }
                //分離処理
                DoSeparation(diffVector);
            }
        }

        //平均を出す
        if (count > 0)
        {
            //averagePosition += managerClass.targetObject.transform.position - transform.position;
            //count++;
            averagePosition /= count;   //平均位置（自身を含めない）
            averageVelocity /= count;
        }
        //対象がいなかった場合、最後の目標水晶へ送る
        else if (lastDestObject)
        {
            Vector3 diff = lastDestObject.transform.position - transform.position;
            averagePosition = diff;
        }
        //対象がいなかった場合、リーダー位置へ向かう（平均しない）
        else
        {
            Vector3 diff = managerClass.targetObject.transform.position - transform.position;
            averagePosition = diff;
        }
<<<<<<< HEAD
        //averagePosition = managerClass.targetObject.transform.position - transform.position;  //デバッグ用、全員ターゲットを目指す
=======
>>>>>>> sub

        if (transform.GetSiblingIndex() != 0) return;   //テスト用
        managerClass.centerMark.transform.position = averagePosition;   //確認用マーカーを動かす
    }

    //視界内か
    bool InView(Vector3 diff)
    {
        //視界内か判定
        if (diff.magnitude < managerClass.searchDistance)    //一定範囲内か
        {
            if (Vector3.Dot(this.transform.forward, diff.normalized) > managerClass.crystalGetAngle)  //一定角度内か
            {
                return true;
            }
        }
        return false;
    }


    //結合：中心に向かって動く
    void DoCohesion()
    {
        //Vector3 diff = averagePosition - this.transform.position;
        //if (diff.magnitude < managerClass.keepDistance) return;     //分離を優先
        
        destination += averagePosition;
        //angle = destination;
        //velocity *= velocity.magnitude;
    }


    //分離：一定距離を保つ
    void DoSeparation(Vector3 diff)
    {
        if (diff.magnitude > managerClass.keepDistance) return;
        //Debug.Log(diff.magnitude);
        
        float removeVelocity = managerClass.keepDistance / diff.magnitude;
<<<<<<< HEAD
        velocity += diff.normalized * removeVelocity;
=======
        destination += diff.normalized * removeVelocity;
>>>>>>> sub
    }


    //整列：平均速度ベクトルに合わせる
    void DoAlignment()
    {
        destination += averageVelocity;
        //angle += destination;
    }

    
    //動く
    void MoveCrystal()
    {
        //velocity = destination.normalized * managerClass.moveSpeed * Time.deltaTime;
        Vector3 diff = managerClass.targetObject.transform.position - transform.position;
        velocity = destination.normalized * managerClass.applicability + diff.normalized * (1f - managerClass.applicability);
        velocity *= managerClass.moveSpeed * randomSpeed * Time.deltaTime;
        this.transform.position += velocity;
    }

    //個体の進行方向に合わせて回転させる
    void RotateCrystal(Vector3 velocity)
    {
        this.transform.LookAt(this.transform.position + velocity.normalized);
    }
}
