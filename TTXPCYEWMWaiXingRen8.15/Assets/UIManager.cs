using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    private Dictionary<string, Canvas> allCanvasDic = new Dictionary<string, Canvas>();
    private Dictionary<Canvas, Dictionary<string, Button>> canvasesAndButtonsDic = new Dictionary<Canvas, Dictionary<string, Button>>();
    private Dictionary<Canvas, Dictionary<string, Image>> canvasesAndImagesDic = new Dictionary<Canvas, Dictionary<string, Image>>();
    private Dictionary<Canvas, Dictionary<string, Text>> canvasesAndTextsDic = new Dictionary<Canvas, Dictionary<string, Text>>();
    private Dictionary<Canvas, Dictionary<string, GameObject>> canvasesAndOtherGameObjDic = new Dictionary<Canvas, Dictionary<string, GameObject>>();

    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<UIManager>();
            return instance;
        }
    }

    void Awake()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            GameObject tempChild = this.gameObject.transform.GetChild(i).gameObject;
            allCanvasDic.Add(tempChild.name, tempChild.GetComponent<Canvas>());

            Dictionary<string, Button> buttonsInCanvasDic = new Dictionary<string, Button>();
            Dictionary<string, Image> imagesInCanvasDic = new Dictionary<string, Image>();
            Dictionary<string, Text> textsInCanvasDic = new Dictionary<string, Text>();
            Dictionary<string, GameObject> gameObjInCanvasDic = new Dictionary<string, GameObject>();

            SearchUICompentsIngameObj(buttonsInCanvasDic, imagesInCanvasDic, textsInCanvasDic, gameObjInCanvasDic, tempChild);

            canvasesAndButtonsDic.Add(tempChild.GetComponent<Canvas>(), buttonsInCanvasDic);
            canvasesAndImagesDic.Add(tempChild.GetComponent<Canvas>(), imagesInCanvasDic);
            canvasesAndTextsDic.Add(tempChild.GetComponent<Canvas>(), textsInCanvasDic);
            canvasesAndOtherGameObjDic.Add(tempChild.GetComponent<Canvas>(), gameObjInCanvasDic);
        }
    }

    /// <summary>
    /// 非递归广度优先遍历查找
    /// </summary>
    /// <param name="buttonsInCanvasDic"></param>
    /// <param name="imagesInCanvasDic"></param>
    /// <param name="textsInCanvasDic"></param>
    /// <param name="gameObjInCanvasDic"></param>
    /// <param name="searchGameObj"></param>
    private void SearchUICompentsIngameObj(Dictionary<string, Button> buttonsInCanvasDic, Dictionary<string, Image> imagesInCanvasDic, Dictionary<string, Text> textsInCanvasDic, Dictionary<string, GameObject> gameObjInCanvasDic, GameObject searchGameObj)
    {
        //List<GameObject> tempStack = new List<GameObject>();
        //for (int i = 0; i < searchGameObj.transform.childCount; i++)
        //    tempStack.Add(searchGameObj.transform.GetChild(i).gameObject);
        //while (tempStack.Count > 0)
        //{
        //    GameObject tempChild = tempStack[0];
        //    if (tempChild.GetComponent<Button>() != null && !buttonsInCanvasDic.ContainsKey(tempChild.name))
        //        buttonsInCanvasDic.Add(tempChild.name, tempChild.GetComponent<Button>());
        //    else if (tempChild.GetComponent<Image>() != null && !imagesInCanvasDic.ContainsKey(tempChild.name))
        //        imagesInCanvasDic.Add(tempChild.name, tempChild.GetComponent<Image>());
        //    else if (tempChild.GetComponent<Text>() != null && !textsInCanvasDic.ContainsKey(tempChild.name))
        //        textsInCanvasDic.Add(tempChild.name, tempChild.GetComponent<Text>());
        //    else if (!gameObjInCanvasDic.ContainsKey(tempChild.name))
        //        gameObjInCanvasDic.Add(tempChild.name, tempChild);

        //    for (int i = 0; i < tempChild.gameObject.transform.childCount; i++)
        //        tempStack.Add(tempChild.transform.GetChild(i).gameObject);
        //    tempStack.Remove(tempStack[0]);
        //}
        for (int i = 0; i < searchGameObj.transform.childCount; i++)
        {
            GameObject tempChild = searchGameObj.transform.GetChild(i).gameObject;
            if (tempChild.GetComponent<Button>() != null && !buttonsInCanvasDic.ContainsKey(tempChild.name))
                buttonsInCanvasDic.Add(tempChild.name, tempChild.GetComponent<Button>());
            else if (tempChild.GetComponent<Image>() != null && !imagesInCanvasDic.ContainsKey(tempChild.name))
                imagesInCanvasDic.Add(tempChild.name, tempChild.GetComponent<Image>());
            else if (tempChild.GetComponent<Text>() != null && !textsInCanvasDic.ContainsKey(tempChild.name))
                textsInCanvasDic.Add(tempChild.name, tempChild.GetComponent<Text>());
            else if (!gameObjInCanvasDic.ContainsKey(tempChild.name))
                gameObjInCanvasDic.Add(tempChild.name, tempChild);
            SearchUICompentsIngameObj(buttonsInCanvasDic, imagesInCanvasDic, textsInCanvasDic, gameObjInCanvasDic, tempChild);
        }

    }

    private Canvas GetCanvas(string canvasName)
    {
        if (allCanvasDic.ContainsKey(canvasName))
            return allCanvasDic[canvasName];
        else
            return null;
    }

    public Button GetButton(string btnName, string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if(tempCanvas == null)
            return null;

        if (canvasesAndButtonsDic[tempCanvas].ContainsKey(btnName))
            return canvasesAndButtonsDic[tempCanvas][btnName];
        else
            return null;
    }

    public Image GetImage(string imageName, string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        if (canvasesAndImagesDic[tempCanvas].ContainsKey(imageName))
            return canvasesAndImagesDic[tempCanvas][imageName];
        else
            return null;
    }

    public Text GetText(string textName, string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        if (canvasesAndTextsDic[tempCanvas].ContainsKey(textName))
            return canvasesAndTextsDic[tempCanvas][textName];
        else
            return null;
    }

    public GameObject GetOtherUIGameObj(string otherGameObjName, string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        if (canvasesAndOtherGameObjDic[tempCanvas].ContainsKey(otherGameObjName))
            return canvasesAndOtherGameObjDic[tempCanvas][otherGameObjName];
        else
            return null;
    }

    public List<Button> GetAllButtons()
    {
        List<Button> allButtions = new List<Button>();
        foreach (Dictionary<string, Button> buttons in canvasesAndButtonsDic.Values)
        {
            foreach (Button button in buttons.Values)
                allButtions.Add(button);
        }
        return allButtions;
    }

    public List<Image> GetAllImages()
    {
        List<Image> allImages = new List<Image>();
        foreach (Dictionary<string, Image> images in canvasesAndImagesDic.Values)
        {
            foreach (Image image in images.Values)
                allImages.Add(image);
        }
        return allImages;
    }

    public List<Text> GetAllTexts()
    {
        List<Text> allTexts = new List<Text>();
        foreach (Dictionary<string, Text> texts in canvasesAndTextsDic.Values)
        {
            foreach (Text text in texts.Values)
                allTexts.Add(text);
        }
        return allTexts;
    }

    public List<GameObject> GetAllOtherUIGameObjs()
    {
        List<GameObject> allOtherUIGameObjs = new List<GameObject>();
        foreach (Dictionary<string, GameObject> otherUIGameObjs in canvasesAndOtherGameObjDic.Values)
        {
            foreach (GameObject otherUIGameObj in otherUIGameObjs.Values)
                allOtherUIGameObjs.Add(otherUIGameObj);
        }
        return allOtherUIGameObjs;
    }

    public List<Button> GetButtonsInCanvas(string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        List<Button> allButtons = new List<Button>();
        foreach (Button button in canvasesAndButtonsDic[tempCanvas].Values)
            allButtons.Add(button);
        return allButtons;
    }

    public List<Image> GetImagesInCanvas(string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        List<Image> allImages = new List<Image>();
        foreach (Image image in canvasesAndImagesDic[tempCanvas].Values)
            allImages.Add(image);
        return allImages;
    }

    public List<Text> GetTextsInCanvas(string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        List<Text> allTexts = new List<Text>();
        foreach (Text text in canvasesAndTextsDic[tempCanvas].Values)
            allTexts.Add(text);
        return allTexts;
    }

    public List<GameObject> GetOtherUIGameObjsInCanvas(string canvasName)
    {
        Canvas tempCanvas = GetCanvas(canvasName);
        if (tempCanvas == null)
            return null;

        List<GameObject> allOtherUIGameObjs = new List<GameObject>();
        foreach (GameObject otherUIGameObj in canvasesAndOtherGameObjDic[tempCanvas].Values)
            allOtherUIGameObjs.Add(otherUIGameObj);
        return allOtherUIGameObjs;
    }

    /// <summary>
    /// 根据canvas名字获得子类
    /// </summary>
    /// <typeparam name="T">子类类型</typeparam>
    /// <param name="canvasName">canvas名字</param>
    /// <returns>子类类型 或者默认类型</returns>
    public T GetSubclass<T>(string canvasName)
    {
        if (allCanvasDic.ContainsKey(canvasName))
            return allCanvasDic[canvasName].gameObject.GetComponent<T>();
        else
            return default(T);
    }

}
