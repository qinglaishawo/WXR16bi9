using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class testTexture1 : MonoBehaviour {
    public Texture2D tuSe;
    public Texture2D wuTuSe;
    public Texture2D JieTu;
    private float ratio;
    public Texture2D tuSeAdjust;
    public Texture2D wuTuSeAdjust;
    private int tuSeAdjustWith=50;
    private Texture2D testTexture;
    private Texture2D testTexture2;
    private List<Vector2> comparePoint = new List<Vector2>();
    private List<Vector3> storeRGB = new List<Vector3>();
    private List<Vector2> testPoint = new List<Vector2>();
    private Texture2D JieTuAdjust;
    private int colorCount;
    private Color yanSe;
    private List<Vector3> daXiangError = new List<Vector3>() {new Vector3(0.6f,0.6f,0.6f), new Vector3(0.8f, 0.8f, 0.9f) ,
        new Vector3(0.4f, 0.5f, 0.5f),new Vector3(0.5f,0.6f,0.6f),new Vector3(0.6f,0.4f,0.3f),new Vector3(0.7f,0.8f,0.9f),new Vector3(0.4f,0.4f,0.4f)};
    // Use this for initialization
    void Start () {
        
        storeRGB.Clear();
        ratio = 1.637f;
        //tuSeAdjust = new Texture2D(573, 350);
        //wuTuSeAdjust = new Texture2D(573, 350);
       // tuSeAdjust = new Texture2D((int)(ratio* tuSeAdjustWith), tuSeAdjustWith);
        //wuTuSeAdjust = new Texture2D((int)(ratio * tuSeAdjustWith), tuSeAdjustWith);
        JieTuAdjust= new Texture2D((int)(ratio * tuSeAdjustWith), tuSeAdjustWith);
        //tuSeAdjust = tuSe;
        //wuTuSeAdjust = wuTuSe;
        //CutPic(tuSe, tuSeAdjust);
        //CutPic(wuTuSe, wuTuSeAdjust);
        /*byte[] bytes1 = tuSeAdjust.EncodeToJPG();
        string filename1 = Application.dataPath + "/tuSeAdjust.jpg";
        System.IO.File.WriteAllBytes(filename1, bytes1);

        byte[] bytes2 = wuTuSeAdjust.EncodeToJPG();
        string filename2 = Application.dataPath + "/wuTuSeAdjust.jpg";
        System.IO.File.WriteAllBytes(filename2, bytes2);*/

        testTexture = null;
        testTexture = new Texture2D((int)(ratio * tuSeAdjustWith), tuSeAdjustWith);
        testTexture2 = null;
        testTexture2 = new Texture2D((int)(ratio * tuSeAdjustWith), tuSeAdjustWith);

        //testTexture = new Texture2D((int)(ratio * tuSeAdjustWith), tuSeAdjustWith);
        //Compare();
        //CompareJieTu();
        //Adjust();
        for (int i = 0; i < testTexture2.height; i++)
        {
            for (int z = 0; z < testTexture2.width; z++)
            {
                testTexture2.SetPixel(z, i, new Color(0.3f,0.6f,0.8f));
            } 
        }                  
        testTexture2.Apply(); 
        byte[] bytes5 = testTexture2.EncodeToJPG(); 
        string filename5 = Application.dataPath + "/testTexture2.jpg";
        System.IO.File.WriteAllBytes(filename5, bytes5);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //比较出涂色范围
    private void Compare()
    {
        for (int i = 0; i < tuSe.height; i++)
        {
            for (int z = 0; z < tuSe.width; z++)
            {
                /*if (wuTuSe.GetPixel(z, i).r -1 > tuSe.GetPixel(z, i).r || tuSe.GetPixel(z, i).r > wuTuSe.GetPixel(z, i).r +1)
                {
                    if (wuTuSe.GetPixel(z, i).b - 1> tuSe.GetPixel(z, i).b || tuSe.GetPixel(z, i).b > wuTuSe.GetPixel(z, i).b + 1)
                    {
                        if(wuTuSe.GetPixel(z, i).g - 1 > tuSe.GetPixel(z, i).g || tuSe.GetPixel(z, i).g > wuTuSe.GetPixel(z, i).g + 1)
                        testTexture.SetPixel(z, i, Color.red);
                    }
                }*/

                /*if (tuSe.GetPixel(z, i) != wuTuSe.GetPixel(z, i))
                {                 
                    //testTexture.SetPixel(z, i, Color.black);
                }*/
                if (tuSe.GetPixel(z, i) != wuTuSe.GetPixel(z, i))
                {
                    if (Mathf.RoundToInt(wuTuSe.GetPixel(z, i).r * 255f) == 255 && Mathf.RoundToInt(wuTuSe.GetPixel(z, i).g * 255f) == 255 && Mathf.RoundToInt(wuTuSe.GetPixel(z, i).b * 255f) == 255)
                    {
                        comparePoint.Add(new Vector2(z,i));
                        testTexture.SetPixel(z, i, Color.black);
                    }
                }
               /* if (wuTuSe.GetPixel(z, i).r >50 && wuTuSe.GetPixel(z, i).g > 50 && wuTuSe.GetPixel(z, i).b > 50)
                {
                    testTexture.SetPixel(z, i, Color.black);
                }*/
               /* if ((tuSe.GetPixel(z, i).r + tuSe.GetPixel(z, i).g + tuSe.GetPixel(z, i).b) == wuTuSe.GetPixel(z, i).r + wuTuSe.GetPixel(z, i).g + wuTuSe.GetPixel(z, i).b||
                    (tuSe.GetPixel(z, i).r + tuSe.GetPixel(z, i).g + tuSe.GetPixel(z, i).b+10) > wuTuSe.GetPixel(z, i).r + wuTuSe.GetPixel(z, i).g + wuTuSe.GetPixel(z, i).b)
                {
                    //if(wuTuSe.GetPixel(z, i).r + wuTuSe.GetPixel(z, i).g + wuTuSe.GetPixel(z, i).b==tuSe.GetPixel(z, i).r + tuSe.GetPixel(z, i).g + tuSe.GetPixel(z, i).b+10)
                    testTexture.SetPixel(z, i, Color.red);
                }*/
            }
        }
        /*for (int i=0; i<tuSeAdjust.height;i++)
        {
            for (int z = 0; z < tuSeAdjust.width; z++)
            {
                if (tuSeAdjust.GetPixel(z, i) != wuTuSeAdjust.GetPixel(z, i))
                {
                    testTexture.SetPixel(z,i,Color.black);
                }
                
            }
        }*/
        testTexture.Apply();
        print(comparePoint.Count);
        byte[] bytes3 = testTexture.EncodeToJPG();
        string filename3 = Application.dataPath + "/testTexture.jpg";
        System.IO.File.WriteAllBytes(filename3, bytes3);
    }

    private void Adjust()
    {
        //CutPic(tuSe, tuSeAdjust);
        //CutPic(wuTuSe, wuTuSeAdjust);
        //CutPic(JieTu, JieTuAdjust);
        byte[] bytes4 = JieTuAdjust.EncodeToJPG();
        string filename4 = Application.dataPath + "/JieTuAdjust.jpg";
        System.IO.File.WriteAllBytes(filename4, bytes4);
        print(tuSeAdjust.width);
        for (int i = 0; i < 64; i++)
        {
            for (int z = 0; z <64; z++)
            {
                if (tuSeAdjust.GetPixel(z, i) != wuTuSeAdjust.GetPixel(z, i))
                {
                    //if (Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).r * 255f) ==0 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).g * 255f) == 0 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).b * 255f) == 0)
                    if (Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).r * 255f) == 255 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).g * 255f) == 255 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).b * 255f) == 255)
                    {
                        comparePoint.Add(new Vector2(z, i));
                        testTexture.SetPixel(z,i, Color.black);
                    }
                }
            }
        }
        testTexture.Apply();
        print(comparePoint.Count);
        byte[] bytes3 = testTexture.EncodeToJPG();
        string filename3 = Application.dataPath + "/testTexture.jpg";
        System.IO.File.WriteAllBytes(filename3, bytes3);
        for (int i = 0; i < comparePoint.Count; i++)
        {
            float r = JieTuAdjust.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).r;   
            int ir = (int)(r * 10);        
            r = ir / 10f;
          
            float g = JieTuAdjust.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).g;
            int ig = (int)(g * 10);
            g = ig / 10f;
            float b = JieTuAdjust.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).b;
            int ib = (int)(b * 10);
            b = ib / 10f;
            if (i == 0)
            {       
                storeRGB.Add(new Vector3(r,g,b));
                testPoint.Add(comparePoint[i]);
                //if(JieTu.GetPixel(comparePoint[i].x,comparePoint[i].y))
            }
            else
            {
                if (!storeRGB.Contains(new Vector3(r, g, b)))
                {
                    if (QuZhiFanWei(storeRGB[storeRGB.Count - 1], new Vector3(r, g, b)))
                    {
                        storeRGB.Add(new Vector3(r, g, b));
                        testPoint.Add(comparePoint[i]);
                    }
                }
            }
        }
        print(storeRGB.Count);

        for (int i = 0; i < testPoint.Count; i++)
        {
            testTexture2.SetPixel((int)testPoint[i].x, (int)testPoint[i].y,new Color(storeRGB[i].x, storeRGB[i].y, storeRGB[i].z, 1));
        }
        testTexture2.Apply();
        byte[] bytes5 = testTexture2.EncodeToJPG();
        string filename5 = Application.dataPath + "/testTexture2.jpg";
        System.IO.File.WriteAllBytes(filename5, bytes5);
        for (int i = 0; i < storeRGB.Count; i++)
        {
            print(storeRGB[i]);
            //print(storeRGB[i].x+"-"+ storeRGB[i].y+"-"+ storeRGB[i].z);
        }
    }
    //判断是不是这个范围
    private bool QuZhiFanWei(Vector3 a,Vector3 b)
    {
        /*for (int i = 0; i < storeRGB.Count; i++)
        {
            if (storeRGB[i].x == b.x && storeRGB[i].y == b.y && storeRGB[i].z == b.z)
            {
                return false;
            }
        }*/
        /* for (int i = 0; i < storeRGB.Count; i++)
         {

             if (storeRGB[i].x - 0.2 < b.x || storeRGB[i].x + 0.2 > b.x)
             {
                 if (storeRGB[i].y - 0.2 < b.y || storeRGB[i].y + 0.2 > b.y)
                 {
                     if (storeRGB[i].z - 0.2 < b.z || storeRGB[i].z + 0.2 > b.z)
                     {
                         return true;
                     }
                 }
             }
         }*/

        /*if (a.x+a.y+a.z<b.x+b.y+b.z-0.3f|| a.x + a.y + a.z > b.x + b.y + b.z + 0.3f)
        {
            return true;
        }*/
        for (int i = 0; i < storeRGB.Count; i++)
        {
            if (storeRGB[i].x >= b.x-0.2f && storeRGB[i].x <= b.x+0.2)
            {
                if (storeRGB[i].y > b.y - 0.2f && storeRGB[i].y < b.y + 0.2)
                {
                    if (storeRGB[i].z > b.z - 0.2f && storeRGB[i].z < b.z + 0.2)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    } 

    private void CompareJieTu()
    {
        for (int i = 0; i < comparePoint.Count; i++)
        {
            if (i == 0)
            {
                storeRGB.Add(new Vector3(JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).r, JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).g, JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).b));
                //if(JieTu.GetPixel(comparePoint[i].x,comparePoint[i].y))
            }     
            else
            {
                if (!storeRGB.Contains(new Vector3(JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).r, JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).g, JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).b)))
                {
                    storeRGB.Add(new Vector3(JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).r, JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).g, JieTu.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).b));
                }
            }
        }
        print(storeRGB.Count);
    }

    private void tuSeOperate()
    {
    
    }
    private void wuTuSeOperate()
    {

    }

    private void CutPic(Texture2D src, Texture2D dst)
    {
        int w0 = src.width;
        int h0 = src.height;
        int w1 = dst.width;
        int h1 = dst.height;
        float fw = (float)w0 / w1;
        float fh = (float)h0 / h1;
        int x0, y0;
        for (int y = 0; y < h1; y++)
        {
            y0 = (int)(y * fh);
            for (int x = 0; x < w1; x++)
            {
                x0 = (int)(x * fw);
                dst.SetPixel(x, y, src.GetPixel(x0, y0));
            }
        }
        dst.Apply();
    }
}
