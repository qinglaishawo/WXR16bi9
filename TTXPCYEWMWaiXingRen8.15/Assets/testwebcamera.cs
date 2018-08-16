using UnityEngine;
using System.Collections;

public class testwebcamera : MonoBehaviour {

    public int index = 0;
	// Use this for initialization
	void Start () {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture(devices[index].name, 1024, 768);
        print(devices[index].name);
        if (devices.Length > 0)
        {
            //webcamTexture.deviceName = devices[1].name;
            GetComponent<MeshRenderer>().material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
