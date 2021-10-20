using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
public class XRGrabNetworkInteractable : XRGrabInteractable
{
    PhotonView pv;


    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        pv.RequestOwnership();
        base.OnSelectEntered(interactor);
    }
}
