using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowGrade : MonoBehaviour {
    public List<GameObject> animals;
    private Manager manager;
    private GameObject curAnimalObj;
    public GameObject FireWorks;
    private float _time;
    private float _time2;
    public bool isClick=false;
    private bool isClickTiQian=false;
    public GameObject QiTaObj;
    public GameObject seCaiObj;
    public GameObject chuangYiObj;
    public GameObject WenZiObj;
    public static bool ispeopleFace;
    public static bool isTiQian;
    // Use this for initialization
    void Start () {
        manager = this.GetComponent<Manager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isClick == true)
        {
            DalayTime(10);
            _time2 = _time2 + Time.deltaTime;
            if (!isClickTiQian)
            {
                if (_time2 > 9)
                {
                    _time2 = 0;
                    isTiQian = true;
                    isClickTiQian = true;
                }
            }
        }
	}

    public void ShowAnimal()
    {
        isClick = true;
        FireWorks.SetActive(true);
        QiTaObj.SetActive(true);
        curAnimalObj = ReturnAnimal();
        curAnimalObj.SetActive(true);
        
        Renderer[] rendererComponents = curAnimalObj.transform.GetChild(0).GetComponentsInChildren<Renderer>();
        foreach (Renderer component in rendererComponents)
        {
            if (component.material.name.Contains(curAnimalObj.name))
            {
                if (component.material.mainTexture != null)
                {
                    //Destroy(component.material.mainTexture);
                    component.material.mainTexture = null;
                }
                if (Manager.isEWM)
                {
                    component.material.mainTexture = Manager.animalMap;
                    //curAnimalObj.transform.GetChild(1).GetComponent<MeshRenderer>().material.mainTexture = Manager.nameMap;
                }
                else
                {
                    component.material.mainTexture = Manager.mainTexture;
                }
            }
        }
        
        SeCaiMethod(seCaiObj);
        ChuangYiMethod(chuangYiObj);
        WenZiMethod();
    }

    private GameObject ReturnAnimal()
    {
        print(manager._ImageTargetObj.name);
        int tempInt=0;
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i].name == manager._ImageTargetObj.name)
            {
                tempInt=i;
            }
        }
        return animals[tempInt];
    }

    private void DalayTime(float time)
    {
        _time = _time + Time.deltaTime;
        if (_time >= time)
        {
            curAnimalObj.SetActive(false);
            FireWorks.SetActive(false);
			HideObjects (seCaiObj.transform.GetChild(0));
			HideObjects (seCaiObj.transform.GetChild(1));
			HideObjects (chuangYiObj.transform.GetChild(0));
			HideObjects (chuangYiObj.transform.GetChild(1));
			HideObjects (WenZiObj.transform);
            QiTaObj.SetActive(false);
            isClick = false;
            _time = 0;
			manager._ImageTargetObj = null;
            ispeopleFace = true;
            _time2 = 0;
            isClickTiQian = false;
        }
    }

    //展现色彩分
    private void SeCaiMethod(GameObject obj)
    {
        SeCaiMethod2();
        GetComponent<GradeSystem>().GradeStandard(GetComponent<GradeSystem>().colorCount);
        print(GetComponent<GradeSystem>().decade+"+"+GetComponent<GradeSystem>().theUnit);
        if (GetComponent<GradeSystem>().decade != 0)
        {
            obj.transform.GetChild(0).GetChild(GetComponent<GradeSystem>().decade - 1).gameObject.SetActive(true);
            obj.transform.GetChild(1).GetChild(GetComponent<GradeSystem>().theUnit - 1).gameObject.SetActive(true);
        }
        else
        {
            obj.transform.GetChild(1).GetChild(10).gameObject.SetActive(true);
        }
        GetComponent<GradeSystem>().Reset();
    }

    private void ChuangYiMethod(GameObject obj)
    {
        //int tempNumber1 = Random.Range(0, 1);
        int tempNumber1 = 1;
        int tempNumber2 = Random.Range(0, 9);
        obj.transform.GetChild(0).GetChild(tempNumber1).gameObject.SetActive(true);
        obj.transform.GetChild(1).GetChild(tempNumber2).gameObject.SetActive(true);
    }

    private void WenZiMethod()
    {
        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        int tempNumber = Random.Range(0,2);
        WenZiObj.transform.GetChild(tempNumber).gameObject.SetActive(true);
    }

	void HideObjects(Transform trans)
	{
		for (int i = 0; i < trans.childCount; ++i) {
			trans.GetChild (i).gameObject.SetActive (false);
		}
		/*for (int i = 0; i < trans.childCount; ++i)
			HideObjects(trans.GetChild(i));
		//if (transform != trans)
			gameObject.SetActive(false);*/
	}

    void SeCaiMethod2()
    {
        if (curAnimalObj.name == "waixingren01")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren01TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren02")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren02TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren04")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren04TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren05")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren05TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren06")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren06TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren07")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren07TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren08")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren08TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren09")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren09TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren10")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren10TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren11")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren11TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        if (curAnimalObj.name == "waixingren12")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.waixingren12TuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }
        /*if (curAnimalObj.name == "songshu")
        {
            GetComponent<GradeSystem>().CompareColorCount(GradeSystem.songShuTuSeArea, Manager.mainTexture, GradeSystem.storeRGB, GradeSystem.daXiangError);
        }*/
    }
}
