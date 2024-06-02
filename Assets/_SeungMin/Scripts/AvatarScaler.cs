using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarScaler : MonoBehaviour
{
    public Transform headTracker; // 머리 트래커
    public Transform footTracker; // 발 트래커
    public Transform avatar; // 아바타의 루트 트랜스폼

    public float userHeight = 1.75f; // 사용자의 실제 키 (미터 단위)
    private float initialAvatarHeight; // 초기 아바타의 키

    void Start()
    {
        // 초기 아바타의 키를 계산
        initialAvatarHeight = Vector3.Distance(avatar.position, footTracker.position);
    }

    void Update()
    {
        // 머리 트래커와 발 트래커 간의 거리를 측정하여 현재 아바타의 키 계산
        float currentAvatarHeight = Vector3.Distance(headTracker.position, footTracker.position);

        // 아바타의 스케일을 사용자의 실제 키에 맞게 조정
        float scaleRatio = userHeight / currentAvatarHeight;
        avatar.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
    }
}

