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
    public float smoothTime = 0.1f; // 부드러운 전환을 위한 시간

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

            // 현재 값을 가져옵니다.
            float currentValue = currentValues[item.animationPropertyName];

            // 임시 변수로 velocity 값을 가져옵니다.
            float velocity = velocities[item.animationPropertyName];

            // Mathf.SmoothDamp를 사용하여 부드럽게 값을 전환합니다.
            currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref velocity, smoothTime);

            // 전환된 값을 저장합니다.
            currentValues[item.animationPropertyName] = currentValue;

            // 업데이트된 velocity 값을 저장합니다.
            velocities[item.animationPropertyName] = velocity;

            // 애니메이터 파라미터를 업데이트합니다.
            animator.SetFloat(item.animationPropertyName, currentValue);
        }
    }
}
