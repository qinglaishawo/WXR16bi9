using UnityEngine;
using System.Collections;//using Vuforia;
using System.Runtime.InteropServices;
using System;
using EasyAR;

public class ImageTargetImageManagePlus : MonoBehaviour
{
    [DllImport("realtimecut")]
    private static extern int getPfromPlugins(int localx, int localy, int i_dest_w, int i_dest_h, double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4, double[] cp);
    [DllImport("realtimecut")]
    private static extern int getPixelfromPlugins(int webCamTW, int webCamTH, int pl, int pt, double[] cp, int destw, int desth, Color32[] source, int bits, byte[] inPixels);

    private double[] cp = new double[8];
    private int webCamTW = 0;
    private int webCamTH = 0;
    private int screenW = 0;
    private int screenH = 0;
    private float viewW = 0;
    private float viewH = 0;
    private int alX = 0;
    private int alY = 0;
    private float aspectS = 0;
    private float aspectT = 0;
    private float viewScreenMultiple = 0;
    private bool initialize = false;
    private Vector3[] aps = new Vector3[4];
    private Vector3[] apw = new Vector3[4];
    private int textureW = 400;
    private int textureH = 300;
    private static Color32[] pixels;
    private Texture2D VideoBackgroundTexure = null;
    private int bits = 3;
    private GameObject target;
    private GameObject main;
    private byte[] inPixels;
    private Camera RenderCamera;
    private RenderTexture renderTexture;
    private static Texture2D mainTexture;
    private static ImageTargetImageManagePlus instance;
    public static ImageTargetImageManagePlus Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject temp = new GameObject("ImageTargetManage");
                instance = temp.AddComponent<ImageTargetImageManagePlus>();
                instance.Initialize();
            }
            return instance;
        }
    }

    void Initialize()
    {
        if (!initialize)
        {
            mainTexture = new Texture2D(textureW, textureH, TextureFormat.RGB24, false, true);
            pixels = new Color32[mainTexture.width * mainTexture.height];
            screenW = Screen.width;
            screenH = Screen.height;

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
            initialize = true;
        }
    }

    /*public bool TestInRect(TargetContent trackerContent)
    {
        Vector2 size = trackerContent.Size;
        float sizeRatio = size.y / size.x;
        Vector3[] apl = new Vector3[] { new Vector3(-0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * -0.5f), new Vector3(-0.5f, 0, sizeRatio * -0.5f) };
        for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = trackerContent.ImageTargetObject.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = Camera.main.WorldToScreenPoint(apw[i]);
            if (aps[i].x < 0 || aps[i].x > Screen.width || aps[i].y < 0 || aps[i].y > Screen.height)
                return false;
        }
        return true;
    }*/

    /*public bool TestInRect(TargetContent trackerContent, Rect rect)
    {
        if (rect.height * rect.width <= 0.001f)
            rect = new Rect(0, 0, Screen.width, Screen.height);
        Vector2 size = trackerContent.Size;
        float sizeRatio = size.y / size.x;
        Vector3[] apl = new Vector3[] { new Vector3(-0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * -0.5f), new Vector3(-0.5f, 0, sizeRatio * -0.5f) };
        for (int i = 0; i < apl.Length; i++)
        {
            apw[i] = trackerContent.ImageTargetObject.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
            aps[i] = Camera.main.WorldToScreenPoint(apw[i]);
            //if (aps[i].x < 0 || aps[i].x > Screen.width || aps[i].y < 0 || aps[i].y > Screen.height)
            if (!rect.Contains(aps[i]))
                return false;
        }
        return true;
    }*/

    public void GetCutImage(GameObject target, GameObject main)
    {
        this.target = target;
        this.main = main;
        GetVideoBackgroundTexure();
        GetReviseTexture();
    }

    public Texture2D GetCutImage(GameObject target)
    {
        this.target = target;
        GetVideoBackgroundTexure();
        GetReviseTexture();
        return mainTexture;
    }

    private void GetVideoBackgroundTexure()
    {
         RenderTexture.active = RenderCamera.targetTexture;
         Rect rect = new Rect(0, 0, RenderCamera.targetTexture.width, RenderCamera.targetTexture.height);
         VideoBackgroundTexure = new Texture2D(RenderCamera.targetTexture.width, RenderCamera.targetTexture.height, TextureFormat.RGB24, false);
         VideoBackgroundTexure.ReadPixels(rect, 0, 0);
         VideoBackgroundTexure.Apply();
         webCamTW = VideoBackgroundTexure.width;
         webCamTH = VideoBackgroundTexure.height;
        //VideoBackgroundTexure = ImageTargetImageManageTTXPC.Instance.VideoBackgroundTexure;
    }

    //private void GetInitializeParameter()
    //{
    //    screenW = Screen.width;
    //    screenH = Screen.height;
    //    aspectS = screenW / (float)screenH;
    //    webCamTW = VideoBackgroundTexure.width;
    //    webCamTH = VideoBackgroundTexure.height;
    //    aspectT = webCamTW / (float)webCamTH;
    //    if (aspectS >= aspectT)
    //    {
    //        viewW = webCamTW;
    //        viewH = viewW / aspectS;
    //    }
    //    else
    //    {
    //        viewH = webCamTH;
    //        viewW = viewH * aspectS;
    //    }
    //    viewScreenMultiple = viewW / (float)screenW;
    //    alX = Mathf.RoundToInt((webCamTW - screenW * viewScreenMultiple) * 0.5f);
    //    alY = Mathf.RoundToInt((webCamTH - screenH * viewScreenMultiple) * 0.5f);
    //    DebugScript.debug("webCamTW = " + webCamTW.ToString());
    //    DebugScript.debug("webCamTH = " + webCamTH.ToString());
    //}

    private bool GetReviseTexture()
    {
        if (initialize)
        {
            Vector2 size = target.GetComponent<ImageTargetBehaviour>().Size;
            float x = size.x;
            float z = size.y;
            float sizeRatio = size.y / size.x;
            Vector3[] apl = new Vector3[] { new Vector3(-0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * 0.5f), new Vector3(0.5f, 0, sizeRatio * -0.5f), new Vector3(-0.5f, 0, sizeRatio * -0.5f) };
            for (int i = 0; i < apl.Length; i++)
            {
                apw[i] = target.transform.localToWorldMatrix.MultiplyPoint(apl[i]);
                aps[i] = Camera.main.WorldToScreenPoint(apw[i]);
                //aps[i].x = aps[i].x;// * viewScreenMultiple + alX;
                //aps[i].y = (aps[i].y);// * viewScreenMultiple + alY;
            }

            //Computer test
            getP(1, 1, mainTexture.width, mainTexture.height, aps[3].x, aps[3].y, aps[2].x, aps[2].y, aps[1].x, aps[1].y, aps[0].x, aps[0].y, cp);

            // Iphone test
            //getPfromPlugins(1, 1, mainTexture.width, mainTexture.height, aps[3].x, aps[3].y, aps[2].x, aps[2].y, aps[1].x, aps[1].y, aps[0].x, aps[0].y, cp);


            //Computer test
            inPixels = VideoBackgroundTexure.GetRawTextureData();
            getPixel(1, 1, cp, mainTexture, pixels, bits);
            // Iphone test
            //getPixelfromPlugins(webCamTW, webCamTH, 1, 1, cp, mainTexture.width, mainTexture.height, pixels, bits, inPixels);

            mainTexture.SetPixels32(pixels);
            //for (int Y = 0; Y < mainTexture.height; Y++)
            //{
            //    for (int X = 0, pos = Y * mainTexture.width; X < mainTexture.width; X++, pos++)
            //    {
            //        Vector4 colorUP = mainTexture.GetPixel(X, Y);

            //        mainTexture.SetPixel(X, Y, colorUP * 1.1f);
            //    }
            //}
            mainTexture.Apply();
            if (main != null)
                main.GetComponent<Renderer>().sharedMaterial.mainTexture = mainTexture;
            return true;
        }
        return false;
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
