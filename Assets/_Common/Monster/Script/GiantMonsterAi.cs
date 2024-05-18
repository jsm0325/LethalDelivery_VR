using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMonsterAi : MonsterAi
{
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update(); // 기본 AI 기능 수행
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 잡아먹는 모션 및 플레이어 사망 효과
    }
}
