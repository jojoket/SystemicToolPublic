using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Unity.EditorCoroutines.Editor;

public class Preview : VisualElement
{
    private RenderTexture m_renderTexture;
    private Camera m_camera;
    private Image m_currentImage;
    public GameObject cameraObject;
    public GameObject FocusedObject;
    private float m_angle = 0;

    private void GetTexture()
    {
        cameraObject = GameObject.Find("PreviewCamera");
        if (cameraObject == null)
        {
            cameraObject = new GameObject("PreviewCamera");
            m_camera = cameraObject.AddComponent<Camera>();
        }
        else
        {
            m_camera = cameraObject.GetComponent<Camera>();
        }

        // Create a RenderTexture
        m_renderTexture = new RenderTexture(256, 256, 16);
        m_camera.targetTexture = m_renderTexture;

    }
    public void Clean()
    {
        // Clean up
        if (m_camera != null)
        {
            m_camera.targetTexture = null;
        }
        if (m_renderTexture != null)
        {
            m_renderTexture.Release();
        }
    }

    private void EditorUpdateFocus()
    {
        if (cameraObject && FocusedObject)
        {
            m_angle += Time.deltaTime % 360;
            Vector3 offSet = new Vector3(Mathf.Cos(m_angle), 1, Mathf.Sin(m_angle));
            cameraObject.transform.position = FocusedObject.transform.position + offSet * 5;
            cameraObject.transform.rotation = Quaternion.LookRotation(FocusedObject.transform.position - cameraObject.transform.position);
        }
    }



    public Preview()
    {
        GetTexture();
        if (m_renderTexture == null)
        {
            return;
        }

        m_currentImage = new Image();
        m_currentImage.image = m_renderTexture;
        Add(m_currentImage);
        HideSelf(true);
        EditorApplication.update += EditorUpdateFocus;
    }

    public void HideSelf(bool doHide)
    {
        m_currentImage.tintColor = doHide? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        
    }
}
