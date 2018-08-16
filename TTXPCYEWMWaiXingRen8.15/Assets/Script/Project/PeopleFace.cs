using UnityEngine;
using System.Collections;
using EasyAR;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PeopleFace : MonoBehaviour {
    public GameObject faceAllObj;
    public GameObject RenderCameraObj;
    public List<GameObject> FacePlanes=new List<GameObject>();
    public Camera mainCamera;
    public bool isClosefront=true;
    private bool isShowFace = false;
    private float ratio;
    private GameObject RCObj;

    private int cameraNumber;
    private float cameraWidth;
    private float cameraHeight;
    private float screenRatio;
    private Color[] pixels;
    private Color[] tempPixels;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public Texture2D VideoBackgroundTexure2 = null;
    private List<GameObject> RCObjs = new List<GameObject>();
    public Texture2D bigMap;
	public static Texture2D faceMap;
    private CreateFolders createFolder;
    public static Texture2D faceZheZhao1;
    public static Texture2D faceZheZhao2;

    private byte[] byteFace;
	private AddPic addPic;

	private Rect rect;
	private Texture2D BackgroundTexure;
	public GameObject NeiCun;
    // Use this for initialization
    void Start() {
        screenRatio = (float)Screen.height / Screen.width;
        startPosition = new Vector3(0,0,0);
        endPosition = new Vector3(0,2.1f,0);
        isClosefront = true;
        createFolder = this.GetComponent<CreateFolders>();
		addPic = this.GetComponent<AddPic>();
        for (int i = 0; i < FacePlanes.Count; i++)
        {
            FacePlanes[i].SetActive(false);
        }
		int VideoBackgroundTexureHeight = Screen.height * 3;
        pixels = new Color[Screen.width * VideoBackgroundTexureHeight];
        VideoBackgroundTexure2 = new Texture2D(Screen.width, Screen.height * 3, TextureFormat.RGB24, false);
		//faceMap = new Texture2D (576,324);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShowGrade.ispeopleFace == true)
        {
            ShowGrade.ispeopleFace = false;
        }
        PeoPleFaceShow();
        if (isShowFace)
        {
            MatchAnimalName(AddPic.curAnimalName).GetComponent<MeshRenderer>().material.mainTexture = RenderCameraObj.GetComponent<MeshRenderer>().material.mainTexture;
			//faceplane.GetComponent<MeshRenderer>().material.mainTexture = RenderCameraObj.GetComponent<MeshRenderer>().material.mainTexture;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.collider.name == "queding")
                {
                    QueDing();
                }
                if (hitinfo.collider.name == "quxiao")
                {
                    QuXiao();
                }
            }
        }
		if ((float)Profiler.GetTotalReservedMemory () / 1024 / 1024 / 1024 > 2.5f) {
			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
		//NeiCun.GetComponent<Text> ().text = ((float)Profiler.GetTotalReservedMemory ()/1024/1024/1024).ToString();
    }

    private void QueDing()
    {
        CutBigMap();
        //CutFaceMap(new Rect(0.25f,0.25f,0.5f,0.5f));
		/*if (addPic.curAnimalObj.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture != null)
        {
			Destroy(addPic.curAnimalObj.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture);
            //Resources.UnloadAsset(AddPic.curAnimalObj.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture);
			addPic.curAnimalObj.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = null;
        }*/
		addPic.curAnimalObj.transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = faceMap;
		addPic.curAnimalObj.transform.GetChild(3).gameObject.SetActive(true);
		print (addPic.curAnimalObj.name);
        //iTween.MoveTo(faceAllObj, iTween.Hash("position", endPosition, "time", 1f, "islocal", true));
        //faceAllObj.transform.localPosition=endPosition;
        faceAllObj.SetActive(false);
        //Invoke("OpenBack",0.8f);
        OpenBack();
    }

    private void OpenBack()
    {
		isShowFace = false;
        MatchAnimalName(AddPic.curAnimalName).GetComponent<MeshRenderer>().material.mainTexture = null;
		//faceplane.GetComponent<MeshRenderer> ().material.mainTexture = null;
        MatchAnimalName(AddPic.curAnimalName).SetActive(false);
        CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Stop();
        if(!Manager.cameraDeviceType)
         CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Open(CameraDevice.Device.Back);
        else
         CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Open(CameraDevice.Device.Front);
        CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Start();
        //ARIsEasyBehaviour.tempImageTargetBaseBehaviour.Clear();
        //ARIsEasyBehaviour.tempImageTargetBaseBehaviour = null;
        /*List<ImageTrackerBaseBehaviour> trackers = ARBuilder.Instance.TrackerBehaviours;
        foreach (ImageTrackerBaseBehaviour itbb in trackers)
            itbb.StartTrack();*/
        ARBuilder.Instance.EasyBuild();
        //print(ARIsEasyBehaviour.tempImageTargetBaseBehaviour.Count);
       /* for (int i = 0; i < ARIsEasyBehaviour.tempImageTargetBaseBehaviour.Count; i++)
        {
            ARBuilder.Instance.TrackerBehaviours[0].LoadImageTargetBehaviour(ARIsEasyBehaviour.tempImageTargetBaseBehaviour[i]);
        }*/
        isClosefront = true;
        isShowFace = false;
        //FacePlane.GetComponent<MeshRenderer>().material.SetTexture("_MaskTexTX", null);
        //FacePlane.GetComponent<MeshRenderer>().material.SetTexture("_TsTex", null);
        //Resources.UnloadAsset(faceZheZhao1);
        //Resources.UnloadAsset(faceZheZhao2);
        //DestroyImmediate (faceZheZhao1);
        //DestroyImmediate (faceZheZhao2);
        //faceZheZhao1 = null;
        //faceZheZhao2 = null;

        //Destroy(Manager.animalMap);
        //Destroy(Manager.nameMap);
        /*Manager.animalMap = null;
        Manager.nameMap = null;
        faceMap = null; */
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        /* if (Manager.isEWM)
         {
             foreach (var behaviour in ARBuilder.Instance.AugmenterBehaviours)
             {
                 behaviour.TextMessage += OnTextMessage;
             }
         }*/
        EWMMethod();
    }
    private void EWMMethod()
    {
        AddPic.curEWMString = null;
        AddPic.isPromptEWM = false;
        AddPic.isEWMDisappear = false;
        AddPic.isFirst =true;
        AddPic.isFirst2 = false;
    }
    private void OnTextMessage(AugmenterBaseBehaviour augmenterBehaviour, string text)
    {
       AddPic.curEWMString = text;
       //print(text);
    }
    private void QuXiao()
    {
        //iTween.MoveTo(faceAllObj, iTween.Hash("position", endPosition, "time", 1f, "islocal", true));
        //faceAllObj.transform.localPosition = endPosition;
        //Invoke("Shang",0.5f);
        //Invoke("OpenBack", 1);
        OpenBack();
        faceAllObj.SetActive(false);
        //faceAllObj.transform.localPosition = endPosition;
    }
    private void Shang()
    {
        faceAllObj.transform.localPosition = endPosition;
    }
    void PeoPleFaceShow()
    {
        if (ShowGrade.isTiQian)
        {           
            isClosefront = false;
            /*foreach (ImageTrackerBaseBehaviour IBB in ARBuilder.Instance.TrackerBehaviours)
            {
                List<ImageTargetBaseBehaviour> a = IBB.LoadedTargetBehaviours;
                for (int i = 0; i < a.Count; i++)
                {
                    IBB.UnloadImageTargetBehaviour(a[i]);
                }
            }*/
           /* for (int i = 0; i < ARIsEasyBehaviour.tempImageTargetBaseBehaviour.Count; i++)
            {
                ARBuilder.Instance.TrackerBehaviours[0].UnloadImageTargetBehaviour(ARIsEasyBehaviour.tempImageTargetBaseBehaviour[i]);
            }*/
            CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Stop();
            if (!Manager.cameraDeviceType)
                CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Open(CameraDevice.Device.Front);
            else
                CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Open(CameraDevice.Device.Back);
            CameraDeviceBehaviour.cameraDeviceBehaviour.Device.Start();
            /*List < ImageTrackerBaseBehaviour > trackers = ARBuilder.Instance.TrackerBehaviours;
            foreach (ImageTrackerBaseBehaviour itbb in trackers)
                itbb.StopTrack(); */
                 //ARBuilder.Instance.EasyBuild();
            ShowGrade.isTiQian = false;
			MatchAnimalName(AddPic.curAnimalName).SetActive(true);
            isShowFace = true;
            Invoke("Initialize",1);
			//Initialize();
            Invoke("XiaLai",2);
			//iTween.MoveTo(faceAllObj, iTween.Hash("position", startPosition, "time",1.5f,"islocal",true));
			//faceAllObj.transform.localPosition=startPosition;
            //HuanZheZhao();
            //FacePlane.GetComponent<MeshRenderer>().material.SetTexture("_MaskTexTX", faceZheZhao1);
            //FacePlane.GetComponent<MeshRenderer>().material.SetTexture("_TsTex", faceZheZhao2);
           
            //FacePlane.transform.localScale = new Vector3(FacePlane.transform.localScale.x, FacePlane.transform.localScale.y, ratio*FacePlane.transform.localScale.x);
        }
    }
    GameObject MatchAnimalName(string animalName)
    {
        for (int i = 0; i < FacePlanes.Count; i++)
        {
            if (FacePlanes[i].name == animalName)
            {
                return FacePlanes[i];
            }
        }
        return FacePlanes[0];
    }
    void HuanZheZhao()
    {
        faceZheZhao1 = (Texture2D)Resources.Load("zhezhao/"+AddPic.curAnimalName+"1");
        faceZheZhao2 = (Texture2D)Resources.Load("zhezhao/" + AddPic.curAnimalName + "2");

    }
    void XiaLai()
    {
        //iTween.MoveTo(faceAllObj, iTween.Hash("position", startPosition, "time",1.5f,"islocal",true));
        faceAllObj.SetActive(true);
    }

    private void createCamera(float y)
    {
        RCObj = new GameObject("RenderCamera2");
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
        RCObjs.Add(RCObj);

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

    void Initialize()
    {
        cameraWidth = Camera.main.gameObject.transform.GetChild(0).localScale.x;
        cameraHeight = Camera.main.gameObject.transform.GetChild(0).localScale.z;
        //print(screenRatio);
        float smallCameraHeight = (float)cameraWidth * screenRatio;
        cameraNumber = (int)Math.Floor((float)cameraHeight / (int)smallCameraHeight);
        //cameraNumber = (int)Math.Floor((double)cameraHeight / smallCameraHeight);
        //print(Math.Floor((double)cameraHeight / smallCameraHeight));
      
        //print(Math.Floor((double)cameraHeight));
        //print(smallCameraHeight);
        //print(cameraNumber);
        if (RCObjs.Count == 0)
        {
            for (int i = 0; i < cameraNumber; i++)
            {
                //print(cameraHeight * 0.5f - smallCameraHeight * 0.5f);
                createCamera(cameraHeight * 0.5f - smallCameraHeight * 0.5f - smallCameraHeight * i);
            }
        }
        /*if (VideoBackgroundTexure2 == null)
        {
			pixels = null;
			VideoBackgroundTexure2 = null;
            int VideoBackgroundTexureHeight = Screen.height * cameraNumber;
            pixels = new Color[Screen.width * VideoBackgroundTexureHeight];
            VideoBackgroundTexure2 = new Texture2D(Screen.width, Screen.height * cameraNumber, TextureFormat.RGB24, false);
        }*/
        //bgObj.transform.localScale = new Vector3(bgObj.transform.localScale.x * (cameraWidth/ cameraHeight), bgObj.transform.localScale.x, bgObj.transform.localScale.x);
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
             for (int j = 0; j < Screen.height; j++)
             {
                 for (int z = 0; z < Screen.width; z++)
                 {
                     pixels[(z + j * Screen.width)+(Screen.height * Screen.width) *i] = BackgroundTexure.GetPixel(z,j);
                     //pixels[(RCObjs.Count - 1)*(Screen.height * Screen.width) - (Screen.height * Screen.width) * i- (z + j * Screen.width)] = BackgroundTexure.GetPixel(z, j);
                 }
             }

         }
         VideoBackgroundTexure2.SetPixels(pixels);
         VideoBackgroundTexure2.Apply();  
     }*/
    public void PicJoint()
    {
        for (int i = RCObjs.Count-1; i >=0; i--)
        {
            Camera RenderCamera = RCObjs[i].GetComponent<Camera>();
            //Camera RenderCamera = RCObjs[RCObjs.Count-1-i].GetComponent<Camera>();
            RenderTexture.active = RenderCamera.targetTexture;
			//print (RenderCamera.targetTexture.width+"#######"+RenderCamera.targetTexture.height);
            rect = new Rect(0, 0, RenderCamera.targetTexture.width, RenderCamera.targetTexture.height);
            BackgroundTexure = new Texture2D(RenderCamera.targetTexture.width, RenderCamera.targetTexture.height, TextureFormat.RGB24, false);
            BackgroundTexure.ReadPixels(rect, 0, 0);
            BackgroundTexure.Apply();
            //print(BackgroundTexure.width);
            //print(BackgroundTexure.height);
            for (int j = 0; j < Screen.height; j++)
            {				
                for (int z = 0; z < Screen.width; z++)
                {
					pixels[(int)(z + j * (Screen.width))+(int)(Screen.height * Screen.width) *(RCObjs.Count-1-i)] = BackgroundTexure.GetPixel(z,j);
                    //pixels[(RCObjs.Count - 1)*(Screen.height * Screen.width) - (Screen.height * Screen.width) * i- (z + j * Screen.width)] = BackgroundTexure.GetPixel(z, j);
                }
            }

        }
        VideoBackgroundTexure2.SetPixels(pixels);
        VideoBackgroundTexure2.Apply();  
    }
    public void CutBigMap()
    {
        PicJoint();
		//faceMap = null;
		faceMap = new Texture2D (576,324);
		CutPic (VideoBackgroundTexure2,faceMap);
        //faceMap = VideoBackgroundTexure2;
        byteFace = faceMap.EncodeToJPG();
        /*print(AddPic.curAnimalName);
        print(AddPic.curAnimalIndex.ToString());
        print(Application.dataPath + "/" + createFolder.rootDirectory + "/" + AddPic.curAnimalName + "/" + AddPic.curAnimalIndex.ToString() + "/" + "faceMap.jpg");*/
        string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + AddPic.curAnimalName + "/" + AddPic.curAnimalIndex.ToString() + "/" + "faceMap.jpg";     
        System.IO.File.WriteAllBytes(filename, byteFace);
        byteFace = null;
    }
	private void CutPic(Texture2D src,Texture2D dst)
	{
		int w0 = src.width;
		int h0 = src.height;
		int w1 = dst.width;
		int h1 = dst.height;
		float fw=(float)w0/w1;
		float fh=(float)h0/h1;
		int x0, y0;
		for (int y = 0; y < h1; y++) {
			y0=(int)(y*fh);
			for (int x = 0; x < w1; x++) {
				x0=(int)(x*fw);
				dst.SetPixel(x,y,src.GetPixel(x0,y0));
			}
		}
		dst.Apply();
	}
    public void CutFaceMap(Rect sourceRect)
    {
        PicJoint();
        int x = Mathf.FloorToInt(VideoBackgroundTexure2.width * sourceRect.x);
        int y = Mathf.FloorToInt(VideoBackgroundTexure2.height * sourceRect.y);
        int width = Mathf.FloorToInt(VideoBackgroundTexure2.width * sourceRect.width);
        int height = Mathf.FloorToInt(VideoBackgroundTexure2.height * sourceRect.height);
        Color[] pix = VideoBackgroundTexure2.GetPixels(x, y, width, height);
        faceMap = new Texture2D(width, height);
        faceMap.SetPixels(pix);
        faceMap.Apply();
        byte[] bytes = faceMap.EncodeToPNG();
        string filename = Application.dataPath + "/Face.png";
        System.IO.File.WriteAllBytes(filename, bytes);
    }
		
}
