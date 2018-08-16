using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuaiWuCode : MonoBehaviour
{
    public List<GameObject> GuaiWus = new List<GameObject>();
    public GameObject XueTiao;
    public List<Sprite> Sprites = new List<Sprite>();
    public Camera mainCamera;
    public static bool isStart;
    private Sprite SuiJiSprite;
    private int CurGuaiCount;
    private List<Sprite> TempSprites = new List<Sprite>();
    private int LastInt;
    public GameObject manager;
    public GameObject GuaiWuImage;
    private int GuaiWu1ClickIndex;
   
    // Use this for initialization
    void Start()
    {
        CurGuaiCount = 15;
        for (int i = 0; i < Sprites.Count; i++)
        {
            TempSprites.Add(Sprites[i]);
        }
        for(int i=0;i< GuaiWus.Count; i++)
        {
            GuaiWus[i].transform.localScale=new Vector3(0,0,0);
        }
        //gai
        StartCoroutine(StartAppear());
    }

    public IEnumerator StartAppear()
    {
        print("StartAppear");
        AnimalXiaoShi();
        yield return new WaitForSeconds(2);
        GuaiWuImage.SetActive(false);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < GuaiWus.Count; i++)
        {
            ObjAppear(GuaiWus[i],i+1);
        }
        Manager.TimerState = 1;
        Manager.isStartGuaiWu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.isStartGuaiWu && Manager.TimerState == 1)
        {
            if (Time.time - Manager.Timer >= 120)
            {
                print("怪物自动消失");
                Manager.isStartGuaiWu = false;
                Manager.TimerState = 2;
                Manager.Timer = Time.time;
                AnimalAppear();
            }
        }
    }

    public void Button1(GameObject thisObj)
    {
        if (thisObj.GetComponent<XiaoGuaiWuFangDa>().isCanClick)
        {
            if (Manager.isStartGuaiWu && Manager.TimerState == 1)
            {
                GuaiWu1ClickIndex++;
                thisObj.transform.GetComponent<Animation>()["GuaiWuHengYiAnimation"].normalizedSpeed = 0;
                thisObj.transform.GetComponent<Animation>().Play("ImageScaleAnimation");
                StartCoroutine(TempWait(thisObj.transform.GetComponent<Animation>()["GuaiWuHengYiAnimation"].normalizedTime, thisObj));
                thisObj.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount= thisObj.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount-0.33f;
                if (GuaiWu1ClickIndex == 3)
                {
                    thisObj.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = 0;
                    print("被打死");
                }
            }
        }
    }

    IEnumerator TempWait(float f,GameObject obj)
    {
        yield return new WaitForSeconds(0.4f);
        obj.transform.GetComponent<Animation>().Stop();
        obj.transform.GetComponent<Animation>()["GuaiWuHengYiAnimation"].normalizedTime = f;
        obj.transform.GetComponent<Animation>()["GuaiWuHengYiAnimation"].normalizedSpeed = 1;
        obj.transform.GetComponent<Animation>().Play("GuaiWuHengYiAnimation");
    }

    private void SuiJiF(Image image)
    {
        if (TempSprites.Count >= 1)
        {
            int tempInt = Random.Range(0, CurGuaiCount);
            SuiJiSprite = TempSprites[tempInt];
            image.sprite = SuiJiSprite;
            TempSprites.RemoveAt(tempInt);
        }
    }

    private void ObjAppear(GameObject obj,int index)
    {
        if (CurGuaiCount != 8)
        {
            if (index == 1)
            {
                GuaiWu1ClickIndex = 0;
            }
            SuiJiF(obj.transform.GetChild(0).GetComponent<Image>());
            obj.transform.localScale = new Vector3(0, 0, 0);
            obj.SetActive(true);
            obj.GetComponent<XiaoGuaiWuFangDa>().isStartFangDa = true;
            obj.GetComponent<XiaoGuaiWuFangDa>().isCanClick = false;
            CurGuaiCount--;
            if (CurGuaiCount ==8)
            {
                print("正真完成,手动怪物消失");
                AnimalAppear();
                Manager.isStartGuaiWu = false;
                Manager.TimerState = 2;
                Manager.Timer = Time.time;
            }
        }
    }

    private void AnimalAppear()
    {
        foreach (GameObject obj in manager.GetComponent<AnimalMove>().AnimalsObj)
        {
            Renderer[] rendererComponents = obj.GetComponentsInChildren<Renderer>();
            foreach (Renderer component in rendererComponents)
            {
                if (component.material.name.Contains(obj.transform.GetChild(0).name))
                {
                    if (component.material.mainTexture != null)
                    {
                        obj.gameObject.SetActive(true);
                        if (obj.transform.GetChild(0).GetComponent<GetModelCompent>().JudgeAniListOrderIsLastOne())
                        {
                            obj.transform.GetChild(0).GetComponent<GetModelCompent>().PlayAniDefaultOrderImmediate();
                        }
                    }
                }
            }
        }
        ReStart();
    }

    private void AnimalXiaoShi()
    {
        for (int i = 0; i < manager.GetComponent<AnimalMove>().AnimalsObj.Count; i++)
        {
            if (manager.GetComponent<AnimalMove>().AnimalsObj[i].activeInHierarchy)
            {
                print(manager.GetComponent<AnimalMove>().AnimalsObj[i].transform.GetChild(0).name);
                //GetComponent<AnimalMove>().AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().AfterModelUnTrackable();
                manager.GetComponent<AnimalMove>().AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().ResumeAnimation();
                manager.GetComponent<AnimalMove>().AnimalsObj[i].SetActive(false);
            }
        }
    }

    public void ReStart()
    {
        isStart = false;
        CurGuaiCount = 15;
        LastInt = 0;
    }
}
