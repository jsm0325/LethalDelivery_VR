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
        base.Update(); // �⺻ AI ��� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ��ƸԴ� ��� �� �÷��̾� ��� ȿ��
    }
}
