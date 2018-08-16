using UnityEngine;
using System.Collections;


public class XiaoGuaiWuFangDa : MonoBehaviour {
    //[HideInInspector]
    public bool isStartFangDa;
    private bool isFangDaFinish;
    //[HideInInspector]
    public bool isCanClick;
    public float FangDaSpeed;
    public GameObject XueTiao;
	// Use this for initialization
	void Start () {
        FangDaSpeed = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (isStartFangDa)
        {
            if (FangDaSpeed <= 1)
            {
                FangDaSpeed = FangDaSpeed + 0.01f;
                this.transform.localScale = new Vector3(FangDaSpeed, FangDaSpeed, FangDaSpeed);              
            }
            else
            {
                ReStart();
                isCanClick = true;
                this.transform.GetComponent<Animation>().Play("GuaiWuHengYiAnimation");
                XueTiao.SetActive(true);
            }
        }
	}

    private void ReStart()
    {
        isFangDaFinish = false;
        isStartFangDa = false;
        FangDaSpeed = 0;
    }
}
