using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[System.Serializable]

public class AnimationInput
{
    public string animationPropertyName;
    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources inputSource;
}

public class AnimateOnInput : MonoBehaviour
{
    public List<AnimationInput> animationInputs;
    public Animator animator;
    public float smoothTime = 0.1f; // �ε巯�� ��ȯ�� ���� �ð�

    private Dictionary<string, float> currentValues = new Dictionary<string, float>();
    private Dictionary<string, float> velocities = new Dictionary<string, float>();

    void Start()
    {
        foreach (var item in animationInputs)
        {
            currentValues[item.animationPropertyName] = 0f;
            velocities[item.animationPropertyName] = 0f;
        }
    }

    void Update()
    {
        foreach (var item in animationInputs)
        {
            bool actionValue = item.action.GetState(item.inputSource);
            float targetValue = actionValue ? 1.0f : 0.0f;

            // ���� ���� �����ɴϴ�.
            float currentValue = currentValues[item.animationPropertyName];

            // �ӽ� ������ velocity ���� �����ɴϴ�.
            float velocity = velocities[item.animationPropertyName];

            // Mathf.SmoothDamp�� ����Ͽ� �ε巴�� ���� ��ȯ�մϴ�.
            currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref velocity, smoothTime);

            // ��ȯ�� ���� �����մϴ�.
            currentValues[item.animationPropertyName] = currentValue;

            // ������Ʈ�� velocity ���� �����մϴ�.
            velocities[item.animationPropertyName] = velocity;

            // �ִϸ����� �Ķ���͸� ������Ʈ�մϴ�.
            animator.SetFloat(item.animationPropertyName, currentValue);
        }
    }
}
