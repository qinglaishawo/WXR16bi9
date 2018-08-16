using UnityEngine;
using System.Collections;
using EasyAR;

public class CutImage : MonoBehaviour {

    public static Texture2D bigMap;
    public static Texture2D animalMap;
    public static Texture2D nameMap;

    // Use this for initialization
    void Start () {
         
    }
	
	// Update is called once per frame
	void Update () {     
  
    }
    public static void CutBigMap()
    {
        ImageTargetImageManageTTXPC.Instance.PicJoint();
        bigMap = ImageTargetImageManageTTXPC.VideoBackgroundTexure;
    }

    public static void CutAnimalMap(Rect sourceRect)
    {
        int x = Mathf.FloorToInt(bigMap.width*sourceRect.x);
        int y = Mathf.FloorToInt(bigMap.height*sourceRect.y);
        int width = Mathf.FloorToInt(bigMap.width*sourceRect.width);
        int height = Mathf.FloorToInt(bigMap.height*sourceRect.height);
        Color[] pix = bigMap.GetPixels(x, y, width, height);
        animalMap = new Texture2D(width, height);
        animalMap.SetPixels(pix);
        animalMap.Apply();
    }

    public static void CutNameMap(Rect sourceRect)
    {
        int x = Mathf.FloorToInt(bigMap.width * sourceRect.x);
        int y = Mathf.FloorToInt(bigMap.height * sourceRect.y);
        int width = Mathf.FloorToInt(bigMap.width * sourceRect.width);
        int height = Mathf.FloorToInt(bigMap.height * sourceRect.height);
        Color[] pix = bigMap.GetPixels(x, y, width, height);
        nameMap = new Texture2D(width, height);
        nameMap.SetPixels(pix);
        nameMap.Apply();
    }

    public void button()
    {
        //CutAnimalMap(0,0,512,512);
        // CutAnimalMap2();
        CutBigMap();
        CutAnimalMap(new Rect(0.1f,0.1f,0.5f,0.5f));
        /* bigMap = (Texture2D)RealityPlane.GetComponent<MeshRenderer>().material.mainTexture;
         cube.GetComponent<MeshRenderer>().material.mainTexture = bigMap;
         bigMap.Apply();*/
         /*byte[] bytes = bigMap.EncodeToPNG();
         string filename = Application.dataPath + "/Screenshot2.png";
         System.IO.File.WriteAllBytes(filename, bytes);*/
    }
}
