using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using EasyAR;

public class ImageTargetImageManageTTXPC : MonoBehaviour
{
    private int screenW = 0;
    private int screenH = 0;

    private bool initialize = false;
    private Color[] pixels;
    public static Texture2D VideoBackgroundTexure = null;
    //private Camera RenderCamera;
    private static Texture2D mainTexture;

    private int cameraNumber;
    private float cameraWidth;
    private float cameraHeight;
    private float screenRatio;
    private List<GameObject> RCObjs = new List<GameObject>();
	private List<GameObject> RCObj2s = new List<GameObject>();
    private Color[] tempPixels;

    /* public GameObject bgObj1;
     public GameObject bgObj2;
     public GameObject bgObjV;*/
    public GameObject testPlane;
    public GameObject testCameraDevice;
    public GameObject testARCamera;

    private string deviceName;
    public WebCamTexture WCT;

    public Texture tempTexture2D;

    private WebCamDevice[] devices;
    private WebCamTexture webcamTexture;

    bool testaaaa = false;
	public  Texture2D VideoBackgroundTexure2;

    private Camera RenderCamera;
    private RenderTexture renderTexture;
    private static ImageTargetImageManageTTXPC instance;
    public static ImageTargetImageManageTTXPC Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        screenW = Screen.width;
        screenH = Screen.height;
        screenRatio = (float)screenH / screenW;
        print(screenH);
        print(screenW);
        Invoke("Initialize", 0.8f);
        /*WebCamDevice[] devices = WebCamTexture.devices;
        deviceName = devices[0].name;
        WCT = new WebCamTexture(deviceName);
        bgObj2.GetComponent<MeshRenderer>().material.mainTexture = WCT;
        WCT.Play();*/
        /* WebCamDevice[] devices = WebCamTexture.devices;
         WebCamTexture webcamTexture = new WebCamTexture(devices[1].name,1024,768);
        print(devices[1].name);
         if (devices.Length > 0)
         {
             //webcamTexture.deviceName = devices[1].name;
            bgObj2.GetComponent<MeshRenderer>().material.mainTexture = webcamTexture;
            webcamTexture.Play();
         }
        //tempTexture2D = webcamTexture;
        bgObj2.GetComponent<MeshRenderer>().material.mainTexture = webcamTexture;*/
        //StartCoroutine(WaitWebcamTexture());
    }

    public void ChangeButton()
    {
        /*List<ImageTrackerBaseBehaviour> trackers = ARBuilder.Instance.TrackerBehaviours;
        foreach (ImageTrackerBaseBehaviour itbb in trackers)
            itbb.StopTrack();*/

        //CameraDeviceBaseBehaviour a = testCameraDevice.GetComponent<CameraDeviceBehaviour>();
        GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Stop();
        GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Open(1);
        GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Start();
        //GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Close();
        //a.Device.Dispose();
        //StartCoroutine(WaitWebcamTexture());
        // testARCamera.SetActive(false);
    }
    public void ChangeButton2()
    {
         testaaaa = !testaaaa;
         if (testaaaa)
         {
            CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Stop();
            CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Open(CameraDevice.Device.Back);
            CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Start();
			Initialize12();
           // Invoke("createCamera2",3);
            //createCamera2(0);
          /*GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Stop();
            GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Open(CameraDevice.Device.Front);
            GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Start();*/
        }
         else
         {
			CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Stop();
			CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Open(CameraDevice.Device.Front);
			CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Start();
             /*GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Stop();
             GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Open(CameraDevice.Device.Back);
             GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Start();*/
			
         }
        
        //ARBuilder.Instance.Start();
        ARBuilder.Instance.EasyBuild();
        

        //StartCoroutine(WaitWebcamTexture2());
        //CameraDeviceBaseBehaviour a = testCameraDevice.GetComponent<CameraDeviceBehaviour>();
        ////a.OpenAndStart();
        //a.Device.Stop();
        /* GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Stop();
         GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Open(0);
         GameObject.Find("CameraDevice").GetComponent<CameraDeviceBehaviour>().Device.Start();*/
        //a.Device.SetFocusMode(CameraDevice.FocusMode.Normal);
        // a.Device.SetFlashTorchMode(true);

        List<ImageTrackerBaseBehaviour> trackers = ARBuilder.Instance.TrackerBehaviours;
        foreach (ImageTrackerBaseBehaviour itbb in trackers)
        {
            itbb.StartTrack();
          
        }
         
        //testARCamera.SetActive(true);
    }
	private void createCamera12(float y)
	{
		GameObject RCObj = new GameObject("RenderCamera2");
		Camera RenderCamera;
		RenderTexture renderTexture;
		RenderCamera = RCObj.AddComponent<Camera>();
		RenderCamera.transform.parent = Camera.main.gameObject.transform.parent;
		RenderCamera.hideFlags = HideFlags.None;
		renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
		RenderCamera.CopyFrom(Camera.main);
		for (int i = 0; i < Camera.main.gameObject.transform.childCount; i++)
		{
			Camera.main.gameObject.transform.GetChild(i).gameObject.layer = 31;
		}
		RenderCamera.depth = 0;
		RenderCamera.cullingMask = 1 << 31;
		RenderCamera.targetTexture = renderTexture;
		RenderCamera.Render();
		RCObj.transform.localPosition = new Vector3(0, y, 0);
		//RCObj.transform.localRotation = Quaternion.Euler(0, 0, 180);
		RCObj2s.Add(RCObj);

		/*Camera dlag = RCObj.GetComponent<Camera>();
        Matrix4x4 temp = new Matrix4x4();
        temp.m00 = 1.70064f;
        temp.m01 = 0;
        temp.m02 = 0;
        temp.m03 = 0;
        temp.m10 = 0;
        temp.m11 = 3.02222f;
        temp.m12 = 0;
        temp.m13 = 0;
        temp.m20 = 0;
        temp.m21 = 0;
        temp.m22 = -1.00080f;
        temp.m23 = -0.40016f;
        temp.m30 = 0;
        temp.m31 = 0;
        temp.m32 = -1;
        temp.m33 = 0;
        dlag.projectionMatrix = temp;*/
	}

	void Initialize12()
	{
		cameraWidth = Camera.main.gameObject.transform.GetChild(0).localScale.x;
		cameraHeight = Camera.main.gameObject.transform.GetChild(0).localScale.z;
		//print(screenRatio);
		float smallCameraHeight = (float)cameraWidth * screenRatio;
		cameraNumber = (int)Math.Floor((float)cameraHeight / (int)smallCameraHeight);
		//cameraNumber = (int)Math.Floor((double)cameraHeight / smallCameraHeight);
		//print(Math.Floor((double)cameraHeight / smallCameraHeight));
		print(cameraWidth);
		print(cameraHeight);
		//print(Math.Floor((double)cameraHeight));
		//print(smallCameraHeight);
		//print(cameraNumber);
		if (RCObj2s.Count == 0)
		{
			for (int i = 0; i < cameraNumber; i++)
			{
				//print(cameraHeight * 0.5f - smallCameraHeight * 0.5f);
				createCamera12(cameraHeight * 0.5f - smallCameraHeight * 0.5f - smallCameraHeight * i);
			}
		}
		if (VideoBackgroundTexure2 == null)
		{
			pixels = null;
			VideoBackgroundTexure2 = null;
			int VideoBackgroundTexureHeight = Screen.height * cameraNumber;
			pixels = new Color[Screen.width * VideoBackgroundTexureHeight];
			VideoBackgroundTexure2 = new Texture2D(Screen.width, Screen.height * cameraNumber, TextureFormat.RGB24, false);
		}
		//bgObj.transform.localScale = new Vector3(bgObj.transform.localScale.x * (cameraWidth/ cameraHeight), bgObj.transform.localScale.x, bgObj.transform.localScale.x);
	}

    IEnumerator WaitWebcamTexture()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            devices = WebCamTexture.devices;
            webcamTexture = new WebCamTexture();

            if (devices.Length > 0)
            {
                print(devices[1].name);
                webcamTexture.deviceName = devices[1].name;
                testPlane.GetComponent<MeshRenderer>().material.mainTexture = webcamTexture;
                webcamTexture.Play();
                print(webcamTexture.width);
                print(webcamTexture.height);
            }
        }
    }
    IEnumerator WaitWebcamTexture2()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            if (devices.Length > 0)
            {
                webcamTexture.Stop();
                testPlane.GetComponent<MeshRenderer>().material.mainTexture = null;
            }
        }
    }


    void Initialize()
    {
        /* if (!cameraDeviceType)
         {
             CameraDeviceBehaviour.cameraDeviceBehaviour.CameraDeviceType = CameraDevice.Device.Default;
         }
         else
         {
             CameraDeviceBehaviour.cameraDeviceBehaviour.CameraDeviceType = CameraDevice.Device.Front;
         }*/
        //createCamera(0f);
        createCamera16bi9();
    }
    /*void Initialize()
    {
        if (!initialize)
        {
            cameraWidth = Camera.main.gameObject.transform.GetChild(0).localScale.x;
            cameraHeight = Camera.main.gameObject.transform.GetChild(0).localScale.z;
            //print(screenRatio);
            float smallCameraHeight =(float)cameraWidth * screenRatio;
            cameraNumber =(int)Math.Floor((double)cameraHeight /(int)smallCameraHeight);
            //cameraNumber = (int)Math.Floor((double)cameraHeight / smallCameraHeight);
            //print(Math.Floor((double)cameraHeight / smallCameraHeight));
            print(cameraWidth);
            print(cameraHeight);
            //print(Math.Floor((double)cameraHeight));
            //print(smallCameraHeight);
            //print(cameraNumber);
            for (int i = 0; i < cameraNumber; i++)
            {
                //print(cameraHeight * 0.5f - smallCameraHeight * 0.5f);
                createCamera(cameraHeight * 0.5f - smallCameraHeight*0.5f- smallCameraHeight*i);
            }
            int VideoBackgroundTexureHeight = screenH * cameraNumber;
            pixels = new Color[screenW* VideoBackgroundTexureHeight];
            tempPixels = new Color[screenH * screenW];
            VideoBackgroundTexure = new Texture2D(screenW, screenH * cameraNumber, TextureFormat.RGB24, false);
            initialize = true;

            //bgObj.transform.localScale = new Vector3(bgObj.transform.localScale.x * (cameraWidth/ cameraHeight), bgObj.transform.localScale.x, bgObj.transform.localScale.x);
        }
    }*/

    private void createCamera(float y)
    {
        GameObject RCObj = new GameObject("RenderCamera1");
        Camera RenderCamera;
        RenderTexture renderTexture;
        RenderCamera = RCObj.AddComponent<Camera>();
        RenderCamera.transform.parent = Camera.main.gameObject.transform.parent;
        RenderCamera.hideFlags = HideFlags.None;
        renderTexture = new RenderTexture(screenW, screenH, 0);
        RenderCamera.CopyFrom(Camera.main);
        for (int i = 0; i < Camera.main.gameObject.transform.childCount; i++)
        {
            Camera.main.gameObject.transform.GetChild(i).gameObject.layer = 31;
        }
        RenderCamera.depth = 0;
        RenderCamera.cullingMask = 1 << 31;
        RenderCamera.targetTexture = renderTexture;
        RenderCamera.Render();
        RCObj.transform.localPosition = new Vector3(0, y, 0);
        RCObj.transform.localRotation = Quaternion.Euler(0, 0, 180);
        RCObjs.Add(RCObj);

        Camera dlag = RCObj.GetComponent<Camera>();
        Matrix4x4 temp = new Matrix4x4();
        temp.m00 = 1.70064f;
        temp.m01 = 0;
        temp.m02 = 0;
        temp.m03 = 0;
        temp.m10 = 0;
        temp.m11 = 3.02222f;
        temp.m12 = 0;
        temp.m13 = 0;
        temp.m20 = 0;
        temp.m21 = 0;
        temp.m22 = -1.00080f;
        temp.m23 = -0.40016f;
        temp.m30 = 0;
        temp.m31 = 0;
        temp.m32 = -1;
        temp.m33 = 0;
        dlag.projectionMatrix = temp;
    }

    private void createCamera2()
    {
        GameObject RCObj = new GameObject("RenderCamera2");
        Camera RenderCamera;
        RenderTexture renderTexture;
        RenderCamera = RCObj.AddComponent<Camera>();
        RenderCamera.transform.parent = Camera.main.gameObject.transform.parent;
        RenderCamera.hideFlags = HideFlags.None;
        renderTexture = new RenderTexture(screenW, screenH, 0);
        RenderCamera.CopyFrom(Camera.main);
        for (int i = 0; i < Camera.main.gameObject.transform.childCount; i++)
        {
            Camera.main.gameObject.transform.GetChild(i).gameObject.layer = 31;
        }
        RenderCamera.depth = 0;
        RenderCamera.cullingMask = 1 << 31;
        RenderCamera.targetTexture = renderTexture;
        RenderCamera.Render();
        RCObj.transform.localPosition = new Vector3(0, 0, 0);
        RCObj.transform.localRotation = Quaternion.Euler(0, 0, 180);
        RCObjs.Add(RCObj);

        Camera dlag = RCObj.GetComponent<Camera>();
        Matrix4x4 temp = new Matrix4x4();
        temp.m00 = 1.70064f;
        temp.m01 = 0;
        temp.m02 = 0;
        temp.m03 = 0;
        temp.m10 = 0;
        temp.m11 = 3.02222f;
        temp.m12 = 0;
        temp.m13 = 0;
        temp.m20 = 0;
        temp.m21 = 0;
        temp.m22 = -1.00080f;
        temp.m23 = -0.40016f;
        temp.m30 = 0;
        temp.m31 = 0;
        temp.m32 = -1;
        temp.m33 = 0;
        dlag.projectionMatrix = temp;
    }

    private void createCamera16bi9()
    {
        GameObject RCObj = new GameObject("RenderCamera");
        RenderCamera = RCObj.AddComponent<Camera>();
        RenderCamera.transform.parent = Camera.main.gameObject.transform.parent;
        RenderCamera.hideFlags = HideFlags.None;
        if (!renderTexture)
            renderTexture = new RenderTexture(screenW, screenH, 0);
        RenderCamera.CopyFrom(Camera.main);
        for (int i = 0; i < Camera.main.gameObject.transform.childCount; i++)
            Camera.main.gameObject.transform.GetChild(i).gameObject.layer = 31;
        RenderCamera.depth = 0;
        RenderCamera.cullingMask = 1 << 31;
        RenderCamera.targetTexture = renderTexture;
        RenderCamera.Render();
    }

    public void PicJoint()
    {
        //Camera RenderCamera = RCObjs[0].GetComponent<Camera>();
       // print(RenderCamera.name);
        //Camera RenderCamera = RCObjs[RCObjs.Count-1-i].GetComponent<Camera>();
        RenderTexture.active = RenderCamera.targetTexture;
        Rect rect = new Rect(0, 0, RenderCamera.targetTexture.width, RenderCamera.targetTexture.height);
        VideoBackgroundTexure = new Texture2D(RenderCamera.targetTexture.width, RenderCamera.targetTexture.height, TextureFormat.RGB24, false);
        VideoBackgroundTexure.ReadPixels(rect, 0, 0);
        VideoBackgroundTexure.Apply();

        //Texture2D BackgroundTexure = new Texture2D(RenderCamera.targetTexture.width, RenderCamera.targetTexture.height, TextureFormat.RGB24, false);

        //BackgroundTexure.ReadPixels(rect, 0, 0);
        //BackgroundTexure.Apply();
        //print(BackgroundTexure.width);
        //print(BackgroundTexure.height);

    }

    /* public void PicJoint()
     {
         for (int i = 0; i < RCObjs.Count; i++)
         {
             Camera RenderCamera = RCObjs[i].GetComponent<Camera>();
             //Camera RenderCamera = RCObjs[RCObjs.Count-1-i].GetComponent<Camera>();
             RenderTexture.active = RenderCamera.targetTexture;
             Rect rect = new Rect(0, 0, RenderCamera.targetTexture.width, RenderCamera.targetTexture.height);
             Texture2D BackgroundTexure = new Texture2D(RenderCamera.targetTexture.width, RenderCamera.targetTexture.height, TextureFormat.RGB24, false);
             BackgroundTexure.ReadPixels(rect, 0, 0);
             BackgroundTexure.Apply();
             //print(BackgroundTexure.width);
             //print(BackgroundTexure.height);
             for (int j = 0; j < screenH; j++)
             {
                 for (int z = 0; z < screenW; z++)
                 {
                     pixels[(z + j * screenW)+(screenH*screenW)*i] = BackgroundTexure.GetPixel(z,j);
                     //pixels[(RCObjs.Count - 1)*(screenH * screenW)- (screenH * screenW) * i- (z + j * screenW)] = BackgroundTexure.GetPixel(z, j);
                 }
             }

         }
         VideoBackgroundTexure.SetPixels(pixels);
         VideoBackgroundTexure.Apply();  

     }*/


    void Update()
    {
        //bgObj2.GetComponent<MeshRenderer>().material.mainTexture = bgObjV.GetComponent<MeshRenderer>().material.mainTexture;
    }

    public void GetReviseTexture()
    {

    }
}
