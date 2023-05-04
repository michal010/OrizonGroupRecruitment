using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class FromFactory : Attribute
{
    public string ObjectName { get; }
    public bool ForceAdd;
    public FromFactory(string objectName, bool forceAdd = false)
    {
        ObjectName = objectName;
        ForceAdd = forceAdd;
    }
}

public interface IInjectable<T>
{
    void Inject(T dependency);
}

public class MonoFactory : MonoBehaviour
{
    private static readonly string DATA_PATH = "Prefabs";
    // key - prefab
    public static Dictionary<string, GameObject> prefabs;
    private void Awake()
    {
        // Load all items
        prefabs = new Dictionary<string, GameObject>();
        LoadPrefabsToDictionary();
    }

    public static T Create<T>(string name)
    {
        GameObject go = Instantiate(prefabs[name]);
        if (typeof(T).Equals(typeof(GameObject)))
            return (T)(object)go;
        return go.GetComponent<T>();
    }


    // Extension to inject
    public static T CreateWithDepedency<T,U>(string name, U dependency)
    {
        GameObject go = Instantiate(prefabs[name]);
        T component = go.GetComponent<T>();

        if (component == null)
        {
            throw new InvalidOperationException($"GameObject '{name}' does not have component '{typeof(T).Name}'.");
        }

        if (dependency != null && component is IInjectable<U> injectable)
        {
            injectable.Inject(dependency);
        }

        return component;
    }

    private void LoadPrefabsToDictionary()
    {
        GameObject[] projectPrefabs = Resources.LoadAll<GameObject>(DATA_PATH);
        
        foreach (var prefab in projectPrefabs)
        {
            Component[] components = prefab.GetComponents<Component>();
            // First, check if custom attribute is contained only once in all components
            List<FromFactory> fromFactories = new List<FromFactory>();
            foreach (var component in components)
            {
                FromFactory a = (FromFactory)FromFactory.GetCustomAttribute(component.GetType(), typeof(FromFactory));
                if(a != null)
                    fromFactories.Add(a);
            }
            // If there is only one fromFactory attribute, add prefab to dict
            if(fromFactories.Count == 1)
            {
                if(!prefabs.ContainsKey(fromFactories[0].ObjectName))
                    prefabs.Add(fromFactories[0].ObjectName, prefab);
                else
                {
                    Debug.LogWarning("There is already prefab of: " + fromFactories[0].ObjectName + " skipping...");
                }
            }
            else
            {
                // Iterate trough all attributes, see which one has force add to it, otherwise
                // don't add
                foreach (var ffAttribute in fromFactories)
                {
                    if(ffAttribute.ForceAdd)
                    {
                        if(!prefabs.ContainsKey(ffAttribute.ObjectName))
                        {
                            prefabs.Add(ffAttribute.ObjectName, prefab);
                        }
                        else
                        {
                            Debug.LogWarning("There is already prefab of: " + ffAttribute.ObjectName + " skipping...");
                        }
                    }
                }
            }
        }
    
    }
}
