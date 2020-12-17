using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour
{
    Renderer m_Renderer;
    private Color color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColour();
    }

    public void ChangeColour()
    {
        if (Input.GetKeyDown("space"))
        {
            color.r = 1f;
            color.g = 1f;
            color.b = 1f;
            // color.a = 255;

            m_Renderer.material.color = color;
        }
            
    }
    
}
