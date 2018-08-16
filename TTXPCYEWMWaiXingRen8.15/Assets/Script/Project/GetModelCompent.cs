using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetModelCompent : MonoBehaviour, IModecControlInterface
{
    /// <summary>
    /// 所有动画对应关系
    /// </summary>
    public List<ClipAndOthers> l_ClipAndOthers = new List<ClipAndOthers>();
    /// <summary>
    /// 当前查找获得的gameobject
    /// </summary>
    GameObject g_CurSearchObject;
    /// <summary>
    /// 动画播放序列list
    /// </summary>
    public List<ClipAndOthers> l_AniPlayOrder = new List<ClipAndOthers>();
    /// <summary>
    /// 带有animation的gameobject
    /// </summary>
    GameObject g_AnimationGameObj;
    /// <summary>
    /// 当前在动画序列中的动画等信息
    /// </summary>
    ClipAndOthers c_CurClipInListAniOrder;
    /// <summary>
    /// 是否显示后第一播放动画
    /// </summary>
    bool IsFirstPlay = true;
    /// <summary>
    /// 动画暂停时所播放的时间
    /// </summary>
    float f_AniPauseTime = -1.0f;
    /// <summary>
    /// 音效暂停所播放的时间
    /// </summary>
    float f_AuPauseTime = -1.0f;
    /// <summary>
    /// 是否开启循环函数
    /// </summary>
    bool IsUpdate = false;
    /// <summary>
    /// 判断是否暂停
    /// </summary>
    bool IsPause = false;
    /// <summary>
    /// 判断动画是否复位
    /// </summary>
    bool IsAniReset = false;
    /// <summary>
    /// 动画索引最后一位
    /// </summary>
    int nClipNameLastIndex = 0;
    /// <summary>
    /// 判断播放动画链表是否只有一个
    /// </summary>
    bool IsLastClipPlay = true;
    /// <summary>
    /// 判断播放动画链表是否有动画
    /// </summary>
    bool IsListHasClipPlay = false;
    /// <summary>
    /// 判断是否直接播放
    /// </summary>
    bool IsImmediatePlay = false;
    /// <summary>
    /// 获得带有collider的物体的名字
    /// </summary>
    public List<string> l_sColliderObjName = new List<string>();
    /// <summary>
    /// 粒子管理链表
    /// </summary>
    public List<GameObject> l_ManageAllParticleObjList = new List<GameObject>();
    /// <summary>
    /// 出厂前粒子特效所挂载的物体
    /// </summary>
    GameObject gAppearanceParticelObj = null;



    void Awake()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            g_CurSearchObject = this.gameObject.transform.GetChild(i).gameObject;

            //获得带有动画剪辑名称索引的空gameobject
            if (g_CurSearchObject.name.Equals("clipname"))
            {
                for (int nTemp = 0; nTemp < g_CurSearchObject.gameObject.transform.childCount; nTemp++)
                {
                    ClipAndOthers Temp = new ClipAndOthers();
                    Temp.s_ClipName = g_CurSearchObject.gameObject.transform.GetChild(nTemp).gameObject.name;
                    l_ClipAndOthers.Add(Temp);
                }
            }

            //获得带有声音动画索引的空gameobject
            if (g_CurSearchObject.name.Equals("modelchildname"))
            {
                for (int nTemp = 0; nTemp < g_CurSearchObject.gameObject.transform.childCount; nTemp++)
                {
                    //判断当前获得是带有哪种组件的gameobject
                    JudgmentAndGetCurGameObj(g_CurSearchObject.gameObject.transform.GetChild(nTemp).gameObject.name);
                }
            }

            //获得带有collider物体的名字
            if (g_CurSearchObject.name.Equals("colliderobjname"))
            {
                for (int nTemp = 0; nTemp < g_CurSearchObject.gameObject.transform.childCount; nTemp++)
                {
                    string sName = g_CurSearchObject.gameObject.transform.GetChild(nTemp).gameObject.name;
                    l_sColliderObjName.Add(sName);
                }
            }
        }
    }

    /// <summary>
    /// 判断当前获得是带有哪种组件的gameobject
    /// </summary>
    void JudgmentAndGetCurGameObj(string sGameObjName)
    {
        //字符串切割
        string[] sAfterCut = sGameObjName.Split(new char[] { '_' });
        //建立查找索引
        int[] n_SearchIndex = new int[sAfterCut.Length - 1];
        for (int i = 0; i < sAfterCut.Length - 1; i++)
        {
            n_SearchIndex[i] = int.Parse(sAfterCut[i + 1]);
        }
        //根据索引查找目标物体
        if (sAfterCut[0].Equals("animation"))
        {
            //查找animation
            g_AnimationGameObj = SearchAppointObject(n_SearchIndex);
            foreach (ClipAndOthers Item in l_ClipAndOthers)
            {
                //判断动画剪辑是否循环
                if (g_AnimationGameObj.gameObject.GetComponent<Animation>().GetClip(Item.s_ClipName).wrapMode == WrapMode.Loop)
                {
                    Item.b_IsLoop = true;
                }
                else
                {
                    Item.b_IsLoop = false;
                }
            }
        }
        else if (sAfterCut[0].Equals("audiosource"))
        {

            //查找audiosource
            GameObject Temp = SearchAppointObject(n_SearchIndex).gameObject;
            //切割audiosource组件名字
            string[] sCutAudioName = Temp.name.Split(new char[] { '#' });
            foreach (ClipAndOthers Item in l_ClipAndOthers)
            {
                if (Item.s_ClipName.Equals(sCutAudioName[0]))
                {
                    Item.l_AudioSourceList.Add(Temp);
                    break;
                }
            }
        }
        else if (sAfterCut[0].Equals("particle"))
        {
            //查找粒子并匹配
            GameObject Temp = SearchAppointObject(n_SearchIndex).gameObject;
            if (Temp.name.Contains("#"))
            {
                string[] sCutAudioName = Temp.name.Split(new char[] { '#' });
                for (int i = 0; i < 2; i++)
                {
                    foreach (ClipAndOthers Item in l_ClipAndOthers)
                    {
                        if (sCutAudioName[i].Equals(Item.s_ClipName))
                        {
                            OtherParticleInfo OtherInfo = new OtherParticleInfo();
                            OtherInfo.gOtherParticleObj = Temp;

                            if (i == 0)
                            {
                                OtherInfo.bIsPlay = true;
                            }
                            else
                            {
                                OtherInfo.bIsPlay = false;
                            }
                            Item.l_OtherParticleSysList.Add(OtherInfo);
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (ClipAndOthers Item in l_ClipAndOthers)
                {
                    if (Temp.gameObject.name.Equals(Item.s_ClipName))
                    {
                        Item.l_ParticliSystemList.Add(Temp);
                        break;
                    }
                }

                if (Temp.gameObject.name.Equals("cc_bp"))
                {
                    gAppearanceParticelObj = Temp;
                }
            }
        }
    }

    /// <summary>
    /// 根据索引查找指定物体
    /// </summary>
    GameObject SearchAppointObject(int[] nIndex)
    {
        GameObject TempGameObj = null;
        if (nIndex.Length == 1)
        {
            TempGameObj = this.gameObject;
        }
        else if (nIndex.Length > 1)
        {
            TempGameObj = this.gameObject;

            for (int i = 0; i < nIndex.Length - 1; i++)
            {
                TempGameObj = TempGameObj.gameObject.transform.GetChild(nIndex[i + 1]).gameObject;
            }
        }

        return TempGameObj;
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    public bool PlayAnimation(string sAniClipName)
    {
        bool bIsAnimation = false;
        foreach (ClipAndOthers Item in l_ClipAndOthers)
        {
            if (Item.s_ClipName.Equals(sAniClipName))
            {
                l_AniPlayOrder.Add(Item);
                bIsAnimation = true;
                break;
            }
        }
        IsUpdate = true;
        return bIsAnimation;
    }

    /// <summary>
    /// 直接播放动画
    /// </summary>
    public bool PlayAnimationImmediate(string sAniClipName, bool bIsLoop)
    {
        bool bIsAnimation = false;
        if (l_AniPlayOrder.Count > 1)
        {
            l_AniPlayOrder.RemoveRange(1, l_AniPlayOrder.Count - 1);
        }
        bIsAnimation = PlayAnimation(sAniClipName, bIsLoop);
        IsImmediatePlay = bIsAnimation;
        return bIsAnimation;
    }

    /// <summary>
    /// 重载播放动画，可穿是否循环函数
    /// </summary>
    public bool PlayAnimation(string sAniClipName, bool IsLoop)
    {
        bool bIsAnimation = false;
        foreach (ClipAndOthers Item in l_ClipAndOthers)
        {
            if (Item.s_ClipName.Equals(sAniClipName))
            {
                l_AniPlayOrder.Add(Item);
                Item.b_IsLoop = IsLoop;
                bIsAnimation = true;
                if (IsLoop)
                {
                    g_AnimationGameObj.GetComponent<Animation>()[Item.s_ClipName].wrapMode = WrapMode.Loop;
                }
                else
                {
                    g_AnimationGameObj.GetComponent<Animation>()[Item.s_ClipName].wrapMode = WrapMode.Once;
                }
                break;
            }
        }
        IsUpdate = true;
        return bIsAnimation;
    }

    /// <summary>
    /// 摇杆控件播放的动画
    /// <summary>
    public void PlayAnimationForJoyStick(string sAniClipName, float fPlaySpeed)
    {
        if (l_AniPlayOrder.Count == 0)
        {
            PlayAnimation(sAniClipName);
            g_AnimationGameObj.GetComponent<Animation>()[sAniClipName].speed = fPlaySpeed;
        }
        else if (l_AniPlayOrder.Count > 0 && c_CurClipInListAniOrder.s_ClipName.Equals(sAniClipName) == false)
        {
            if (l_AniPlayOrder.Count > 1)
            {
                l_AniPlayOrder.RemoveRange(1, l_AniPlayOrder.Count - 1);
            }
            PlayAnimation(sAniClipName);
            g_AnimationGameObj.GetComponent<Animation>()[sAniClipName].speed = fPlaySpeed;
            IsImmediatePlay = true;
        }
        else if (l_AniPlayOrder.Count > 0 && c_CurClipInListAniOrder.s_ClipName.Equals(sAniClipName))
        {
            g_AnimationGameObj.GetComponent<Animation>()[sAniClipName].speed = fPlaySpeed;
        }
    }

    /// <summary>
    /// 根据动画剪辑中间的名字查找链表中存放的对象，并加入到播放的管理链表中
    /// </summary>
    bool SearchListWithName(string sClipMidName, bool bIsImmediate)
    {
        bool IsSerarched = false;
        string sTempName = sClipMidName + "_" + nClipNameLastIndex.ToString();

        if (bIsImmediate)
        {
            IsSerarched = PlayAnimationImmediate(sTempName, false);
        }
        else
        {
            IsSerarched = PlayAnimation(sTempName);
        }
        return IsSerarched;
    }

    /// <summary>
    /// 默认播放顺序播放动画，等待循环结束
    /// </summary>
    public void PlayAniDefaultOrder(bool IsSinglePlay = false)
    {
        DefaultOrder(false, IsSinglePlay);
    }

    /// <summary>
    /// 默认播放顺序播放动画,不等待循环结束
    /// </summary>
    public void PlayAniDefaultOrderImmediate(bool IsSinglePlay = false)
    {
        DefaultOrder(true, IsSinglePlay);
    }

    /// <summary>
    /// 将默认播放顺序播放动画添加到队列中
    /// </summary>
    void DefaultOrder(bool bIsImmediate, bool IsSinglePlay)
    {
        if (l_AniPlayOrder.Count == 0 && IsFirstPlay)
        {
            //当播放序列为0时，播放出场动画
            bool bTemp = true;
            while (bTemp)
            {
                bTemp = SearchListWithName("cc", false);
                nClipNameLastIndex += 1;
            }
            nClipNameLastIndex = 0;
            SearchListWithName("dj", false);
            if (gAppearanceParticelObj)
            {
                gAppearanceParticelObj.GetComponent<ParticleSystem>().Play();
            }
        }
        else if (l_AniPlayOrder.Count > 0)
        {
            //字符串切割
           
            string[] sAfterCut = c_CurClipInListAniOrder.s_ClipName.Split(new char[] { '_' });

            if (c_CurClipInListAniOrder.s_ClipName.Contains("hd"))
            {
                return;
            }
            nClipNameLastIndex = int.Parse(sAfterCut[sAfterCut.Length - 1]);
            //查找互动
            if (SearchListWithName("hd", bIsImmediate))
            {
                //查找下个待机动画，如果没有，添加上一个待机动画
                nClipNameLastIndex += 1;
                if (SearchListWithName("dj", false) == false)
                {
                    if (!IsSinglePlay)
                    {
                        nClipNameLastIndex = 0;
                        SearchListWithName("dj", false);
                    }
                }
            }
            else if (SearchListWithName("hd", bIsImmediate) == false)
            {
                if (!IsSinglePlay)
                {
                    nClipNameLastIndex = 0;
                    SearchListWithName("dj", false);
                }
            }
        }
        IsUpdate = true;
    }

    /// <summary>
    /// 自定义顺序播放动画,把播放序列中的第一个以后的动画删除，添加一个指定动画(可设置循环)
    /// </summary>
    public void PlayAniDefineOrder(string sAniClipName, bool IsLoop)
    {
        if (l_AniPlayOrder.Count > 1)
        {
            l_AniPlayOrder.RemoveRange(1, l_AniPlayOrder.Count - 1);
        }
        PlayAnimation(sAniClipName, IsLoop);
    }

    /// <summary>
    /// 自定义顺序播放动画,把播放序列中的第一个以后的动画删除，添加一个指定动画
    /// </summary>
    public void PlayAniDefineOrder(string sAniClipName)
    {
        if (l_AniPlayOrder.Count > 1)
        {
            l_AniPlayOrder.RemoveRange(1, l_AniPlayOrder.Count - 1);
        }
        PlayAnimation(sAniClipName);
    }

    /// <summary>
    /// 判断播放链表是否播放到最后一个动画
    /// </summary>
    public bool JudgeAniListOrderIsLastOne()
    {
        return IsLastClipPlay;
    }

    /// <summary>
    /// 判断播放链表是否有动画
    /// </summary>
    public bool JudgeAniListOrderIsHasClip()
    {
        return IsListHasClipPlay;
    }

    /// <summary>
    /// 返回当前播放动画剪辑的名称
    /// <summary>
    public string ReturnCurAniClipName()
    {
        if (l_AniPlayOrder.Count > 0)
        {
            return c_CurClipInListAniOrder.s_ClipName;
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// 清空待播放的动画队列
    /// </summary>
    public void ClearAniOrderList()
    {
        l_AniPlayOrder.Clear();
    }

    /// <summary>
    /// 停止动画
    /// </summary>
    public void StopAnimation()
    {
        IsUpdate = false;

        if (l_AniPlayOrder.Count > 0)
        {
            IsAniReset = true;
            g_AnimationGameObj.GetComponent<Animation>().Rewind();
            AudioExecutionCurOrder(c_CurClipInListAniOrder, 0);
            ParticleExecutionCurOrder(c_CurClipInListAniOrder, 0);
            OtherParticleExecutionCurOrder(0);
            f_AniPauseTime = -1.0f;
            f_AuPauseTime = -1.0f;
        }
    }

    /// <summary>
    /// 暂停动画
    /// </summary>
    public void PauseAnimation()
    {
        IsUpdate = false;
        IsPause = true;
        if (l_AniPlayOrder.Count > 0)
        {
            AniExecutionCurOrder(c_CurClipInListAniOrder, 1);
            AudioExecutionCurOrder(c_CurClipInListAniOrder, 1);
            ParticleExecutionCurOrder(c_CurClipInListAniOrder, 1);
            OtherParticleExecutionCurOrder(1);
        }
    }

    /// <summary>
    /// 恢复动画
    /// </summary>
    public void ResumeAnimation()
    {
        if (l_AniPlayOrder.Count > 0)
        {
            AniExecutionCurOrder(c_CurClipInListAniOrder, 2);
            AudioExecutionCurOrder(c_CurClipInListAniOrder, 2);
            ParticleExecutionCurOrder(c_CurClipInListAniOrder, 2);
            OtherParticleExecutionCurOrder(2);
        }
        if (IsPause)
        {
            IsPause = false;
            IsUpdate = true;
        }
    }

    /// <summary>
    /// animation执行当前动画操作指令,nOrderIndex(0为停止，1为暂停，2是恢复)
    /// </summary>

    void AniExecutionCurOrder(ClipAndOthers CurClip, int nOrderIndex)
    {
        //动画执行指令
        switch (nOrderIndex)
        {
            case 0:
                {
                    g_AnimationGameObj.GetComponent<Animation>().Stop();
                }
                break;
            case 1:
                {
                    //记录暂停的时间
                    if (l_AniPlayOrder.Count > 0 && g_AnimationGameObj.GetComponent<Animation>().isPlaying)
                    {
                        f_AniPauseTime = g_AnimationGameObj.GetComponent<Animation>()[c_CurClipInListAniOrder.s_ClipName].time;
                        g_AnimationGameObj.GetComponent<Animation>().Stop();
                    }
                }
                break;
            case 2:
                {
                    //根据暂停时间恢复播放
                    if (l_AniPlayOrder.Count > 0 && f_AniPauseTime > 0)
                    {
                        g_AnimationGameObj.GetComponent<Animation>()[c_CurClipInListAniOrder.s_ClipName].time = f_AniPauseTime;
                        g_AnimationGameObj.GetComponent<Animation>().CrossFade(c_CurClipInListAniOrder.s_ClipName, 0.3f);
                        f_AniPauseTime = -1.0f;
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// audion执行当前动画操作指令,nOrderIndex(0为停止，1为暂停，2是恢复)
    /// </summary>
    void AudioExecutionCurOrder(ClipAndOthers CurClip, int nOrderIndex)
    {
        //声音执行指令
        foreach (GameObject g_AudioSource in CurClip.l_AudioSourceList)
        {
            switch (nOrderIndex)
            {
                case 0:
                    {
                        g_AudioSource.GetComponent<AudioSource>().Stop();
                    }
                    break;
                case 1:
                    {
                        //音效记录暂停时间
                        if (l_AniPlayOrder.Count > 0 && g_AudioSource.GetComponent<AudioSource>().isPlaying)
                        {
                            f_AuPauseTime = g_AudioSource.GetComponent<AudioSource>().time;
                            g_AudioSource.GetComponent<AudioSource>().Stop();

                        }
                    }
                    break;
                case 2:
                    {
                        //根据暂停时间恢复音效播放
                        if (l_AniPlayOrder.Count > 0 && f_AuPauseTime > 0)
                        {
                            g_AudioSource.GetComponent<AudioSource>().time = f_AuPauseTime;
                            g_AudioSource.GetComponent<AudioSource>().Play();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        if (nOrderIndex == 2)
        {
            f_AuPauseTime = -1.0f;
        }
    }

    /// <summary>
    /// particle执行当前动画操作指令,nOrderIndex(0为停止，1为暂停，2是恢复)
    /// </summary>
    void ParticleExecutionCurOrder(ClipAndOthers CurClip, int nOrderIndex)
    {
        //粒子执行指令
        foreach (GameObject g_ParticleSystem in CurClip.l_ParticliSystemList)
        {
            switch (nOrderIndex)
            {
                case 0:
                    {
                        g_ParticleSystem.GetComponent<ParticleSystem>().Stop();
                    }
                    break;
                case 1:
                    {
                        //粒子记录暂停时间
                        if (l_AniPlayOrder.Count > 0 && g_ParticleSystem.GetComponent<ParticleSystem>().isPlaying)
                        {
                            g_ParticleSystem.GetComponent<ParticleSystem>().Pause();
                        }
                    }
                    break;
                case 2:
                    {
                        //根据暂停时间恢复粒子播放
                        if (l_AniPlayOrder.Count > 0)
                        {
                            g_ParticleSystem.GetComponent<ParticleSystem>().Play();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 其余与动画剪辑不绑定particle执行当前动画操作指令,nOrderIndex(0为停止，1为暂停，2是恢复)
    /// </summary>
    void OtherParticleExecutionCurOrder(int nOrderIndex)
    {
        //其余粒子的播放控制
        foreach (GameObject g_OtherParticleObj in l_ManageAllParticleObjList)
        {
            switch (nOrderIndex)
            {
                case 0:
                    {
                        g_OtherParticleObj.GetComponent<ParticleSystem>().Stop();
                    }
                    break;
                case 1:
                    {
                        //粒子记录暂停时间
                        if (g_OtherParticleObj.GetComponent<ParticleSystem>().isPlaying)
                        {
                            g_OtherParticleObj.GetComponent<ParticleSystem>().Pause();
                        }
                    }
                    break;
                case 2:
                    {
                        //根据暂停时间恢复粒子播放
                        g_OtherParticleObj.GetComponent<ParticleSystem>().Play();
                    }
                    break;
                default:
                    break;
            }
        }
        if (nOrderIndex == 0)
        {
            l_ManageAllParticleObjList.Clear();
        }
    }

    /// <summary>
    /// 其余与动画剪辑不绑定particle判断是否停止播放
    /// </summary>
    void OtherParticleJudgeIsStop(ClipAndOthers CurClip)
    {
        foreach (OtherParticleInfo OtherInfo in CurClip.l_OtherParticleSysList)
        {
            if (!OtherInfo.bIsPlay)
            {
                l_ManageAllParticleObjList.Remove(OtherInfo.gOtherParticleObj);
                OtherInfo.gOtherParticleObj.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    //在更新函数中检测动画并播放
    void Update()
    {
    
        if (l_AniPlayOrder.Count > 0)
        {
            IsListHasClipPlay = true;
            if (l_AniPlayOrder.Count == 1)
            {
                IsLastClipPlay = true;
            }
            else
            {
                IsLastClipPlay = false;
            }
        }
        else
        {
            IsListHasClipPlay = false;
            IsLastClipPlay = false;
        }

        if (IsAniReset)
        {
            if (g_AnimationGameObj.GetComponent<Animation>()[c_CurClipInListAniOrder.s_ClipName].time >= 0.01f)
            {
                g_AnimationGameObj.GetComponent<Animation>().Stop();
                IsAniReset = false;
            }
        }

        if (IsUpdate)
        {
            if (l_AniPlayOrder.Count == 1)
            {
                //拿到播放序列的第一个播放元素
                c_CurClipInListAniOrder = l_AniPlayOrder[0];
                //让第一个播放元素播放动画等
                //如果animation未播放，那么播放动画s_OtherParticleName.Length
                if (g_AnimationGameObj.GetComponent<Animation>().isPlaying == false && IsFirstPlay == true)
                {
                    IsFirstPlay = false;
                    if (c_CurClipInListAniOrder.s_ClipName != "")
                    {
                        g_AnimationGameObj.GetComponent<Animation>().CrossFade(c_CurClipInListAniOrder.s_ClipName, 0.3f);
                    }
                    //播放声音和粒子特效
                    PlayAudioAndParticle(c_CurClipInListAniOrder);
                }//如果animation播放一个完成并且播放队列长度为1,当前动画不循环
                else if (g_AnimationGameObj.GetComponent<Animation>().isPlaying == false && IsFirstPlay == false)
                {
                    //停止播放
                    AniExecutionCurOrder(c_CurClipInListAniOrder, 0);
                    AudioExecutionCurOrder(c_CurClipInListAniOrder, 0);
                    if (IsPause == false)
                    {
                        ParticleExecutionCurOrder(c_CurClipInListAniOrder, 0);
                        //判断其余粒子的关闭
                        OtherParticleJudgeIsStop(c_CurClipInListAniOrder);
                    }
                    //播放队列清空
                    if (IsPause == false)
                    {
                        l_AniPlayOrder.Clear();
                    }
                }
            }
            else if (l_AniPlayOrder.Count > 1)
            {
                //如果animation未播放，那么播放动画
                if (g_AnimationGameObj.GetComponent<Animation>().isPlaying == false && IsFirstPlay == true)
                {
                    IsFirstPlay = false;
                    //拿到播放序列的第一个播放元素
                    c_CurClipInListAniOrder = l_AniPlayOrder[0];
                    g_AnimationGameObj.GetComponent<Animation>().CrossFade(c_CurClipInListAniOrder.s_ClipName, 0.3f);
                    //播放声音和粒子特效
                    PlayAudioAndParticle(c_CurClipInListAniOrder);
                }//如果animation播放一个完成并且播放队列长度大于1,当前动画不循环
                else if (IsFirstPlay == false)
                {
                    if ((g_AnimationGameObj.GetComponent<Animation>().isPlaying == false || IsImmediatePlay) && c_CurClipInListAniOrder.b_IsLoop == false)
                    {
                        IsImmediatePlay = false;
                        //停止播放粒子
                        if (IsPause == false)
                        {
                            ParticleExecutionCurOrder(c_CurClipInListAniOrder, 0);
                            //判断其余粒子的关闭
                            OtherParticleJudgeIsStop(c_CurClipInListAniOrder);
                        }
                        //当前播放元素索引加1
                        c_CurClipInListAniOrder = l_AniPlayOrder[1];
                        g_AnimationGameObj.GetComponent<Animation>().CrossFade(c_CurClipInListAniOrder.s_ClipName, 0.3f);
                        //播放声音和粒子特效
                        PlayAudioAndParticle(c_CurClipInListAniOrder);
                        if (IsPause == false)
                        {
                            l_AniPlayOrder.RemoveAt(0);
                        }
                    }
                    else if (c_CurClipInListAniOrder.b_IsLoop == true && (g_AnimationGameObj.GetComponent<Animation>()[c_CurClipInListAniOrder.s_ClipName].normalizedTime % 1 >= 0.99f || IsImmediatePlay))
                    {
                        IsImmediatePlay = false;
                        //停止播放
                        AniExecutionCurOrder(c_CurClipInListAniOrder, 0);
                        AudioExecutionCurOrder(c_CurClipInListAniOrder, 0);
                        if (IsPause == false)
                        {
                            ParticleExecutionCurOrder(c_CurClipInListAniOrder, 0);
                            //判断其余粒子的关闭
                            OtherParticleJudgeIsStop(c_CurClipInListAniOrder);
                        }
                        //当前播放元素索引加1
                        c_CurClipInListAniOrder = l_AniPlayOrder[1];
                        g_AnimationGameObj.GetComponent<Animation>().CrossFade(c_CurClipInListAniOrder.s_ClipName, 0.3f);
                        //播放声音和粒子特效
                        PlayAudioAndParticle(c_CurClipInListAniOrder);
                        if (IsPause == false)
                        {
                            l_AniPlayOrder.RemoveAt(0);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 模型丢失之后的处理
    /// </summary>
    public void AfterModelUnTrackable()
    {
        IsFirstPlay = true;
        IsPause = false;
        StopAnimation();
        IsAniReset = false;
        g_AnimationGameObj.GetComponent<Animation>().Stop();
        if (gAppearanceParticelObj)
        {
            gAppearanceParticelObj.GetComponent<ParticleSystem>().Stop();
        }
        ClearAniOrderList();
    }

    /// <summary>
    /// 播放声音和粒子特效
    /// </summary>
    void PlayAudioAndParticle(ClipAndOthers PlayItem)
    {
        //播放声音
        foreach (GameObject g_Audio in PlayItem.l_AudioSourceList)
        {
            if (g_Audio.GetComponent<AudioSource>().clip == null)
            {
                g_Audio.GetComponent<AudioSource>().clip = Resources.Load(g_Audio.name) as AudioClip;
            }
            g_Audio.GetComponent<AudioSource>().time = 0f;
            g_Audio.GetComponent<AudioSource>().Play();
            //判断是否循环播放音效
            g_Audio.GetComponent<AudioSource>().loop = PlayItem.b_IsLoop;
        }
        //打开粒子特效
        foreach (GameObject g_Particle in PlayItem.l_ParticliSystemList)
        {
            g_Particle.GetComponent<ParticleSystem>().Play();
        }
        //播放其余粒子特效
        foreach (OtherParticleInfo g_Particle in PlayItem.l_OtherParticleSysList)
        {
            if (g_Particle.bIsPlay)
            {
                g_Particle.gOtherParticleObj.GetComponent<ParticleSystem>().Play();
                l_ManageAllParticleObjList.Add(g_Particle.gOtherParticleObj);
            }
        }
    }

    

    /// <summary>
    /// 存放动画剪辑以及其对应关系的类
    /// </summary>
    public class ClipAndOthers
    {
        /// <summary>
        /// 动画剪辑(animationclip)名称
        /// </summary>
        public string s_ClipName = "";
        /// <summary>
        /// 动画剪辑是否循环
        /// </summary>
        public bool b_IsLoop;
        /// </summary>
        /// 多个动画对应的粒子名字
        /// </summary>
        public List<OtherParticleInfo> l_OtherParticleSysList = new List<OtherParticleInfo>();
        /// <summary>
        /// 存放audiosource的链表
        /// </summary>
        public List<GameObject> l_AudioSourceList = new List<GameObject>();
        /// <summary>
        /// 存放粒子效果的链表
        /// </summary>
        public List<GameObject> l_ParticliSystemList = new List<GameObject>();
    }
    /// <summary>
    /// 存放其余粒子特效信息（即在多个动画中都播放的粒子）
    /// </summary>
    public class OtherParticleInfo
    {
        /// <summary>
        /// 粒子挂载的gameobject
        /// </summary>
        public GameObject gOtherParticleObj;
        /// <summary>
        /// 粒子播放或停止
        /// </summary>
        public bool bIsPlay;
    }

    
}

