using UnityEngine;
using System.Collections;

public class LongJuanFeng : MonoBehaviour {
    public GameObject manager;
    public GameObject LongJuanFengs;
    public GameObject LongJuanFengImage;
    public Camera mainCamera;
    private int LongJuanFengCount;
    // Use this for initialization
    void Start () {
        LongJuanFengs.SetActive(false);
        LongJuanFengCount = LongJuanFengs.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.isStartLongJuanFeng && Manager.TimerState == 3)
        {
            if (Time.time - Manager.Timer >= 120)
            {
                print("龙卷风自动消失");
                Manager.isStartLongJuanFeng = false;
                Manager.TimerState = 0;
                Manager.Timer = Time.time;
                AnimalAppear();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                if (hitinfo.collider.tag == "LongJuanFeng")
                {
                    hitinfo.collider.gameObject.SetActive(false);
                    LongJuanFengCount--;
                    if (LongJuanFengCount == 0)
                    {
                        print("手动消灭龙卷风");
                        Manager.isStartLongJuanFeng = false;
                        Manager.TimerState = 0;
                        Manager.Timer = Time.time;
                        AnimalAppear();
                    }
                }
            }
        }
    }

    private void AnimalAppear()
    {
        /*for (int i = 0; i < manager.GetComponent<AnimalMove>().AnimalsObj.Count; i++)
        {
            if (manager.GetComponent<AnimalMove>().AnimalsObj[i].activeInHierarchy)
            {
                print(GetComponent<AnimalMove>().AnimalsObj[i].transform.GetChild(0).name);
                //GetComponent<AnimalMove>().AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().AfterModelUnTrackable();
                manager.GetComponent<AnimalMove>().AnimalsObj[i].transform.GetChild(0).GetComponent<GetModelCompent>().ResumeAnimation();
                manager.GetComponent<AnimalMove>().AnimalsObj[i].SetActive(false);
            }
        }*/
        ReStart();
    }

    public IEnumerator StartAppear()
    {
        yield return new WaitForSeconds(2);
        LongJuanFengImage.SetActive(false);
        yield return new WaitForSeconds(3);
        LongJuanFengs.SetActive(true);
    }

    private void ReStart()
    {
        LongJuanFengs.SetActive(false);
        for (int i=0; i < LongJuanFengs.transform.childCount; i++)
        {
            LongJuanFengs.transform.GetChild(i).gameObject.SetActive(true);
        }
        LongJuanFengCount = LongJuanFengs.transform.childCount;
    }
}
