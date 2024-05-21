using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointRotationLimit : MonoBehaviour
{
    public Animator animator;

    // ������ ���� ���� ���� (����: ��)
    public float neckFlexionLimit = 45f;
    public float neckExtensionLimit = 45f;
    public float neckLateralFlexionLimit = 45f;
    public float neckRotationLimit = 60f;

    public float shoulderFlexionLimit = 180f;
    public float shoulderExtensionLimit = 60f;
    public float shoulderAbductionLimit = 180f;
    public float shoulderAdductionLimit = 75f;
    public float shoulderInternalRotationLimit = 70f;
    public float shoulderExternalRotationLimit = 90f;

    public float elbowFlexionLimit = 145f;
    public float elbowExtensionLimit = 0f;

    public float wristFlexionLimit = 80f;
    public float wristExtensionLimit = 70f;
    public float wristRadialDeviationLimit = 20f;
    public float wristUlnarDeviationLimit = 30f;

    public float spineFlexionLimit = 90f;
    public float spineExtensionLimit = 30f;
    public float spineLateralFlexionLimit = 30f;
    public float spineRotationLimit = 45f;

    public float hipFlexionLimit = 120f;
    public float hipExtensionLimit = 30f;
    public float hipAbductionLimit = 45f;
    public float hipAdductionLimit = 30f;
    public float hipInternalRotationLimit = 40f;
    public float hipExternalRotationLimit = 45f;

    public float kneeFlexionLimit = 135f;
    public float kneeExtensionLimit = 0f;

    public float ankleDorsiflexionLimit = 20f;
    public float anklePlantarflexionLimit = 50f;
    public float ankleInversionLimit = 35f;
    public float ankleEversionLimit = 25f;

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // ���� ȸ�� ����
            LimitJointRotation(HumanBodyBones.Neck, neckFlexionLimit, neckExtensionLimit, neckLateralFlexionLimit, neckRotationLimit);

            // ������ ����� ȸ�� ����
            LimitJointRotation(HumanBodyBones.RightUpperArm, shoulderFlexionLimit, shoulderExtensionLimit, shoulderAbductionLimit, shoulderAdductionLimit, shoulderInternalRotationLimit, shoulderExternalRotationLimit);

            // ������ �Ȳ�ġ�� ȸ�� ����
            LimitJointRotation(HumanBodyBones.RightLowerArm, elbowFlexionLimit, elbowExtensionLimit);

            // ������ �ո��� ȸ�� ����
            LimitJointRotation(HumanBodyBones.RightHand, wristFlexionLimit, wristExtensionLimit, wristRadialDeviationLimit, wristUlnarDeviationLimit);

            // ô���� ȸ�� ����
            LimitJointRotation(HumanBodyBones.Spine, spineFlexionLimit, spineExtensionLimit, spineLateralFlexionLimit, spineRotationLimit);

            // ������ ������� ȸ�� ����
            LimitJointRotation(HumanBodyBones.RightUpperLeg, hipFlexionLimit, hipExtensionLimit, hipAbductionLimit, hipAdductionLimit, hipInternalRotationLimit, hipExternalRotationLimit);

            // ������ ������ ȸ�� ����
            LimitJointRotation(HumanBodyBones.RightLowerLeg, kneeFlexionLimit, kneeExtensionLimit);

            // ������ �߸��� ȸ�� ����
            LimitJointRotation(HumanBodyBones.RightFoot, ankleDorsiflexionLimit, anklePlantarflexionLimit, ankleInversionLimit, ankleEversionLimit);
        }
    }

    void LimitJointRotation(HumanBodyBones bone, float flexionLimit, float extensionLimit)
    {
        Transform joint = animator.GetBoneTransform(bone);
        if (joint != null)
        {
            Vector3 localEulerAngles = joint.localEulerAngles;
            localEulerAngles.x = Mathf.Clamp(localEulerAngles.x, -extensionLimit, flexionLimit);
            joint.localEulerAngles = localEulerAngles;
        }
    }

    void LimitJointRotation(HumanBodyBones bone, float flexionLimit, float extensionLimit, float lateralFlexionLimit, float rotationLimit)
    {
        Transform joint = animator.GetBoneTransform(bone);
        if (joint != null)
        {
            Vector3 localEulerAngles = joint.localEulerAngles;
            localEulerAngles.x = Mathf.Clamp(localEulerAngles.x, -extensionLimit, flexionLimit);
            localEulerAngles.y = Mathf.Clamp(localEulerAngles.y, -rotationLimit, rotationLimit);
            localEulerAngles.z = Mathf.Clamp(localEulerAngles.z, -lateralFlexionLimit, lateralFlexionLimit);
            joint.localEulerAngles = localEulerAngles;
        }
    }

    void LimitJointRotation(HumanBodyBones bone, float flexionLimit, float extensionLimit, float abductionLimit, float adductionLimit, float internalRotationLimit, float externalRotationLimit)
    {
        Transform joint = animator.GetBoneTransform(bone);
        if (joint != null)
        {
            Vector3 localEulerAngles = joint.localEulerAngles;
            localEulerAngles.x = Mathf.Clamp(localEulerAngles.x, -extensionLimit, flexionLimit);
            localEulerAngles.y = Mathf.Clamp(localEulerAngles.y, -externalRotationLimit, internalRotationLimit);
            localEulerAngles.z = Mathf.Clamp(localEulerAngles.z, -adductionLimit, abductionLimit);
            joint.localEulerAngles = localEulerAngles;
        }
    }

}
