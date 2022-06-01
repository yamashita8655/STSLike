using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class UIGenerator : EditorWindow {
    [UnityEditor.MenuItem("ShortCutCommand/UIGeneratorStart")]
    public static void UIGeneratorStart() {
        string[] guids;
        GameObject Root = new GameObject("Root");
        Root.AddComponent<RectTransform>();
        GameObject Child1 = new GameObject("Child1");
        Child1.AddComponent<RectTransform>();
        Child1.transform.SetParent(Root.transform);
        GameObject Child1_Child1 = new GameObject("Child1_Child1");
        Child1_Child1.AddComponent<RectTransform>();
        Child1_Child1.transform.SetParent(Child1.transform);
        GameObject Child1_Child2 = new GameObject("Child1_Child2");
        Child1_Child2.AddComponent<RectTransform>();
        Child1_Child2.transform.SetParent(Child1.transform);
        GameObject Child1_Child3 = new GameObject("Child1_Child3");
        Child1_Child3.AddComponent<RectTransform>();
        Child1_Child3.transform.SetParent(Child1.transform);
        GameObject Child2 = new GameObject("Child2");
        Child2.AddComponent<RectTransform>();
        Child2.transform.SetParent(Root.transform);
        GameObject Child3 = new GameObject("Child3");
        Child3.AddComponent<RectTransform>();
        Child3.transform.SetParent(Root.transform);
        GameObject Child3_Child1 = new GameObject("Child3_Child1");
        Child3_Child1.AddComponent<RectTransform>();
        Child3_Child1.transform.SetParent(Child3.transform);
        GameObject Child3_Child1_Child1 = new GameObject("Child3_Child1_Child1");
        Child3_Child1_Child1.AddComponent<RectTransform>();
        Child3_Child1_Child1.transform.SetParent(Child3_Child1.transform);
        GameObject Child4 = new GameObject("Child4");
        Child4.AddComponent<RectTransform>();
        Child4.transform.SetParent(Root.transform);
    }
}
