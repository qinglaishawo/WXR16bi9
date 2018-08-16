using UnityEngine;
using System.Collections;
using UnityEditor;

public class SezrchClipWithoutScriptInModel : Editor
{
    //临时记录当前查找的gameobject
    static GameObject g_TempObj;
    //存放粒子,animation,audiosource的name的gameobject
    static GameObject g_NameObj;
    //存放动画剪辑名称的gameobject
    static GameObject g_ClipNameObj;
    //存放带有碰撞的gameobject
    static GameObject g_ColliderNameObj;
    //动画剪辑数组
    static AnimationClip[] a_AniClips;
    
    //添加脚本
    [MenuItem("PrecastActionManage/AddPrecastActionScript")]
    public static void AddGetModelCompent()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length;i++ )
        {
            if(!tSelectTrans[i].gameObject.GetComponent<GetModelCompent>())
            {
                GetModelCompent gGetModelScript = new GetModelCompent();
              
                gGetModelScript = tSelectTrans[i].gameObject.AddComponent<GetModelCompent>();
                
            }
           /* if (!tSelectTrans[i].gameObject.GetComponent<AnimalIndividuality>())
            {
                AnimalIndividuality animalIndividuality = new AnimalIndividuality();
                animalIndividuality = tSelectTrans[i].gameObject.AddComponent<AnimalIndividuality>();
            }*/
        }
    }

    [MenuItem("PrecastActionManage/DeletePrecastActionScript")]
    public static void DeleteGetModelCompent()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length; i++)
        {
            if (tSelectTrans[i].gameObject.GetComponent<GetModelCompent>())
            {
                DestroyImmediate(tSelectTrans[i].gameObject.GetComponent<GetModelCompent>());
            }
        }
    }

    [MenuItem("PrecastActionManage/SerachPrecastActionInfo")]
    public static void GetClipAndOtherInfo()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length;i++ )
        {
            g_ClipNameObj = null;
            g_NameObj = null;
            g_ColliderNameObj = null;

            GameObject TempGameObj;
            for (int nTemp = 0; nTemp < tSelectTrans[i].GetChildCount(); nTemp++)
            {
                TempGameObj = tSelectTrans[i].GetChild(nTemp).gameObject;

                if (TempGameObj.gameObject.name.Equals("clipname"))
                {
                    g_ClipNameObj = TempGameObj;
                }
                else if (TempGameObj.gameObject.name.Equals("modelchildname"))
                {
                    g_NameObj = TempGameObj;
                }
                else if (TempGameObj.gameObject.name.Equals("colliderobjname"))
                {
                    g_ColliderNameObj = TempGameObj;
                }
            }

            if (g_ClipNameObj)
            {
                DestroyImmediate(g_ClipNameObj.gameObject);
            }

            if (g_NameObj)
            {
                DestroyImmediate(g_NameObj.gameObject);
            }

            if (g_ColliderNameObj)
            {
                DestroyImmediate(g_ColliderNameObj.gameObject);
            }

            GameObject TempNewObj = new GameObject();
            TempNewObj.gameObject.name = "clipname";
            TempNewObj.gameObject.transform.parent = tSelectTrans[i].gameObject.transform;
            g_ClipNameObj = TempNewObj;

            TempNewObj = new GameObject();
            TempNewObj.gameObject.name = "modelchildname";
            TempNewObj.gameObject.transform.parent = tSelectTrans[i].gameObject.transform;
            g_NameObj = TempNewObj;

            TempNewObj = new GameObject();
            TempNewObj.gameObject.name = "colliderobjname";
            TempNewObj.gameObject.transform.parent = tSelectTrans[i].gameObject.transform;
            g_ColliderNameObj = TempNewObj;

            GetSearchChild(tSelectTrans[i].gameObject, "", true);

        }
    }

    //递归查找
    static void GetSearchChild(GameObject g_CurrentSearchParticle, string s_PreviousIndex, bool bIsSerachSelf)
    {
        if (bIsSerachSelf)
        {
            SearchInModel(g_CurrentSearchParticle, s_PreviousIndex, true, 0);
        }
        else
        {
            for (int i = 0; i < g_CurrentSearchParticle.gameObject.transform.GetChildCount(); i++)
            {
                SearchInModel(g_CurrentSearchParticle, s_PreviousIndex, false, i);
            }
        }
    }

    static void SearchInModel(GameObject g_CurrentSearchParticle, string s_PreviousIndex, bool bIsSerachSelf, int nIndex)
    {
        //记录索引
        string s_NewStr = nIndex.ToString();
        if (s_PreviousIndex != "")
        {
            s_NewStr = s_PreviousIndex + "_" + s_NewStr;
        }
        //当前遍历的子节点
        if (bIsSerachSelf)
        {
            g_TempObj = g_CurrentSearchParticle.gameObject;
        }
        else
        {
            g_TempObj = g_CurrentSearchParticle.gameObject.transform.GetChild(nIndex).gameObject;
        }

        //找到含有animation的gameobject
        if (g_TempObj.gameObject.GetComponent<Animation>())
        {
            //获得所有动画剪辑并建立空的gameobject存放名字
            a_AniClips = AnimationUtility.GetAnimationClips(g_TempObj.gameObject.GetComponent<Animation>());
            for (int nTemp = 0; nTemp < a_AniClips.Length; nTemp++)
            {
                GameObject NewClipNameGameObj = new GameObject();
                NewClipNameGameObj.name = a_AniClips[nTemp].name;
                NewClipNameGameObj.gameObject.transform.parent = g_ClipNameObj.transform;
            }
            //建立空的gameobject存放挂在animation的gameobject
            GameObject NewNameObj = new GameObject();
            NewNameObj.name = "animation" + "_" + s_NewStr;
            NewNameObj.gameObject.transform.parent = g_NameObj.transform;
        }

        if (g_TempObj.gameObject.GetComponent<AudioSource>())
        {
            //建立空的gameobject存放挂在AudioSource的gameobject
            GameObject NewNameObj = new GameObject();
            NewNameObj.name = "audiosource" + "_" + s_NewStr;
            NewNameObj.gameObject.transform.parent = g_NameObj.transform;
        }

        if (g_TempObj.gameObject.GetComponent<ParticleSystem>())
        {
            //建立空的gameobject存放挂在particle的gameobject
            GameObject NewNameObj = new GameObject();
            NewNameObj.name = "particle" + "_" + s_NewStr;
            NewNameObj.gameObject.transform.parent = g_NameObj.transform;
            return;
        }

        if (g_TempObj.gameObject.GetComponent<BoxCollider>() || g_TempObj.gameObject.GetComponent<SphereCollider>() || g_TempObj.gameObject.GetComponent<CapsuleCollider>() || g_TempObj.gameObject.GetComponent<MeshCollider>() || g_TempObj.gameObject.GetComponent<WheelCollider>() || g_TempObj.gameObject.GetComponent<TerrainCollider>())
        {
            bool IsHasCollider = false;
            for (int i = 0; i < g_ColliderNameObj.transform.GetChildCount();i++ )
            {
                GameObject gTempColliderObj=g_ColliderNameObj.transform.GetChild(i).gameObject;
                if (gTempColliderObj.name.Equals(g_TempObj.gameObject.name))
                {
                    IsHasCollider = true;
                }
            }

            if (!IsHasCollider)
            {
                //建立空的gameobject存放含有collider的gameobject
                GameObject NewNameObj = new GameObject();
                NewNameObj.name = g_TempObj.gameObject.name;
                NewNameObj.gameObject.transform.parent = g_ColliderNameObj.transform;
            }
        }

        GetSearchChild(g_TempObj.gameObject, s_NewStr, false);
    }

    [MenuItem("PrecastActionManage/AddText'_D'")]
    public static void AddGetModelTextD()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length; i++)
        {

            tSelectTrans[i].gameObject.name = tSelectTrans[i].gameObject.name + "_D";
        }
    }

    [MenuItem("PrecastActionManage/AddText'_ND'")]
    public static void AddGetModelTextND()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length; i++)
        {

            tSelectTrans[i].gameObject.name = tSelectTrans[i].gameObject.name + "_ND";
        }
    }

    [MenuItem("PrecastActionManage/AddText'_clone'")]
    public static void AddGetModelText()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length; i++)
        {

            tSelectTrans[i].gameObject.name = tSelectTrans[i].gameObject.name + "_clone";
        }
    }
    [MenuItem("PrecastActionManage/AddBone4")]
    public static void AddGetModelBone4()
    {
        Transform[] tSelectTrans = Selection.GetTransforms(SelectionMode.TopLevel);
        for (int i = 0; i < tSelectTrans.Length; i++)
        {

            for (int w = 0; w < tSelectTrans[i].childCount; w++)
            {
                SkinnedMeshRenderer[] render = tSelectTrans[i].GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer comp in render)
                {
                    comp.quality = SkinQuality.Bone4;
                }
            }
        }
    }
}
