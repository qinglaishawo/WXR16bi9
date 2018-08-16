using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GradeSystem : MonoBehaviour {
    //waixingren01
    public Texture2D waixingren01TuSe;
    public Texture2D waixingren01WuTuSe;
    public static Texture2D waixingren01TuSeAdjust;
    public static Texture2D waixingren01WuTuSeAdjust;
    public static List<Vector2> waixingren01TuSeArea = new List<Vector2>();
    //public static List<Vector3> biHuRGB = new List<Vector3>();
    //waixingren02
    public Texture2D waixingren02TuSe;
    public Texture2D waixingren02WuTuSe;
    public static Texture2D waixingren02TuSeAdjust;
    public static Texture2D waixingren02WuTuSeAdjust;
    public static List<Vector2> waixingren02TuSeArea = new List<Vector2>();
    //public static List<Vector3> ciWeiRGB = new List<Vector3>();
    //waixingren04
    public Texture2D waixingren04TuSe;
    public Texture2D waixingren04WuTuSe;
    public static Texture2D waixingren04TuSeAdjust;
    public static Texture2D waixingren04WuTuSeAdjust;
    public static List<Vector2> waixingren04TuSeArea = new List<Vector2>();
    //public static List<Vector3> daXiangRGB = new List<Vector3>();
    //waixingren05
    public Texture2D waixingren05TuSe;
    public Texture2D waixingren05WuTuSe;
    public static Texture2D waixingren05TuSeAdjust;
    public static Texture2D waixingren05WuTuSeAdjust;
    public static List<Vector2> waixingren05TuSeArea = new List<Vector2>();
    //public static List<Vector3> daJiaoShouRGB = new List<Vector3>();
    //waixingren06
    public Texture2D waixingren06TuSe;
    public Texture2D waixingren06WuTuSe;
    public static Texture2D waixingren06TuSeAdjust;
    public static Texture2D waixingren06WuTuSeAdjust;
    public static List<Vector2> waixingren06TuSeArea = new List<Vector2>();
    //public static List<Vector3> houZiRGB = new List<Vector3>();
    //waixingren07
    public Texture2D waixingren07TuSe;
    public Texture2D waixingren07WuTuSe;
    public static Texture2D waixingren07TuSeAdjust;
    public static Texture2D waixingren07WuTuSeAdjust;
    public static List<Vector2> waixingren07TuSeArea = new List<Vector2>();
    //public static List<Vector3> huLiRGB = new List<Vector3>();
    //waixingren08
    public Texture2D waixingren08TuSe;
    public Texture2D waixingren08WuTuSe;
    public static Texture2D waixingren08TuSeAdjust;
    public static Texture2D waixingren08WuTuSeAdjust;
    public static List<Vector2> waixingren08TuSeArea = new List<Vector2>();
    //public static List<Vector3> huoLieNiaoRGB = new List<Vector3>();
    //waixingren09
    public Texture2D waixingren09TuSe;
    public Texture2D waixingren09WuTuSe;
    public static Texture2D waixingren09TuSeAdjust;
    public static Texture2D waixingren09WuTuSeAdjust;
    public static List<Vector2> waixingren09TuSeArea = new List<Vector2>();
    //public static List<Vector3> laoHuRGB = new List<Vector3>();
    //waixingren10
    public Texture2D waixingren10TuSe;
    public Texture2D waixingren10WuTuSe;
    public static Texture2D waixingren10TuSeAdjust;
    public static Texture2D waixingren10WuTuSeAdjust;
    public static List<Vector2> waixingren10TuSeArea = new List<Vector2>();
    //public static List<Vector3> maoTouYingRGB = new List<Vector3>();
    //waixingren11
    public Texture2D waixingren11TuSe;
    public Texture2D waixingren11WuTuSe;
    public static Texture2D waixingren11TuSeAdjust;
    public static Texture2D waixingren11WuTuSeSeAdjust;
    public static List<Vector2> waixingren11TuSeArea = new List<Vector2>();
    //public static List<Vector3> meiHuaLuRGB = new List<Vector3>();
    //waixingren12
    public Texture2D waixingren12TuSe;
    public Texture2D waixingren12WuTuSe;
    public static Texture2D waixingren12TuSeAdjust;
    public static Texture2D waixingren12WuTuSeAdjust;
    public static List<Vector2> waixingren12TuSeArea = new List<Vector2>();
    //public static List<Vector3> songShuRGB = new List<Vector3>();
    //熊
   /* public Texture2D xiongTuSe;
    public Texture2D xiongWuTuSe;
    public static Texture2D xiongTuSeAdjust;
    public static Texture2D xiongWuTuSeAdjust;
    public static List<Vector2> xiongTuSeArea = new List<Vector2>();*/
    //public static List<Vector3> xiongRGB = new List<Vector3>();
    //当前不同的RBG
    public static List<Vector3> storeRGB = new List<Vector3>();
    //宽高比
    private float ratio;
    //调整后的高
    private int adjustHeight = 50;
    //调整后的宽
    private int adjustWith;
    //大象没有涂色的像素块
    public static List<Vector3> daXiangError = new List<Vector3>() {new Vector3(0.6f,0.6f,0.6f), new Vector3(0.8f, 0.8f, 0.9f) ,
        new Vector3(0.4f, 0.5f, 0.5f),new Vector3(0.5f,0.6f,0.6f),new Vector3(0.6f,0.4f,0.3f),new Vector3(0.7f,0.8f,0.9f),new Vector3(0.4f,0.4f,0.4f)};
    //当前调整的截图
    public Texture2D jieTuTest;
    private Texture2D jieTuAdjust;
    private Texture2D testTexture;
    //当前分数十位
    public int decade;
    //当前分数各位
    public int theUnit;
    //当前颜色的个数
    public int colorCount;
    // Use this for initialization
    void Start () {
        ratio = 1.637f;
        adjustWith=(int)(ratio * adjustHeight);
        CutPics();
        /*testTexture = new Texture2D(adjustWith, adjustHeight);
        for (int i = 0; i < daXiangTuSeArea.Count; i++)
        {
            testTexture.SetPixel((int)daXiangTuSeArea[i].x, (int)daXiangTuSeArea[i].y,Color.black);
        }
        testTexture.Apply();
        byte[] bytes = testTexture.EncodeToJPG();
        string filename= Application.dataPath + "/testTexture5.jpg";
        System.IO.File.WriteAllBytes(filename, bytes);*/
  
        //CompareColorCount(daXiangTuSeArea,jieTuTest,daXiangRGB);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //比较出涂色区域
   /* private void CompareTuSeArea(Texture2D TuSe,Texture2D WuTuSe)
    {
        for (int i = 0; i < adjustHeight; i++)
        {
            for (int z = 0; z < adjustWith; z++)
            {
                if (TuSe.GetPixel(z, i) != WuTuSe.GetPixel(z, i))
                {
                    if (Mathf.RoundToInt(WuTuSe.GetPixel(z, i).r * 255f) == 255 && Mathf.RoundToInt(WuTuSe.GetPixel(z, i).g * 255f) == 255 && Mathf.RoundToInt(WuTuSe.GetPixel(z, i).b * 255f) == 255)
                    {
                        if (TuSe.name == "biHuTuSe")
                        {
                            biHuTuSeArea.Add(new Vector2(z, i));
                        }
                        if (TuSe.name == "ciWeiTuSe")
                        {

                        }
                        if (TuSe.name == "DXTuSe")
                        {
                            daXiangTuSeArea.Add(new Vector2(z, i));
                        }
                        if (TuSe.name == "duJiaoShouTuSe")
                        {

                        }
                        if (TuSe.name == "houZiTuSe")
                        {

                        }
                        if (TuSe.name == "huLiTuSe")
                        {

                        }
                        if (TuSe.name == "huoLieNiaoTuSe")
                        {

                        }
                        if (TuSe.name == "laoHuTuSe")
                        {

                        }
                        if (TuSe.name == "maoTouYingTuSe")
                        {

                        }
                        if (TuSe.name == "meiHuaLuTuSe")
                        {

                        }
                        if (TuSe.name == "songShuTuSe")
                        {

                        }
                        if (TuSe.name == "xiongTuSe")
                        {

                        }
                    }
                }
            }
        }
    }*/

    //比较出有多少的颜色块
    public  void CompareColorCount(List<Vector2> comparePoint,Texture2D jieTu, List<Vector3> storeRGB,List<Vector3> Error)
    {
        jieTuAdjust = new Texture2D(adjustWith, adjustHeight);
        CutPic(jieTu, jieTuAdjust);
        for (int i = 0; i < comparePoint.Count; i++)
        {
            float r = jieTuAdjust.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).r;
            int ir = (int)(r * 10);
            r = ir / 10f;

            float g = jieTuAdjust.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).g;
            int ig = (int)(g * 10);
            g = ig / 10f;
            float b = jieTuAdjust.GetPixel((int)comparePoint[i].x, (int)comparePoint[i].y).b;
            int ib = (int)(b * 10);
            b = ib / 10f;
            if (i == 0)
            {
                //if (!Error.Contains(new Vector3(r, g, b)))
                {
                    storeRGB.Add(new Vector3(r, g, b));
                    print(storeRGB[i]);
                }          
            }
            else
            {
                if (!storeRGB.Contains(new Vector3(r, g, b)))
                {
                    if (QuZhiFanWei(new Vector3(r, g, b), storeRGB))
                    {
                        if (!Error.Contains(new Vector3(r, g, b)))
                        {
                            storeRGB.Add(new Vector3(r, g, b));
                            print(storeRGB[storeRGB.Count - 1]);
                        }
                    }
                }
            }
        }
        colorCount = storeRGB.Count;
        print(storeRGB.Count);
    }
    //提高同意颜色采取的范围
    private bool QuZhiFanWei( Vector3 b, List<Vector3> storeRGB)
    {     
        for (int i = 0; i < storeRGB.Count; i++)
        {
            if (storeRGB[i].x >= b.x - 0.2f && storeRGB[i].x <= b.x + 0.2)
            {
                if (storeRGB[i].y >= b.y - 0.2f && storeRGB[i].y <= b.y + 0.2)
                {
                    if (storeRGB[i].z >= b.z - 0.2f && storeRGB[i].z <= b.z + 0.2)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    //切图
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
    //初始化
    private void CutPics()
    {
        //waixingren01
        waixingren01TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren01WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren01TuSe, waixingren01TuSeAdjust);
        CutPic(waixingren01WuTuSe, waixingren01WuTuSeAdjust);
        CompareArea(waixingren01TuSeAdjust, waixingren01WuTuSeAdjust, waixingren01TuSeArea);
        //waixingren02
        waixingren02TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren02WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren02TuSe, waixingren02TuSeAdjust);
        CutPic(waixingren02WuTuSe, waixingren02WuTuSeAdjust);
        CompareArea(waixingren02TuSeAdjust, waixingren02WuTuSeAdjust, waixingren02TuSeArea);
        //waixingren04
        waixingren04TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren04WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren04TuSe, waixingren04TuSeAdjust);
        CutPic(waixingren04WuTuSe, waixingren04WuTuSeAdjust);
        CompareArea(waixingren04TuSeAdjust, waixingren04WuTuSeAdjust, waixingren04TuSeArea);
        //waixingren05
        waixingren05TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren05WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren05TuSe, waixingren05TuSeAdjust);
        CutPic(waixingren05WuTuSe, waixingren05WuTuSeAdjust);
        CompareArea(waixingren05TuSeAdjust, waixingren05WuTuSeAdjust, waixingren05TuSeArea);
        //waixingren06
        waixingren06TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren06WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren06TuSe, waixingren06TuSeAdjust);
        CutPic(waixingren06WuTuSe, waixingren06WuTuSeAdjust);
        CompareArea(waixingren06TuSeAdjust, waixingren06WuTuSeAdjust, waixingren06TuSeArea);
        //waixingren07
        waixingren07TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren07WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren07TuSe, waixingren07TuSeAdjust);
        CutPic(waixingren07WuTuSe, waixingren07WuTuSeAdjust);
        CompareArea(waixingren07TuSeAdjust, waixingren07WuTuSeAdjust, waixingren07TuSeArea);
        //waixingren08
        waixingren08TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren08WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren08TuSe, waixingren08TuSeAdjust);
        CutPic(waixingren08WuTuSe, waixingren08WuTuSeAdjust);
        CompareArea(waixingren08TuSeAdjust, waixingren08WuTuSeAdjust, waixingren08TuSeArea);
        //waixingren09
        waixingren09TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren09WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren09TuSe, waixingren09TuSeAdjust);
        CutPic(waixingren09WuTuSe, waixingren09WuTuSeAdjust);
        CompareArea(waixingren09TuSeAdjust, waixingren09WuTuSeAdjust, waixingren09TuSeArea);
        //waixingren10
        waixingren10TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren10WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren10TuSe, waixingren10TuSeAdjust);
        CutPic(waixingren10WuTuSe, waixingren10WuTuSeAdjust);
        CompareArea(waixingren10TuSeAdjust, waixingren10WuTuSeAdjust, waixingren10TuSeArea);
        //waixingren11
        waixingren11TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren11WuTuSeSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren11TuSe, waixingren11TuSeAdjust);
        CutPic(waixingren11WuTuSe, waixingren11WuTuSeSeAdjust);
        CompareArea(waixingren11TuSeAdjust, waixingren11WuTuSeSeAdjust, waixingren11TuSeArea);
        //waixingren12
        waixingren12TuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        waixingren12WuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(waixingren12TuSe, waixingren12TuSeAdjust);
        CutPic(waixingren12WuTuSe, waixingren12WuTuSeAdjust);
        CompareArea(waixingren12TuSeAdjust, waixingren12WuTuSeAdjust, waixingren12TuSeArea);
        //熊
        /*xiongTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        xiongWuTuSeAdjust = new Texture2D((int)(ratio * adjustHeight), adjustHeight);
        CutPic(xiongTuSe, xiongTuSeAdjust);
        CutPic(xiongWuTuSe, xiongWuTuSeAdjust);
        CompareArea(xiongTuSeAdjust, xiongWuTuSeAdjust, xiongTuSeArea);*/
        ClearCutPics();
    }

    //比较出涂色区域
    private void CompareArea(Texture2D tuSeAdjust,Texture2D wuTuSeAdjust,List<Vector2> compareArea)
    {
        for (int i = 0; i < tuSeAdjust.height; i++)
        {
            for (int z = 0; z < tuSeAdjust.width; z++)
            {
                if (tuSeAdjust.GetPixel(z, i) != wuTuSeAdjust.GetPixel(z, i))
                {
                    //if (Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).r * 255f) ==0 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).g * 255f) == 0 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).b * 255f) == 0)
                    if (Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).r * 255f) == 255 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).g * 255f) == 255 && Mathf.RoundToInt(wuTuSeAdjust.GetPixel(z, i).b * 255f) == 255)
                    {
                        compareArea.Add(new Vector2(z, i));
                    }
                }
            }
        }
    }
    //评分标准
    public void GradeStandard(int colorCount)
    {
        if (colorCount >= 16)
        {
            decade = 9;
            theUnit= UnityEngine.Random.Range(5,9);
        }
        if (colorCount < 16 && colorCount >= 14)
        {
            decade = 9;
            theUnit = UnityEngine.Random.Range(0,4);
        }
        if (colorCount < 14 && colorCount >= 12)
        {
            decade = 8;
            theUnit = UnityEngine.Random.Range(0,9);
        }
        if (colorCount < 12 && colorCount >= 8)
        {
            decade = 7;
            theUnit = UnityEngine.Random.Range(0, 9);
        }
        if (colorCount < 9 && colorCount >= 7)
        {
            decade = 6;
            theUnit = UnityEngine.Random.Range(0, 9);
        }
        if (colorCount < 7&& colorCount >= 6)
        {
            if (colorCount == 6)
            {
                decade = 5;
            }
            //decade = UnityEngine.Random.Range(3,5);
            theUnit = UnityEngine.Random.Range(0,9);
        }
        if (colorCount < 6 && colorCount >= 4)
        {
            decade = 1;
            //decade = UnityEngine.Random.Range(1, 2);
            theUnit = UnityEngine.Random.Range(1, 9);
        }
        if (colorCount <=3)
        {
            decade = 0;
            theUnit = 0;
        }
    }

    public void ClearCutPics()
    {
        //壁虎
        waixingren01TuSe = null;
        waixingren01WuTuSe = null;
        waixingren01TuSeAdjust = null;
        waixingren01WuTuSeAdjust = null;
        //刺猬
        waixingren02TuSe = null;
        waixingren02WuTuSe = null;
        waixingren02TuSeAdjust = null;
        waixingren02WuTuSeAdjust = null;
        //大象
        waixingren04TuSe = null;
        waixingren04WuTuSe = null;
        waixingren04TuSeAdjust = null;
        waixingren04WuTuSeAdjust = null;
        //独角兽
        waixingren05TuSe = null;
        waixingren05WuTuSe = null;
        waixingren05TuSeAdjust = null;
        waixingren05WuTuSeAdjust = null;
        //猴子
        waixingren06TuSe = null;
        waixingren06WuTuSe = null;
        waixingren06TuSeAdjust = null;
        waixingren06WuTuSeAdjust = null;
        //狐狸
        waixingren07TuSe = null;
        waixingren07WuTuSe = null;
        waixingren07TuSeAdjust = null;
        waixingren07WuTuSeAdjust = null;
        //火烈鸟
        waixingren08TuSe = null;
        waixingren08WuTuSe = null;
        waixingren08TuSeAdjust = null;
        waixingren08WuTuSeAdjust = null;
        //老虎
        waixingren09TuSe = null;
        waixingren09WuTuSe = null;
        waixingren09TuSeAdjust = null;
        waixingren09WuTuSeAdjust = null;
        //猫头鹰
        waixingren10TuSe = null;
        waixingren10WuTuSe = null;
        waixingren10TuSeAdjust = null;
        waixingren10WuTuSeAdjust = null;
        //梅花鹿
        waixingren11TuSe = null;
        waixingren11WuTuSe = null;
        waixingren11TuSeAdjust = null;
        waixingren11WuTuSeSeAdjust = null;
        //松鼠
        waixingren12TuSe = null;
        waixingren12WuTuSe = null;
        waixingren12TuSeAdjust = null;
        waixingren12WuTuSeAdjust = null;
        //熊
       /* xiongTuSe = null;
        xiongWuTuSe = null;
        xiongTuSeAdjust = null;
        xiongWuTuSeAdjust = null;*/
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    public void Reset()
    {
        decade = 0;
        theUnit = 0;
        colorCount = 0;
        storeRGB.Clear();
        jieTuAdjust = null;
    }
}
