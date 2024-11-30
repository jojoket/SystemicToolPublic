using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.EditorCoroutines.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SystemicEditorNode : Node
{
    private SystemicNode m_systemicNode;


    private Vector3Field PositionField;

    private List<NodeComponent> m_nodeComponents = new List<NodeComponent>();

    private List<SystemicEditorNode> _LinkedNodes = new List<SystemicEditorNode>();


    public SystemicEditorNode(SystemicNode node)
    {
        this.AddToClassList("systemic-node");

        m_systemicNode = node;

        Type typeInfo = node.GetType();
        NodeInfoAttribute info = typeInfo.GetCustomAttribute<NodeInfoAttribute>();

        title = info.NodeTitle;

        //replace " " by "-"
        string[] depths = info.MenuItem.Split('/');
        foreach (string depth in depths)
        {
            this.AddToClassList(depth.ToLower().Replace(' ', '-'));
        }

        this.name = typeInfo.Name;
        if (node.title != "")
        {
            title = node.title;
        }

        Button selectButton = new Button();
        selectButton.text = "Select";
        selectButton.clicked += OnSelect;
        titleButtonContainer.Add(selectButton);
        Button focusButton = new Button();
        focusButton.text = "Focus";
        focusButton.clicked += OnFocus;
        titleButtonContainer.Add(focusButton);


        //Components
        Component[] components = node.LinkedGameObject.GetComponents(typeof(Component));

        foreach (Component component in components)
        {
            if (component.GetType().GetCustomAttributes(typeof(SystemicTagAttribute), false).Length <= 0)
            {
                continue;
            }
            NodeComponent nodeComponent = new NodeComponent(component, component.GetType().Name);
            m_nodeComponents.Add(nodeComponent);
            extensionContainer.Add(nodeComponent);
        }

        expanded = false;
        expanded = true;
    }

    private void OnSelect()
    {
        Selection.SetActiveObjectWithContext(m_systemicNode.LinkedGameObject, m_systemicNode.LinkedGameObject);
    }

    private void OnFocus()
    {
        Selection.SetActiveObjectWithContext(m_systemicNode.LinkedGameObject, m_systemicNode.LinkedGameObject);
        EditorCoroutineUtility.StartCoroutineOwnerless(FocusAfter(0.01f));
    }

    private IEnumerator FocusAfter(float sec)
    {
        yield return new EditorWaitForSeconds(sec);
        SceneView.lastActiveSceneView.FrameSelected();
    }

    public override void OnUnselected()
    {
        base.OnUnselected();
        m_systemicNode.rect = GetPosition();
        SystemicToolWindow.window.SetPreviewFocus(null);

    }

    public override void OnSelected()
    {
        base.OnSelected();
        SystemicToolWindow.window.SetPreviewFocus(m_systemicNode.LinkedGameObject);
    }
}
