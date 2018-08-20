using UnityEngine;
using System.Collections;
using EasyAR;
using System;


public class Manager : MonoBehaviour {
    public Camera mainCamera;
    public static bool isEWM=true;
    public  GameObject _ImageTargetObj;
    private Vector3[] aps = new Vector3[4];
    private Vector3[] apw = new Vector3[4];
    private GameObject augmenter;
    private GameObject RCObj1;
    private GameObject RCObj2;
    private GameObject RCObj3;
    private int webCamTW = 0;
    private int webCamTH = 0;
    private byte[] inPixels;
    public static Texture2D mainTexture;
    public static int textureW = 573;
    public static int textureH = 350;
    private static Color32[] pixels;
    private double[] cp = new double[8];
    private int bits = 3;
    public static Texture2D animalMap;
    public static Texture2D nameMap;
    private CreateFolders createFolder;
    public bool isDotContain=false;
     
    public static bool cameraDeviceType = true;

    public static float Timer;
    public static bool isStartGuaiWu;
    public static int TimerState;
    public static bool isStartLongJuanFeng;
    public GuaiWuCode guaiWuCode;
    public LongJuanFeng longJuanFeng;
    public GameObject GuaiWuImage;
    public GameObject LongJuanFengImage;
    //private GameObject cameraDeviceT;
    // Use this for initializations
    void Start () {
        //cameraDeviceT = GameObject.Find("CameraDevice");
        textureW = 573;
        textureH = 350;
        augmenter = GameObject.Find("Augmenter");
        mainTexture = new Texture2D(textureW, textureH, TextureFormat.RGB24, false, true);
        
        pixels = new Color32[textureW * textureH];
        createFolder = this.GetComponent<CreateFolders>();
        TimerState = 0;
        Timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                GameObject gameObj = hitinfo.collider.gameObject;
                if (gameObj.GetComponent<AnimalIndividuality>() != null)
                {
                    if (gameObj.GetComponent<Animation>().IsPlaying("dj_0"))
                    {
                        if (gameObj.GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                        {
                            gameObj.GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                        }
                    }
                }
                if (gameObj.tag == "people")
                {
                    if (gameObj.GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                    {
                        gameObj.GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                    }
                }
                gameObj = null;
            }
        }

        if(Time.time-Timer>=10&& isStartGuaiWu == false&& TimerState==0)
        {
            print("怪物出现");
            TimerState = 1;
            isStartGuaiWu = true;
            StartCoroutine(guaiWuCode.StartAppear());
            Timer = Time.time;
            GuaiWuImage.SetActive(true);
        }
        if(TimerState == 2&& isStartLongJuanFeng==false&& Time.time - Timer >= 10)
        {
            print("龙卷风出现");
            TimerState = 3;
            isStartLongJuanFeng = true;
            StartCoroutine(longJuanFeng.StartAppear());
            Timer = Time.time;
            LongJuanFengImage.SetActive(true);
        }
    }

    public void Found(GameObject ImageTargetObj)
    {
        _ImageTargetObj=ImageTargetObj;
        print(ImageTargetObj.name);
        this.GetComponent<AddPic>().curSignName = ImageTargetObj.name;
    }

    public void Lost(GameObject ImageTargetObj)
    {
        _ImageTargetObj = ImageTargetObj;
        this.GetComponent<AddPic>().curSignName = null;
    }

