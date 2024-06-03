using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class IKTargetFollowVRRig : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public IKFootSolver leftIKFootSolver;
    public IKFootSolver rightIKFootSolver;
    public bool onFullTracking = true;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    public VRMap Waist;
    public VRMap leftFoot;
    public VRMap rightFoot;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;

    private static IKTargetFollowVRRig instance;
    private Animator animator;
    // Update is called once per frame
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        transform.position = head.ikTarget.position + headBodyPositionOffset;
        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z),turnSmoothness);

        head.Map();
        leftHand.Map();
        rightHand.Map();
        if(onFullTracking == true)
        {
            if (leftIKFootSolver.enabled == true)
            {
                leftIKFootSolver.enabled = false;
                rightIKFootSolver.enabled = false;
            }
            //Waist.Map();
            leftFoot.Map();
            rightFoot.Map();
        }
        else
        {

            if (leftIKFootSolver.enabled == false)
            {
                leftIKFootSolver.enabled = true;
                rightIKFootSolver.enabled = true;
            }
        }
    }
}
