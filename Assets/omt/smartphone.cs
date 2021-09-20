using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class smartphone : MonoBehaviour
{
    [SerializeField] GameObject home, Item, CamAppView, zoomslider, MapApp, InfoApp;
    private Slider slider;
    [SerializeField] Camera came;
    public TextMeshProUGUI TimeText, ueTimeText;
    float scroll;
    public static bool camApp;
    TimeSpan time;
    // Start is called before the first frame update
    void OnEnable()
    {
        slider = zoomslider.GetComponent<Slider>();
        slider.value = 70;
    }

    // Update is called once per frame
    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        time = TimeSpan.FromSeconds(Player.gamesec);
        //Debug.Log("経過時間:"+ time.ToString(@"hh\:mm\:ss"));
        ueTimeText.text = time.ToString(@"hh\:mm\:ss");
        TimeText.text = time.ToString(@"hh\:mm") + "<br><size=30> " + DateTime.Now.ToString("MM月dd日") + "(" + ("日月火水木金土").Substring(int.Parse(DateTime.Now.DayOfWeek.ToString("d")), 1) + ")";
        if (Input.GetMouseButtonDown(1)) OnHomeButton();//右クリックをしたらホームに戻る
        if (camApp) Cam();
    }

    void Cam()
    {
        Cursor.lockState = CursorLockMode.Locked;//カーソルロック
        Cursor.visible = false;//カーソル非表示
        slider.value -= (scroll * 3f);
        came.fieldOfView = slider.value;
    }

    public void OnItemApp()
    {
        home.SetActive(false);
        Item.SetActive(true);
    }

    public void OnHomeButton()
    {
        Cursor.lockState = CursorLockMode.None;//カーソル解除
        Cursor.visible = true;//カーソル表示
        home.SetActive(true);
        Item.SetActive(false);
        CamAppView.SetActive(false);
        camApp = false;
        MapApp.SetActive(false);
        InfoApp.SetActive(false);
    }

    public void OnCamApp()
    {
        Cursor.lockState = CursorLockMode.Locked;//カーソルロック
        Cursor.visible = false;//カーソル非表示
        CamAppView.SetActive(true);
        camApp = true;
        Cursor.lockState = CursorLockMode.Locked;//カーソルロック
        Cursor.visible = false;//カーソル非表示
    }

    public void OnMapApp()
    {
        MapApp.SetActive(true);
    }

    public void OnInfoApp()
    {
        InfoApp.SetActive(true);
    }

    public void OnTweet()
    {
        naichilab.UnityRoomTweet.Tweet("tgc2021_hitokiyojima", "ここに文字を入力", "TGC", "人拒島");
    }

    public void save()
    {
        PlayerPrefs.SetFloat("playtime",Player.gamesec);
        PlayerPrefs.SetString("", DateTime.Now.Year+ DateTime.Now.ToString("MM月dd日") + "(" + ("日月火水木金土").Substring(int.Parse(DateTime.Now.DayOfWeek.ToString("d")), 1) + ")");
    }

    
}
