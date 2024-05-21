using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimController : MonoBehaviour
{
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    public SteamVR_Action_Boolean leftGripAction;
    public SteamVR_Action_Boolean leftPinchAction;
    public SteamVR_Action_Boolean rightGripAction;
    public SteamVR_Action_Boolean rightPinchAction;
    private void Start()
    {
        // �׼��� �ùٸ��� �Ҵ�Ǿ����� Ȯ��
        if (leftGripAction == null)
            Debug.LogError("Left Grip Action is not assigned.");
        if (rightGripAction == null)
            Debug.LogError("Right Grip Action is not assigned.");
        if (leftPinchAction == null)
            Debug.LogError("Left Pinch Action is not assigned.");
        if (rightPinchAction == null)
            Debug.LogError("Right Pinch Action is not assigned.");
    }
    private void Update()
    {
        // �޼� �ִϸ��̼� ������Ʈ
        bool leftGripValue = leftGripAction.GetState(SteamVR_Input_Sources.LeftHand);
        bool leftPinchValue = leftPinchAction.GetState(SteamVR_Input_Sources.LeftHand);
        UpdateHandAnimator(leftHandAnimator, "Left Grab", "Left Pinch", leftGripValue, leftPinchValue);

        // ������ �ִϸ��̼� ������Ʈ
        bool rightGripValue = rightGripAction.GetState(SteamVR_Input_Sources.RightHand);
        bool rightPinchValue = rightPinchAction.GetState(SteamVR_Input_Sources.RightHand);
        UpdateHandAnimator(rightHandAnimator, "Right Grab", "Right Pinch", rightGripValue, rightPinchValue);
    }

    private void UpdateHandAnimator(Animator handAnimator, string gripParam, string pinchParam, bool gripValue, bool pinchValue)
    {
        handAnimator.SetFloat(gripParam, gripValue ? 1.0f : 0.0f);
        handAnimator.SetFloat(pinchParam, pinchValue ? 1.0f : 0.0f);
    }
}
