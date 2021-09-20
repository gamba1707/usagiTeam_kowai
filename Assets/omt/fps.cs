using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fps : MonoBehaviour
{
    private float fpscount;
    TextMeshProUGUI fpsText;
    // Start is called before the first frame update
    void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine("waitone");
        //Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        fpscount = 1f / Time.deltaTime;
        //Debug.Log(""fpscount.ToString("0.0"));
        
    }

    IEnumerator waitone()
    {
        while (true)
        {
            fpsText.text = fpscount.ToString("0.0");
            yield return new WaitForSeconds(1f);
        }
    }
}
