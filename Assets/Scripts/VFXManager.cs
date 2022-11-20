using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class VFXManager : Singleton<VFXManager>
{

    [SerializeField]
    private List<GameObject> vfxList;

    private Dictionary<string, GameObject> _vfxDic;

    [SerializeField]
    private float lazyDestroyTime = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        _vfxDic = new Dictionary<string, GameObject>();
        foreach (var vfx in vfxList)
        {
            if (_vfxDic.TryAdd(vfx.name, vfx))
            {
                Debug.Log($"特效管理器---》成功注册特效{vfx.name}");
            }
            else
            {
                Debug.LogWarning($"特效管理器---》特效{vfx.name}注册失败！");
            }
        }
    }

    public GameObject CreateVFX(Vector3 position, int index = 0)
    {
        var obj = Instantiate(_vfxDic.ElementAt(index).Value);
        obj.transform.position = position;
        Destroy(obj, lazyDestroyTime);
        return obj;
    }

    public GameObject CreateVFXRange(Vector3 position)
    {
        return CreateVFX(position, Random.Range(0, vfxList.Count));
    }
    
    public GameObject CreateVFX(Vector3 position, string key)
    {
        GameObject obj;
        if (_vfxDic.TryGetValue(key, out var vfx))
        {
            obj = Instantiate(vfx);
            obj.transform.position = position;
            Destroy(obj, lazyDestroyTime);
        }
        else
        {
            var msg = $"特效管理器---》未能找到name={key}的特效";
            Debug.LogWarning(msg);
            throw new Exception(msg);
        }
        return obj;
    }

}