using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EasyAR;
using System.IO;
using System;
using UnityEngine.UI;

public class AddPic : MonoBehaviour
{
    private Manager manager;
    private ShowGrade showGrade;
    private PeopleFace peopleFace;
    public  GameObject curAnimalObj;
    public static int curAnimalIndex;
    public static string curAnimalName;
    private CreateFolders createFolder;

    public List<GameObject> waixingren01 = new List<GameObject>();
    public static int waixingren01Index;
    public List<GameObject> waixingren02 = new List<GameObject>();
    public static int waixingren02Index;
    public List<GameObject> waixingren03 = new List<GameObject>();
    public static int waixingren03Index;
    public List<GameObject> waixingren04 = new List<GameObject>();
    public static int waixingren04Index;
    public List<GameObject> waixingren05 = new List<GameObject>();
    public static int waixingren05Index;
    public List<GameObject> waixingren06 = new List<GameObject>();
    public static int waixingren06Index;
    public List<GameObject> waixingren07 = new List<GameObject>();
    public static int waixingren07Index;
    public List<GameObject> waixingren08 = new List<GameObject>();
    public static int waixingren08Index;
    public List<GameObject> waixingren09 = new List<GameObject>();
    public static int waixingren09Index;
    public List<GameObject> waixingren10 = new List<GameObject>();
    public static int waixingren10Index;
    public List<GameObject> waixingren11 = new List<GameObject>();
    public static int waixingren11Index;
    public List<GameObject> waixingren12 = new List<GameObject>();
    public static int waixingren12Index;
   
    public GameObject EWMPrompt;

    public static string curEWMString = null;
    public static bool isPromptEWM=false;
    public static bool isEWMDisappear=false;
    private float timeWERPrompt = 0;

    public string curSignName;
    private Texture2D bigMap;

    private byte[] byteAnimal;
    private byte[] byteName;
	public static bool isFirst=true;
    public static bool isFirst2 = false;
    public GameObject PlaneFirst;
    public GameObject realityPlane;
    public Camera mainCamera;
    public GameObject tanKuang;

    public Dictionary<string, string> match = new Dictionary<string, string>();
    // Use this for initialization
    void Start()
    {
        tanKuang.SetActive(false);
        manager = this.GetComponent<Manager>();
        showGrade = this.GetComponent<ShowGrade>();
        peopleFace = this.GetComponent<PeopleFace>();
        createFolder = this.GetComponent<CreateFolders>();
        if (Manager.isEWM)
        {
            foreach (var behaviour in ARBuilder.Instance.AugmenterBehaviours)
            {
                behaviour.TextMessage += OnTextMessage;
            }
        }
        StartCoroutine(Init());
        //ValidateManager.QueryProductSever("TTX01DSZ4KexDWDgkvoxozj1H", "TTXS", "1.0", checkResult);
        AddMatch();
    }

    void AddMatch()
    {
        match.Add("37", "waixingren04");
        match.Add("38", "waixingren07");
        match.Add("39", "waixingren02");
        match.Add("40", "waixingren08");
        match.Add("41", "waixingren03");
        match.Add("42", "waixingren09");
        match.Add("43", "waixingren10");
        match.Add("44", "waixingren11");
        match.Add("45", "waixingren12");
        match.Add("46", "waixingren06");
        match.Add("47", "waixingren01");
        match.Add("48", "waixingren05");
    }

