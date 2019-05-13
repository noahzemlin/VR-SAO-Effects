using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawScript : MonoBehaviour
{

    public Texture2D baseTex;
    public Texture2D cutTex;
    public Texture2D patternTex;
    public Material material;
    public int scale = 128;
    private static readonly int Property = Shader.PropertyToID("Texture2D_591618A8");

    public Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = Camera.main;
        cutTex = new Texture2D(scale, scale);
        GetComponent<Renderer>().material.SetTexture(Property, cutTex);
        for (int y = 0; y < cutTex.height; y++)
        {
            for (int x = 0; x < cutTex.width; x++)
            {
                cutTex.SetPixel(x, y, Color.black);
            }
        }
        cutTex.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 cursorPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay=sceneCamera.ScreenPointToRay (cursorPos);

        if (Physics.Raycast(cursorRay, out hit,200)){

                cutTex.SetPixel((int)( hit.textureCoord.x*scale), (int)( hit.textureCoord.y*scale), Color.red);
        }
        
        cutTex.Apply();
    }
    
}
