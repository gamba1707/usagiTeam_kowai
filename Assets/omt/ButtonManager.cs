using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool pause;
    public GameObject lightobj,fpsText;
    // Start is called before the first frame update
    void Start()
    {
        //pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pause) Onpause();
        else Offpause();
        if(Input.GetButtonDown("lightbutton"))
        {
            if (lightobj.activeInHierarchy) lightobj.SetActive(false);
            else lightobj.SetActive(true);
        }
    }

    public void Onpause()//ポーズ画面を出す
    {
        pause = true;
        Cursor.lockState = CursorLockMode.None;//カーソル解除
        Cursor.visible = true;//カーソル表示
        pausePanel.SetActive(true);
    }
    public void Offpause()//ポーズ画面を消す
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;//カーソルロック
        Cursor.visible = false;//カーソル非表示
        pausePanel.SetActive(false);
    }

    public void OnShot()
    {
        DateTime dt = DateTime.Now;
        ScreenCapture.CaptureScreenshot("Assets/"+ dt.ToString("yyyy-MM-dd_HH-mm-ss")+".png");
        Debug.Log("SHOT");
    }

    public void Onfps()
    {
        if (fpsText.activeInHierarchy) fpsText.SetActive(false);
        else fpsText.SetActive(true);
    }

}
