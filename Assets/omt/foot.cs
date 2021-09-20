using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class foot : MonoBehaviour
{
    private Material footmaterial;
    string footname;
    [SerializeField] AudioClip[] clips;
    private AudioSource audiosource;

    bool ground;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Footon()
    {
        Debug.Log("ground:"+ground);
        if (ground) {
            switch (footname)
            {
                
                default:
                    audiosource.PlayOneShot(clips[0]);//nomal
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ground = true;
        Debug.Log("着地！");
        Footon();
    }

    private void OnTriggerStay(Collider other)
    {
        
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name.Equals("Terrain"))
        {
            footname = "Terrain";
            
        }
        else footmaterial = other.gameObject.GetComponent<Renderer>().material;

        if (!footmaterial.name.Equals("Default-Material (Instance)")) footname = footmaterial.name;
        Debug.Log("footmaterial:" + footname);
    }

    private void OnTriggerExit(Collider other)
    {
        ground = false;
    }
}
