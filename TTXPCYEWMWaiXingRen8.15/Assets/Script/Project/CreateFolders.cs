using UnityEngine;
using System.Collections;
using System.IO;

public class CreateFolders : MonoBehaviour {

    private AddPic addPic;
    public string rootDirectory= "MapDepot";
    private string[] animalName=new string[] { "waixingren01", "waixingren02", "waixingren03", "waixingren04", "waixingren05", "waixingren06", "waixingren07", "waixingren08", "waixingren09", "waixingren10", "waixingren11", "waixingren12"};
    private int[] animalCount = new int[] { 4,4,2,4,3,3,3,1,2,3,3,3}; 
    // Use this for initialization
    void Start () {
        CreateFolder();
        addPic=this.GetComponent<AddPic>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateFolder()
    {
        if (!Directory.Exists(Application.dataPath + "/" + rootDirectory))
        {
            Directory.CreateDirectory(Application.dataPath + "/" + rootDirectory);
        }

        for (int i = 0; i < animalName.Length; i++)
        {
            if (!Directory.Exists(Application.dataPath + "/" +rootDirectory+"/"+animalName[i]))
            {

                Directory.CreateDirectory(Application.dataPath + "/" + rootDirectory + "/" + animalName[i]);
            }
        }
        StorePic();
    }

    public void StorePic()
    {
        for (int i = 0; i < animalName.Length; i++)
        {
            for (int w = 0; w < animalCount[i]; w++)
            {
                if (!Directory.Exists(Application.dataPath + "/" + rootDirectory + "/" + animalName[i] + "/" + w))
                {

                    Directory.CreateDirectory(Application.dataPath + "/" + rootDirectory + "/" + animalName[i] + "/" + w);
                }
            }
        }
    }

    void  Init()
    {
        
    }
}
