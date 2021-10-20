using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;


public class NetworkPlayer : MonoBehaviour
{
    public Transform head, leftHand, rightHand;
    private Transform _headRig, _leftHandRig, _rightHandRig;
    private PhotonView pv;

    public Animator leftHandAnimator, rightHandAnimator;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        XRRig rig = FindObjectOfType<XRRig>();
        _headRig = rig.transform.Find("Camera Offset/Main Camera");
        _leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        _rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");

        if (pv.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            

            MapPosition(head, _headRig);
            MapPosition(leftHand, _leftHandRig);
            MapPosition(rightHand, _rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand),leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand),rightHandAnimator);
        }
    }


    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }


    void MapPosition(Transform target, Transform rigTransform) {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