    public void JudgeDotContain()
    {
        int index=0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        RCObj2 = augmenter.transform.GetChild(1).gameObject; 
        Vector2 size = _ImageTargetObj.GetComponent<ImageTargetBehaviour>().Size;
        float x = size.x;
        float z = size.y;
        float sizeRatio = size.y / size.x;
        Vector3[] apl = new Vector3[] { new Vector3(-0.5f, 0, sizeRatio * 0.5f), new Vector3(0.375f, 0, sizeRatio * 0.5f), new Vector3(0.375f, 0, sizeRatio * -0.5f), new Vector3(-0.5f, 0, sizeRatio * -0.5f) };

        for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj2.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
            //Debug.Log(aps[i].x.ToString() + " +++++++++++++++++++++++++++++++ " + aps[i].y.ToString());
            if (rect.Contains(new Vector2(aps[i].x, aps[i].y)))
            {
                index++;
            }
        }
        if (index == apl.Length)
        {
            isDotContain = true;
        }
        else
        {
            isDotContain = false;
            
        }
    }
   
    public void GetReviseTexture()
    {
        RCObj1 = augmenter.transform.GetChild(3).gameObject;
        RCObj2 = augmenter.transform.GetChild(2).gameObject;
        RCObj3 = augmenter.transform.GetChild(1).gameObject;
        webCamTW = ImageTargetImageManageTTXPC.VideoBackgroundTexure.width;
        webCamTH = ImageTargetImageManageTTXPC.VideoBackgroundTexure.height;
        print(webCamTW);
        print(webCamTH);
        Vector2 size = _ImageTargetObj.GetComponent<ImageTargetBehaviour>().Size;
        float x = size.x;
        float z = size.y;
        float sizeRatio = size.y / size.x;
        Vector3[] apl = new Vector3[] { new Vector3(-0.5f, 0, sizeRatio * 0.5f), new Vector3(0.375f, 0, sizeRatio * 0.5f), new Vector3(0.375f, 0, sizeRatio * -0.5f), new Vector3(-0.5f, 0, sizeRatio * -0.5f) };
        /*for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj1.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
            Debug.Log(aps[i].x.ToString() + " ************************************ " + aps[i].y.ToString());
            //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
            //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
        }*/
        for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj2.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
            Debug.Log(aps[i].x.ToString() + " ************************************ " + aps[i].y.ToString());

            //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
            //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
        }
        aps[0].x = aps[0].x;
        //aps[1].x = aps[1].x - 10f;
        //aps[2].x = aps[2].x - 10f;
        aps[3].x = aps[3].x;

        aps[0].y = aps[0].y+180 ;
        aps[1].y = aps[1].y+180 ;
        aps[2].y = aps[2].y ;
        aps[3].y = aps[3].y ;
        /*for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj3.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
            Debug.Log(aps[i].x.ToString() + " ************************************ " + aps[i].y.ToString());
            //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
            //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
        }*/

        getP(1, 1, mainTexture.width, mainTexture.height, aps[3].x, aps[3].y, aps[2].x, aps[2].y, aps[1].x, aps[1].y, aps[0].x, aps[0].y, cp);
        inPixels = ImageTargetImageManageTTXPC.VideoBackgroundTexure.GetRawTextureData();
        getPixel(1, 1, cp, mainTexture, pixels, bits);
        mainTexture.SetPixels32(pixels);
        for (int Y = 0; Y < mainTexture.height; Y++)
        {
            for (int X = 0, pos = Y * mainTexture.width; X < mainTexture.width; X++, pos++)
            {
                Vector4 colorUP = mainTexture.GetPixel(X, Y);

                mainTexture.SetPixel(X, Y, colorUP * 1.1f);
            }
        }
        mainTexture.Apply();

        byte[] bytes = mainTexture.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot2.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        //CutAdjustment(new Rect(0, 0.25f, 1, 0.75f));
        //CutAdjustment(new Rect(0.05f,0.25f,0.95f,0.75f));
        CutAnimalMap(new Rect(0,0,1,1));
        //CutNameMap(new Rect(0.870f,0.255f,0.125f,0.7f));
    }

    public void GetReviseTexture2()
    {
        //RCObj1 = augmenter.transform.GetChild(3).gameObject;
       // RCObj2 = augmenter.transform.GetChild(2).gameObject;
        //RCObj3 = augmenter.transform.GetChild(1).gameObject;
		RCObj2 = augmenter.transform.GetChild(1).gameObject;
        webCamTW = ImageTargetImageManageTTXPC.VideoBackgroundTexure.width;
        webCamTH = ImageTargetImageManageTTXPC.VideoBackgroundTexure.height;
        //print(webCamTW);
        //print(webCamTH);
        Vector2 size = _ImageTargetObj.GetComponent<ImageTargetBehaviour>().Size;
        //float x = size.x;
        //float z = size.y;
        float sizeRatio = size.y / size.x;
        Vector3[] apl = new Vector3[] { new Vector3(-0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * -0.5f), new Vector3(-0.5f, 0, sizeRatio * -0.5f) };
        /*for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj1.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
            Debug.Log(aps[i].x.ToString() + " ************************************ " + aps[i].y.ToString());
            //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
            //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
        }*/
        for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj2.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
           // Debug.Log(aps[i].x.ToString() + " ************************************ " + aps[i].y.ToString());

            //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
            //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
        }
       /* aps[0].x = aps[0].x;
        //aps[1].x = aps[1].x - 10f;
        //aps[2].x = aps[2].x - 10f;
        aps[3].x = aps[3].x;

		//aps [0].y = aps [0].y + 1100;
	    //aps[1].y = aps[1].y+1100;
        aps[2].y = aps[2].y;
        aps[3].y = aps[3].y;*/
        /*for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = _ImageTargetObj.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = RCObj3.GetComponent<Camera>().WorldToScreenPoint(apw[i]);
            Debug.Log(aps[i].x.ToString() + " ************************************ " + aps[i].y.ToString());
            //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
            //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
        }*/

        getP(1, 1, mainTexture.width, mainTexture.height, aps[3].x, aps[3].y, aps[2].x, aps[2].y, aps[1].x, aps[1].y, aps[0].x, aps[0].y, cp);
        inPixels = ImageTargetImageManageTTXPC.VideoBackgroundTexure.GetRawTextureData();
        getPixel(1, 1, cp, mainTexture, pixels, bits);
        mainTexture.SetPixels32(pixels);
        /*for (int Y = 0; Y < mainTexture.height; Y++)
        {
            for (int X = 0, pos = Y * mainTexture.width; X < mainTexture.width; X++, pos++)
            {
                Vector4 colorUP = mainTexture.GetPixel(X, Y);

                mainTexture.SetPixel(X, Y, colorUP * 1.1f);
            }
        }*/
        mainTexture.Apply();
       /*byte[] bytes = mainTexture.EncodeToJPG();
       string filename = Application.dataPath + "/MianMap.jpg";
       System.IO.File.WriteAllBytes(filename, bytes);*/
        if (isEWM)
        {
            CutPicAdjust();
            //CutNameMap(new Rect(0.8675f, 0.228f, 0.118f, 0.7485f));
        }
        //CutAdjustment(new Rect(0, 0.26f, 1f, 0.74f));
        // CutNameMap(new Rect(0.88f, 0.245f, 0.11f, 0.74f));
        /*byte[] bytes = mainTexture.EncodeToPNG();
         string filename = Application.dataPath + "/Screenshot1.png";
         System.IO.File.WriteAllBytes(filename, bytes);*/
        //CutAdjustment(new Rect(0, 0.26f, 1f, 0.74f));
        /*CutAdjustment(new Rect(0, 0.26f,1f, 0.74f));
        CutAnimalMap(new Rect(0, 0, 0.875f, 1));
        CutNameMap(new Rect(0.88f, 0.245f, 0.11f, 0.74f));*/
    }

    void CutPicAdjust()
    {
        CutAnimalMap(new Rect(0, 0, 0.851f, 1));
    }

    public void CutAdjustment(Rect sourceRect)
    {
        int x = Mathf.FloorToInt(mainTexture.width * sourceRect.x);
        int y = Mathf.FloorToInt(mainTexture.height * sourceRect.y);
        int width = Mathf.FloorToInt(mainTexture.width * sourceRect.width);
        int height = Mathf.FloorToInt(mainTexture.height * sourceRect.height);
        Color[] pix = mainTexture.GetPixels(x, y, width, height);
        animalMap = new Texture2D(width, height);
        animalMap.SetPixels(pix);
        animalMap.Apply();
         byte[] bytes = animalMap.EncodeToPNG();
         string filename = Application.dataPath + "/Screenshot3.png";
         System.IO.File.WriteAllBytes(filename, bytes);
    }
    private void CutAdjustment2(Rect sourceRect)
    {
        int x = Mathf.FloorToInt(mainTexture.width * sourceRect.x);
        int y = Mathf.FloorToInt(mainTexture.height * sourceRect.y);
        int width = Mathf.FloorToInt(mainTexture.width * sourceRect.width);
        int height = Mathf.FloorToInt(mainTexture.height * sourceRect.height);
        Color[] pix = mainTexture.GetPixels(x, y, width, height);
		int w = 720;
		int h = 420;
		int mw = width - x;
		int mh = height;
		Color[] temp = new Color[w * h];
		for (int i = 0; i < h; i++)
		{
			for (int j = 0; j < w; j++) 
			{
				int th = Convert.ToInt32( (i / (float)h) * mh);
				int tw = Convert.ToInt32((j / (float)w) * mw);
				th = Mathf.Clamp (th, 0, mh);
				tw = Mathf.Clamp (tw, 0, mw);
				temp [i * w + j] = pix [th * mw + tw];
			}
		}
        mainTexture = new Texture2D(w, h);
        mainTexture.SetPixels(temp);
        mainTexture.Apply();
        byte[] bytes = mainTexture.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot6.png";
        System.IO.File.WriteAllBytes(filename, bytes);
    }

    public void CutAnimalMap(Rect sourceRect)
    {
        int x = Mathf.FloorToInt(mainTexture.width * sourceRect.x);
        int y = Mathf.FloorToInt(mainTexture.height * sourceRect.y);
        int width = Mathf.FloorToInt(mainTexture.width * sourceRect.width);
        int height = Mathf.FloorToInt(mainTexture.height * sourceRect.height);
        Color[] pix = mainTexture.GetPixels(x, y, width, height);
        //animalMap = null;
        animalMap = new Texture2D(width, height);
        animalMap.SetPixels(pix);
        animalMap.Apply();
        /*byte[] bytes = animalMap.EncodeToJPG();
        string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + AddPic.curAnimalName + "/" +AddPic.curAnimalIndex.ToString() + "/" + "animalMap.jpg";
        print(filename);
        System.IO.File.WriteAllBytes(filename, bytes);*/
        /*byte[] bytes = animalMap.EncodeToPNG();
        string filename = Application.dataPath + "/Temp.png";
        System.IO.File.WriteAllBytes(filename, bytes);*/

    }

    public void CutNameMap(Rect sourceRect)
    {
        int x = Mathf.FloorToInt(mainTexture.width * sourceRect.x);
        int y = Mathf.FloorToInt(mainTexture.height * sourceRect.y);
        int width = Mathf.FloorToInt(mainTexture.width * sourceRect.width);
        int height = Mathf.FloorToInt(mainTexture.height * sourceRect.height);
        Color[] pix = mainTexture.GetPixels(x, y, width, height);
        nameMap = null;
        nameMap = new Texture2D(width, height);
        nameMap.SetPixels(pix);
        nameMap.Apply();
        /*byte[] bytes = nameMap.EncodeToJPG();
        string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + AddPic.curAnimalName + "/" + AddPic.curAnimalIndex.ToString() + "/" + "nameMap.jpg";
        System.IO.File.WriteAllBytes(filename, bytes);*/
    }

    private bool getP(int localx, int localy, int i_dest_w, int i_dest_h, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, double[] cp)
    {

        //UnityEngine.MonoBehaviour.print(x1 + "  " + y1);
        //UnityEngine.MonoBehaviour.print(x2 + "  " + y2);
        //UnityEngine.MonoBehaviour.print(x3 + "  " + y3);
        //UnityEngine.MonoBehaviour.print(x4 + "  " + y4);

        double ltx = localx;
        double lty = localy;
        double rbx = ltx + i_dest_w;
        double rby = lty + i_dest_h;
        double det_1, a13, a14, a23, a24, a33, a34, a43, a44, b11, b12, b13, b14, b21, b22, b23, b24, b31, b32, b33, b34, b41, b42, b43, b44, t1, t2, t3, t4, t5, t6;
        double kx0, kx1, kx2, kx3, kx4, kx5, kx6, kx7, ky0, ky1, ky2, ky3, ky4, ky5, ky6, ky7;
        a13 = -ltx * x1;
        a14 = -lty * x1;
        a23 = -rbx * x2;
        a24 = -lty * x2;
        a33 = -rbx * x3;
        a34 = -rby * x3;
        a43 = -ltx * x4;
        a44 = -rby * x4;
        t1 = a33 * a44 - a34 * a43;
        t4 = a34 * ltx - rbx * a44;
        t5 = rbx * a43 - a33 * ltx;
        t2 = rby * (a34 - a44);
        t3 = rby * (a43 - a33);
        t6 = rby * (rbx - ltx);
        b21 = -a23 * t4 - a24 * t5 - rbx * t1;
        b11 = (a23 * t2 + a24 * t3) + lty * t1;
        b31 = (a24 * t6 - rbx * t2) + lty * t4;
        b41 = (-rbx * t3 - a23 * t6) + lty * t5;
        t1 = a43 * a14 - a44 * a13;
        t2 = a44 * lty - rby * a14;
        t3 = rby * a13 - a43 * lty;
        t4 = ltx * (a44 - a14);
        t5 = ltx * (a13 - a43);
        t6 = ltx * (lty - rby);
        b12 = -rby * t1 - a33 * t2 - a34 * t3;
        b22 = (a33 * t4 + a34 * t5) + rbx * t1;
        b32 = (-a34 * t6 - rby * t4) + rbx * t2;
        b42 = (-rby * t5 + a33 * t6) + rbx * t3;
        t1 = a13 * a24 - a14 * a23;
        t4 = a14 * rbx - ltx * a24;
        t5 = ltx * a23 - a13 * rbx;
        t2 = lty * (a14 - a24);
        t3 = lty * (a23 - a13);
        t6 = lty * (ltx - rbx);
        b23 = -a43 * t4 - a44 * t5 - ltx * t1;
        b13 = (a43 * t2 + a44 * t3) + rby * t1;
        b33 = (a44 * t6 - ltx * t2) + rby * t4;
        b43 = (-ltx * t3 - a43 * t6) + rby * t5;
        t1 = a23 * a34 - a24 * a33;
        t2 = a24 * rby - lty * a34;
        t3 = lty * a33 - a23 * rby;
        t4 = rbx * (a24 - a34);
        t5 = rbx * (a33 - a23);
        t6 = rbx * (rby - lty);
        b14 = -lty * t1 - a13 * t2 - a14 * t3;
        b24 = a13 * t4 + a14 * t5 + ltx * t1;
        b34 = -a14 * t6 - lty * t4 + ltx * t2;
        b44 = -lty * t5 + a13 * t6 + ltx * t3;
        det_1 = (ltx * (b11 + b14) + rbx * (b12 + b13));
        if (det_1 == 0) det_1 = 0.0001;
        det_1 = 1 / det_1;
        kx0 = (b11 * x1 + b12 * x2 + b13 * x3 + b14 * x4) * det_1;
        kx1 = (b11 + b12 + b13 + b14) * det_1;
        kx2 = (b21 * x1 + b22 * x2 + b23 * x3 + b24 * x4) * det_1;
        kx3 = (b21 + b22 + b23 + b24) * det_1;
        kx4 = (b31 * x1 + b32 * x2 + b33 * x3 + b34 * x4) * det_1;
        kx5 = (b31 + b32 + b33 + b34) * det_1;
        kx6 = (b41 * x1 + b42 * x2 + b43 * x3 + b44 * x4) * det_1;
        kx7 = (b41 + b42 + b43 + b44) * det_1;
        a13 = -ltx * y1;
        a14 = -lty * y1;
        a23 = -rbx * y2;
        a24 = -lty * y2;
        a33 = -rbx * y3;
        a34 = -rby * y3;
        a43 = -ltx * y4;
        a44 = -rby * y4;
        t1 = a33 * a44 - a34 * a43;
        t4 = a34 * ltx - rbx * a44;
        t5 = rbx * a43 - a33 * ltx;
        t2 = rby * (a34 - a44);
        t3 = rby * (a43 - a33);
        t6 = rby * (rbx - ltx);
        b21 = -a23 * t4 - a24 * t5 - rbx * t1;
        b11 = (a23 * t2 + a24 * t3) + lty * t1;
        b31 = (a24 * t6 - rbx * t2) + lty * t4;
        b41 = (-rbx * t3 - a23 * t6) + lty * t5;
        t1 = a43 * a14 - a44 * a13;
        t2 = a44 * lty - rby * a14;
        t3 = rby * a13 - a43 * lty;
        t4 = ltx * (a44 - a14);
        t5 = ltx * (a13 - a43);
        t6 = ltx * (lty - rby);
        b12 = -rby * t1 - a33 * t2 - a34 * t3;
        b22 = (a33 * t4 + a34 * t5) + rbx * t1;
        b32 = (-a34 * t6 - rby * t4) + rbx * t2;
        b42 = (-rby * t5 + a33 * t6) + rbx * t3;
        t1 = a13 * a24 - a14 * a23;
        t4 = a14 * rbx - ltx * a24;
        t5 = ltx * a23 - a13 * rbx;
        t2 = lty * (a14 - a24);
        t3 = lty * (a23 - a13);
        t6 = lty * (ltx - rbx);
        b23 = -a43 * t4 - a44 * t5 - ltx * t1;
        b13 = (a43 * t2 + a44 * t3) + rby * t1;
        b33 = (a44 * t6 - ltx * t2) + rby * t4;
        b43 = (-ltx * t3 - a43 * t6) + rby * t5;
        t1 = a23 * a34 - a24 * a33;
        t2 = a24 * rby - lty * a34;
        t3 = lty * a33 - a23 * rby;
        t4 = rbx * (a24 - a34);
        t5 = rbx * (a33 - a23);
        t6 = rbx * (rby - lty);
        b14 = -lty * t1 - a13 * t2 - a14 * t3;
        b24 = a13 * t4 + a14 * t5 + ltx * t1;
        b34 = -a14 * t6 - lty * t4 + ltx * t2;
        b44 = -lty * t5 + a13 * t6 + ltx * t3;
        det_1 = (ltx * (b11 + b14) + rbx * (b12 + b13));
        if (det_1 == 0) det_1 = 0.0001;
        det_1 = 1 / det_1;
        ky0 = (b11 * y1 + b12 * y2 + b13 * y3 + b14 * y4) * det_1;
        ky1 = (b11 + b12 + b13 + b14) * det_1;
        ky2 = (b21 * y1 + b22 * y2 + b23 * y3 + b24 * y4) * det_1;
        ky3 = (b21 + b22 + b23 + b24) * det_1;
        ky4 = (b31 * y1 + b32 * y2 + b33 * y3 + b34 * y4) * det_1;
        ky5 = (b31 + b32 + b33 + b34) * det_1;
        ky6 = (b41 * y1 + b42 * y2 + b43 * y3 + b44 * y4) * det_1;
        ky7 = (b41 + b42 + b43 + b44) * det_1;
        det_1 = kx5 * (-ky7) - (-ky5) * kx7;
        if (det_1 == 0) det_1 = 0.0001;
        det_1 = 1 / det_1;
        double C, F;
        cp[2] = C = (-ky7 * det_1) * (kx4 - ky4) + (ky5 * det_1) * (kx6 - ky6); // C
        cp[5] = F = (-kx7 * det_1) * (kx4 - ky4) + (kx5 * det_1) * (kx6 - ky6); // F
        cp[6] = kx4 - C * kx5;
        cp[7] = kx6 - C * kx7;
        cp[0] = kx0 - C * kx1;
        cp[1] = kx2 - C * kx3;
        cp[3] = ky0 - F * ky1;
        cp[4] = ky2 - F * ky3;
        return true;
    }

    private bool getPixel(int pl, int pt, double[] cp, Texture2D dest, Color32[] source, int bits)
    {
        //int templ = Convert.ToByte(Mathf.Clamp(light * 255, 0, 255));
        int iw = webCamTW;
        int ih = webCamTH;
        double cp0 = cp[0];
        double cp3 = cp[3];
        double cp6 = cp[6];
        double cp1 = cp[1];
        double cp4 = cp[4];
        double cp7 = cp[7];
        int ow = dest.width;
        int oh = dest.height;
        double c1 = cp7 * pt + 1.0f + cp6 * pl;
        double c2 = cp1 * pt + cp[2] + cp0 * pl;
        double c3 = cp4 * pt + cp[5] + cp3 * pl;
        int p = 0;
        for (int iy = 0; iy < oh; iy++)
        {
            double c4 = c1;
            double c5 = c2;
            double c6 = c3;
            for (int ix = 0; ix < ow; ix++)
            {
                double d = 1 / (c4);
                int x = (int)((c5) * d);
                int y = (int)((c6) * d);
                if (x < 0) x = 0; else if (x >= iw) x = iw - 1;
                if (y < 0) y = 0; else if (y >= ih) y = ih - 1;
                int bp = (x + y * iw) * bits;
                c4 += cp6;
                c5 += cp0;
                c6 += cp3;
                //Color32 color = BackGroundVideoColors[bp / 4];
                //source[p].r = color.r;
                //source[p].g = color.g;
                //source[p].b = color.b;

                source[p].r = inPixels[bp];
                source[p].g = inPixels[bp + 1];
                source[p].b = inPixels[bp + 2];
                // 				float r = inPixels[bp]/255;
                // 				float g = inPixels[bp+1]/255;
                // 				float b = inPixels[bp+2]/255;
                //                
                // 				r = ((r - 0.5f) * contrast + 0.5f) * 255;
                // 				g = ((g - 0.5f) * contrast + 0.5f) * 255;
                // 				b = ((b - 0.5f) * contrast + 0.5f) * 255;
                // 				source[p].r = Convert.ToByte(Mathf.Clamp(r + templ, 0, 255));
                // 				source[p].g = Convert.ToByte(Mathf.Clamp(g + templ, 0, 255));
                // 				source[p].b = Convert.ToByte(Mathf.Clamp(b + templ, 0, 255));
                //source[p].a = 255;
                p++;
            }
            c1 += cp7;
            c2 += cp1;
            c3 += cp4;
        }
        return true;
    }
}
