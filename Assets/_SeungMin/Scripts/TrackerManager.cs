using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TrackerManager : MonoBehaviour
{
    public SteamVR_TrackedObject leftFootTracker;
    public SteamVR_TrackedObject rightFootTracker;
    public SteamVR_TrackedObject waistTracker;

    void Start()
    {
        InitializeTrackers();
    }

    void InitializeTrackers()
    {
        leftFootTracker = InitializeTracker(leftFootTracker, "LHR-021AC8BE");
        rightFootTracker = InitializeTracker(rightFootTracker, "LHR-35D29139");
        waistTracker = InitializeTracker(waistTracker, "LHR-3C051207");
    }

    SteamVR_TrackedObject InitializeTracker(SteamVR_TrackedObject trackedObject, string serial)
    {
        // Set device index based on the serial number
        var trackedDeviceIndex = GetDeviceIndexBySerial(serial);
        if (trackedDeviceIndex != -1)
        {
            trackedObject.SetDeviceIndex(trackedDeviceIndex);
        }

        return trackedObject;
    }

    int GetDeviceIndexBySerial(string serial)
    {
        for (int i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            var error = ETrackedPropertyError.TrackedProp_Success;
            var capacity = OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, null, 0, ref error);
            if (capacity > 1)
            {
                var result = new System.Text.StringBuilder((int)capacity);
                OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, result, capacity, ref error);
                if (result.ToString() == serial)
                {
                    return i;
                }
            }
        }
        return -1;
    }
}
