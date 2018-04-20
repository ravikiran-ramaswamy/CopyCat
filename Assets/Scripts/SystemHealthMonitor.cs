using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleDiskUtils;

public class SystemHealthMonitor : MonoBehaviour {

    public GameObject KinectDataProviderGameObject;
    public bool isSystemHealthy { get; private set; }

    private KinectDataProvider kinectDataProvider;

	// Use this for initialization
	void Start () {
        isSystemHealthy = false;
        kinectDataProvider = KinectDataProviderGameObject.GetComponent<KinectDataProvider>();
	}
	
	// Update is called once per frame
	void Update () {
        isSystemHealthy = DiskUtils.CheckAvailableSpace() > 1024;
        isSystemHealthy &= kinectDataProvider.kinectAvailable();
        isSystemHealthy &= kinectDataProvider.numBodies() > 0;
	}

    public bool bodiesFound()
    {
        return kinectDataProvider.numBodies() > 0;
    }
}
