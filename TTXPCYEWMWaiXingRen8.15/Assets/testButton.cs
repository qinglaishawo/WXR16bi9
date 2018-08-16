using UnityEngine;
using System.Collections;

public class testButton : MonoBehaviour {
    public Texture2D texture2D;
    private Texture2D animalMap;
    private Texture2D nameMap;
    private Rect rect;
    private Rect rect2;
    // Use this for initialization
    void Start () {
        rect = new Rect(new Rect(0, 0, 0.875f, 1));
        rect2 = new Rect(new Rect(0.88f, 0.245f, 0.11f, 0.74f));
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void CutAnimalMap()
    {
        int x = Mathf.FloorToInt(texture2D.width * rect.x);
        int y = Mathf.FloorToInt(texture2D.height * rect.y);
        int width = Mathf.FloorToInt(texture2D.width * rect.width);
        int height = Mathf.FloorToInt(texture2D.height * rect.height);
       // texture2D.ReadPixels(new Rect(0,0,1,1), 0, 0);
        //texture2D.Apply();
        Color[] pix = texture2D.GetPixels(x, y, width, height);
        animalMap = new Texture2D(width, height);
        animalMap.SetPixels(pix);
        animalMap.Apply();
        byte[] bytes = animalMap.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot3.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        CutNameMap();
    }

    public void CutNameMap()
    {
        int x = Mathf.FloorToInt(texture2D.width * rect2.x);
        int y = Mathf.FloorToInt(texture2D.height * rect2.y);
        int width = Mathf.FloorToInt(texture2D.width * rect2.width);
        int height = Mathf.FloorToInt(texture2D.height * rect2.height);
        Color[] pix = texture2D.GetPixels(x, y, width, height);
        nameMap = new Texture2D(width, height);
        nameMap.SetPixels(pix);
        nameMap.Apply();
         byte[] bytes = nameMap.EncodeToPNG();
         string filename = Application.dataPath + "/Screenshot4.png";
         System.IO.File.WriteAllBytes(filename, bytes);
    }
}