    IEnumerator Init()
    {
        //waixingren01
        for (int i = 0; i < waixingren01.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren01[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren01[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren01[i].SetActive(true);
                        waixingren01Index = i + 1;                    
                    }
                }
                if (waixingren01Index == waixingren01.Count)
                {
                    waixingren01Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren01[i].transform.childCount > 2)
                    {
                        waixingren01[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren01[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren01[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren01[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren01[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren01[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren01[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren01[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren02
        for (int i = 0; i < waixingren02.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren02[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren02[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren02[i].SetActive(true);
                        waixingren02Index = i + 1;
                    }
                }
                if (waixingren02Index == waixingren02.Count)
                {
                    waixingren02Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren02[i].transform.childCount > 2)
                    {
                        waixingren02[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren02[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren02[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren02[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren02[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren02[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren02[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren02[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }

        //waixingren03
        for (int i = 0; i < waixingren03.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren03[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren03[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren03[i].SetActive(true);
                        waixingren03Index = i + 1;
                    }
                }
                if (waixingren03Index == waixingren03.Count)
                {
                    waixingren03Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren03[i].transform.childCount > 2)
                    {
                        waixingren03[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren03[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren03[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren03[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren03[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren03[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren03[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren03[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren04
        for (int i = 0; i < waixingren04.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren04[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren04[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren04[i].SetActive(true);
                        waixingren04Index = i + 1;
                    }
                }
                if (waixingren04Index == waixingren04.Count)
                {
                    waixingren04Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren04[i].transform.childCount > 2)
                    {
                        waixingren04[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren04[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren04[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren04[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren04[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren04[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren04[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren04[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren05
        for (int i = 0; i < waixingren05.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren05[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren05[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren05[i].SetActive(true);
                        waixingren05Index = i + 1;
                    }
                }
                if (waixingren05Index == waixingren05.Count)
                {
                    waixingren05Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren05[i].transform.childCount > 2)
                    {
                        waixingren05[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren05[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren05[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren05[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren05[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren05[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren05[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren05[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren06
        for (int i = 0; i < waixingren06.Count; i++)
        {
            //print(Application.dataPath + "/MapDepot" + "/" + houzi[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren06[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren06[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren06[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren06[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture,false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren06[i].SetActive(true);
                        waixingren06Index = i + 1;
                    }
                }
                if (waixingren06Index == waixingren06.Count)
                {
                    waixingren06Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren06[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren06[i].transform.childCount > 2)
                    {
                        waixingren06[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren06[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren06[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren06[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren06[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren06[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren06[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren06[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren06[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren06[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren07
        for (int i = 0; i < waixingren07.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren07[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren07[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren07[i].SetActive(true);
                        waixingren07Index = i + 1;
                    }
                }
                if (waixingren07Index == waixingren07.Count)
                {
                    waixingren07Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren07[i].transform.childCount > 2)
                    {
                        waixingren07[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren07[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren07[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren07[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren07[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren07[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren07[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren07[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren08
        for (int i = 0; i < waixingren08.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren08[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren08[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren08[i].SetActive(true);
                        waixingren08Index = i + 1;
                    }
                }
                if (waixingren08Index == waixingren08.Count)
                {
                    waixingren08Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren08[i].transform.childCount > 2)
                    {
                        waixingren08[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren08[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren08[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren08[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren08[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren08[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren08[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren08[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren09
        for (int i = 0; i < waixingren09.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren09[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren09[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren09[i].SetActive(true);
                        waixingren09Index = i + 1;
                    }
                }
                if (waixingren09Index == waixingren09.Count)
                {
                    waixingren09Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren09[i].transform.childCount > 2)
                    {
                        waixingren09[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren09[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren09[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren09[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren09[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren09[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren09[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren09[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren10
        for (int i = 0; i < waixingren10.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren10[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren10[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren10[i].SetActive(true);
                        waixingren10Index = i + 1;
                    }
                }
                if (waixingren10Index == waixingren10.Count)
                {
                    waixingren10Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren10[i].transform.childCount > 2)
                    {
                        waixingren10[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren10[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren10[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren10[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren10[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren10[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren10[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren10[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren11
        for (int i = 0; i < waixingren11.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren11[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren11[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren11[i].SetActive(true);
                        waixingren11Index = i + 1;
                    }
                }
                if (waixingren11Index == waixingren11.Count)
                {
                    waixingren11Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren11[i].transform.childCount > 2)
                    {
                        waixingren11[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren11[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren11[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren11[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren11[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren11[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren11[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren11[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        //waixingren12
        for (int i = 0; i < waixingren12.Count; i++)
        {
            print(Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg");
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg"))
            {

                Renderer[] rendererComponents = waixingren12[i].GetComponentsInChildren<Renderer>();
                foreach (Renderer component in rendererComponents)
                {
                    if (component.material.name.Contains(waixingren12[i].transform.GetChild(0).name))
                    {
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/animalMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(component.material.mainTexture, false);
                        component.material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren12[i].SetActive(true);
                        waixingren12Index = i + 1;
                    }
                }
                if (waixingren12Index == waixingren12.Count)
                {
                    waixingren12Index = 0;
                }
            }
            /*if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg"))
            {
                if (Manager.isEWM)
                {
                    if (waixingren12[i].transform.childCount > 2)
                    {
                        waixingren12[i].transform.GetChild(2).gameObject.SetActive(true);
                        string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/nameMap.jpg";
                        WWW www = new WWW(filePath);
                        yield return www;
                        Texture2D tex = www.texture;
                        DestroyImmediate(waixingren12[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture, false);
                        waixingren12[i].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = tex;
                        tex = null;
                        www = null;
                        waixingren12[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }*/
            if (File.Exists(Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg"))
            {
                string filePath = "file://" + Application.dataPath + "/MapDepot" + "/" + waixingren12[i].transform.GetChild(0).name + "/" + i.ToString() + "/faceMap.jpg";
                WWW www = new WWW(filePath);
                yield return www;
                Texture2D tex = www.texture;
                DestroyImmediate(waixingren12[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture, false);
                waixingren12[i].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = tex;
                tex = null;
                www = null;
                waixingren12[i].transform.GetChild(3).gameObject.SetActive(true);
            }
        }
       
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.isEWM)
        {
            if (isPromptEWM == false)
            {
                if (curEWMString != null)
                {
                    isPromptEWM = true;
                    EWMPrompt.SetActive(true);
                }
            }
            if (isEWMDisappear == false)
            {
                if (isPromptEWM)
                {
                    timeWERPrompt = timeWERPrompt + Time.deltaTime;
                    if (timeWERPrompt >= 3)
                    {
                        isEWMDisappear = true;
                        timeWERPrompt = 0;
                        EWMPrompt.SetActive(false);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.collider.name == "cancel")
                {
                    PlaneFirst.SetActive(false);
                    isFirst2 = false;
                    curEWMString = null;
                    isPromptEWM = false;
                    isEWMDisappear = false;
                    AddPic.isFirst = true;
                    timeWERPrompt = 0;
                    EWMPrompt.SetActive(false);
                }
            }
        }
        PlaneFirst.GetComponent<MeshRenderer>().material.mainTexture = realityPlane.GetComponent<MeshRenderer>().material.mainTexture;
    }


    public void AddPics(string gameObjectName)
    {
        if (gameObjectName == "waixingren01")
        {
            tempAddPic(waixingren01, waixingren01Index, gameObjectName);
            if (waixingren01Index < waixingren01.Count)
            {
                waixingren01Index++;
            }
            if (waixingren01Index == waixingren01.Count)
            {
                waixingren01Index = 0;
            }
        }
        if (gameObjectName == "waixingren02")
        {
            tempAddPic(waixingren02, waixingren02Index, gameObjectName);
            if (waixingren02Index < waixingren02.Count)
            {
                waixingren02Index++;
            }
            if (waixingren02Index == waixingren02.Count)
            {
                waixingren02Index = 0;
            }
        }
        if (gameObjectName == "waixingren03")
        {
            tempAddPic(waixingren03, waixingren03Index, gameObjectName);
            if (waixingren03Index < waixingren03.Count)
            {
                waixingren03Index++;
            }
            if (waixingren03Index == waixingren03.Count)
            {
                waixingren03Index = 0;
            }
        }
        if (gameObjectName == "waixingren04")
        {
            tempAddPic(waixingren04, waixingren04Index, gameObjectName);
            if (waixingren04Index < waixingren04.Count)
            {
                waixingren04Index++;
            }
            if (waixingren04Index == waixingren04.Count)
            {
                waixingren04Index = 0;
            }
        }
        if (gameObjectName == "waixingren05")
        {
            tempAddPic(waixingren05, waixingren05Index, gameObjectName);
            if (waixingren05Index < waixingren05.Count)
            {
                waixingren05Index++;
            }
            if (waixingren05Index == waixingren05.Count)
            {
                waixingren05Index = 0;
            }
        }
        if (gameObjectName == "waixingren06")
        {
            tempAddPic(waixingren06, waixingren06Index, gameObjectName);
            if (waixingren06Index < waixingren06.Count)
            {
                waixingren06Index++;
            }
            if (waixingren06Index == waixingren06.Count)
            {
                waixingren06Index = 0;
            }
        }
        if (gameObjectName == "waixingren07")
        {
            tempAddPic(waixingren07, waixingren07Index, gameObjectName);
            if (waixingren07Index < waixingren07.Count)
            {
                waixingren07Index++;
            }
            if (waixingren07Index == waixingren07.Count)
            {
                waixingren07Index = 0;
            }
        }
        if (gameObjectName == "waixingren08")
        {
            tempAddPic(waixingren08, waixingren08Index, gameObjectName);
            if (waixingren08Index < waixingren08.Count)
            {
                waixingren08Index++;
            }
            if (waixingren08Index == waixingren08.Count)
            {
                waixingren08Index = 0;
            }
        }
        if (gameObjectName == "waixingren09")
        {
            tempAddPic(waixingren09, waixingren09Index, gameObjectName);
            if (waixingren09Index < waixingren09.Count)
            {
                waixingren09Index++;
            }
            if (waixingren09Index == waixingren09.Count)
            {
                waixingren09Index = 0;
            }
        }
        if (gameObjectName == "waixingren10")
        {
            tempAddPic(waixingren10, waixingren10Index, gameObjectName);
            if (waixingren10Index < waixingren10.Count)
            {
                waixingren10Index++;
            }
            if (waixingren10Index == waixingren10.Count)
            {
                waixingren10Index = 0;
            }
        }
        if (gameObjectName == "waixingren11")
        {
            tempAddPic(waixingren11, waixingren11Index, gameObjectName);
            if (waixingren11Index < waixingren11.Count)
            {
                waixingren11Index++;
            }
            if (waixingren11Index == waixingren11.Count)
            {
                waixingren11Index = 0;
            }
        }
        if (gameObjectName == "waixingren12")
        {
            tempAddPic(waixingren12, waixingren12Index, gameObjectName);
            if (waixingren12Index < waixingren12.Count)
            {
                waixingren12Index++;
            }
            if (waixingren12Index == waixingren12.Count)
            {
                waixingren12Index = 0;
            }
        }
        /*if (gameObjectName == "songshu")
        {
            tempAddPic(songshu, songshuIndex, gameObjectName);
            if (songshuIndex < songshu.Count)
            {
                songshuIndex++;
            }
            if (songshuIndex == songshu.Count)
            {
                songshuIndex = 0;
            }
        }*/
    }

    private void tempAddPic(List<GameObject> gameObjectList, int index, string name)
    {
        curAnimalObj = gameObjectList[index];
        curAnimalIndex = index;
        curAnimalName = name;
        //gameObjectList[index].SetActive(false);
        gameObjectList[index].transform.GetChild(1).gameObject.SetActive(false);
        gameObjectList[index].transform.GetChild(1).gameObject.SetActive(true);
        gameObjectList[index].SetActive(true);
        Renderer[] rendererComponents = gameObjectList[index].GetComponentsInChildren<Renderer>();
        foreach (Renderer component in rendererComponents)
        {
            if (component.material.name.Contains(name))
            {
                /*if (component.material.mainTexture != null)
                {
                    Destroy(component.material.mainTexture);
                    Resources.UnloadAsset(component.material.mainTexture);
                    component.material.mainTexture = null;				
                }*/
                /*if (!Manager.isEWM)
                {                 
                    component.material.mainTexture = Manager.mainTexture;
                    byteAnimal = Manager.mainTexture.EncodeToJPG();
                    string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + name + "/" + curAnimalIndex.ToString() + "/" + "animalMap.jpg";
                    System.IO.File.WriteAllBytes(filename, byteAnimal);
                    //byteAnimal = null;
                    
                }

                else
                {
                    component.material.mainTexture = Manager.animalMap;
                    byteAnimal = Manager.animalMap.EncodeToJPG();
                    string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + name + "/" + curAnimalIndex.ToString() + "/" + "animalMap.jpg";
                    System.IO.File.WriteAllBytes(filename, byteAnimal);
                    //byteAnimal = null;                 
                }*/
                component.material.mainTexture = Manager.animalMap;
                byteAnimal = Manager.animalMap.EncodeToJPG();
                string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + name + "/" + curAnimalIndex.ToString() + "/" + "animalMap.jpg";
                System.IO.File.WriteAllBytes(filename, byteAnimal);
            }
        }
        //Manager.animalMap = null;
        //if (Manager.isEWM)
        //{
        //    if (gameObjectList[index].transform.childCount > 2)
        //    {
        //        gameObjectList[index].transform.GetChild(2).gameObject.SetActive(true);
        //        //gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = nameMap;
        //        /*if (gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture != null)
        //        {
        //            Destroy(gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture);
        //            //Resources.UnloadAsset(gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture);
        //            gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = null;
        //        }*/
        //        gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = Manager.nameMap;
        //        byteName  = Manager.nameMap.EncodeToJPG();
        //        string filename = Application.dataPath + "/" + createFolder.rootDirectory + "/" + AddPic.curAnimalName + "/" + AddPic.curAnimalIndex.ToString() + "/" + "nameMap.jpg";
        //        System.IO.File.WriteAllBytes(filename, byteName);
        //        byteName = null;
        //        //Manager.nameMap = null;
        //    }
        //}
        if (gameObjectList[index].transform.childCount > 3)
        {
            if (gameObjectList[index].transform.GetChild(3).gameObject.activeInHierarchy == true)
            {
                gameObjectList[index].transform.GetChild(3).gameObject.SetActive(false);
                gameObjectList[index].transform.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = null;
                //gameObjectList[index].transform.GetChild(2).GetComponent<MeshRenderer>().material.mainTexture = PeopleFace.faceMap;
            }
        }
    }

    public void StartRecognition()
    {
        if (isFirst)
        {
            PlaneFirst.SetActive(true);
            isFirst2 = true;
            if (manager._ImageTargetObj != null)
                manager.JudgeDotContain();
            //ValidateManager.QueryProductSever("TTX08TkHAbMcsHsCaRKxkdHoV", "TTXS", "1.0", checkResult);
            if (manager._ImageTargetObj != null && showGrade.isClick == false && peopleFace.isClosefront == true && manager.isDotContain == true && isFirst2 == true&&Manager.isStartGuaiWu==false)
            {
                /*foreach (var behaviour in ARBuilder.Instance.AugmenterBehaviours)
                {
                    behaviour.TextMessage -= OnTextMessage;
                }*/

                /*if (!string.IsNullOrEmpty(curEWMString))
                {
                    isFirst = false;
                    ValidateManager.QueryProductSever(curEWMString, "TTXS", "1.0", checkResult);
                }*/
                //Test.GetComponent<Text>().text = "11111111111111";
                ValidateResult r = new ValidateResult();
                r.state = ValidateResult.State.Success;
                checkResult(r);
            }
        }

    }

    private void checkResult(ValidateResult result)
    {

        switch (result.state)
        {
            case ValidateResult.State.None:
                break;
            case ValidateResult.State.Checking:
                break;
            case ValidateResult.State.ErrorRequestStream:
                curEWMString = null;
                isPromptEWM = false;
                isEWMDisappear = false;
                AddPic.isFirst = true;
                isFirst2 = false;
                EWMPrompt.SetActive(false);
                tanKuang.SetActive(true);
                tanKuang.transform.GetChild(1).GetComponent<Text>().text = result.ToString();
                break;
            case ValidateResult.State.ErrorBackContent:
                curEWMString = null;
                isPromptEWM = false;
                isEWMDisappear = false;
                AddPic.isFirst = true;
                isFirst2 = false;
                EWMPrompt.SetActive(false);
                tanKuang.SetActive(true);
                tanKuang.transform.GetChild(1).GetComponent<Text>().text = result.ToString();
                break;
            case ValidateResult.State.ErrorTimeOut:
                curEWMString = null;
                isPromptEWM = false;
                isEWMDisappear = false;
                AddPic.isFirst = true;
                isFirst2 = false;
                EWMPrompt.SetActive(false);
                tanKuang.SetActive(true);
                tanKuang.transform.GetChild(1).GetComponent<Text>().text = result.ToString();
                break;
            case ValidateResult.State.KeyError:
                curEWMString = null;
                isPromptEWM = false;
                isEWMDisappear = false;
                AddPic.isFirst = true;
                isFirst2 = false;
                EWMPrompt.SetActive(false);
                tanKuang.SetActive(true);
                tanKuang.transform.GetChild(1).GetComponent<Text>().text = result.ToString();
                break;
            case ValidateResult.State.KeyMaxCount:
                curEWMString = null;
                isPromptEWM = false;
                isEWMDisappear = false;
                AddPic.isFirst = true;
                isFirst2 = false;
                EWMPrompt.SetActive(false);
                tanKuang.SetActive(true);
                tanKuang.transform.GetChild(1).GetComponent<Text>().text = result.ToString();
                break;
            case ValidateResult.State.Success:
                //if (match[result.productCode.Substring(4, 2)] == GetComponent<Manager>()._ImageTargetObj.name)
                //{
                    CutBigMap();
                    manager.GetReviseTexture2();
                    AddPics(curSignName);
                    showGrade.ShowAnimal();
                    //MagnifyAnimal();
                    Manager.mainTexture = null;
                    Manager.mainTexture = new Texture2D(Manager.textureW, Manager.textureH, TextureFormat.RGB24, false, true);
                    curSignName = null;
                    curEWMString = null;
                    isFirst = false;
                    PlaneFirst.SetActive(false);
                    tanKuang.SetActive(true);
                    tanKuang.transform.GetChild(1).GetComponent<Text>().text = result.ToString();
                //}
                /*else
                {
                    curEWMString = null;
                    isPromptEWM = false;
                    isEWMDisappear = false;
                    AddPic.isFirst = true;
                    isFirst2 = false;
                    EWMPrompt.SetActive(false);
                    tanKuang.SetActive(true);
                    tanKuang.transform.GetChild(1).GetComponent<Text>().text = "当前二维码与识别图不匹配";
                }*/
                break;
            default:
                break;
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (var behaviour in ARBuilder.Instance.AugmenterBehaviours)
        {
            behaviour.TextMessage += OnTextMessage;
        }
    }

    public void OnTextMessage(AugmenterBaseBehaviour augmenterBehaviour, string text)
    {
       if(curEWMString==null&& isPromptEWM==false&&isFirst2==true)
        curEWMString = text;
        //print(text);
    }
    private void JudgeSignName()
    {

    }

    public void CutBigMap()
    {
        ImageTargetImageManageTTXPC.Instance.PicJoint();
        bigMap = ImageTargetImageManageTTXPC.VideoBackgroundTexure;
        /*byte[] bytes = bigMap.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(filename, bytes);*/
    }


    public void button()
    {
        tanKuang.SetActive(false);
    }
}
