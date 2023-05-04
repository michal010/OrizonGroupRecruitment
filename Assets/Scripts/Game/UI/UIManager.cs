using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIManager
{
    void ShowGameOverPanel();
}

[FromFactory("UIManager")]
public class UIManager : MonoBehaviour, IUIManager
{
    public virtual void ShowGameOverPanel()
    {
    }
}
