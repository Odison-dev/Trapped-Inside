using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPersistentCloneObjiects: MonoBehaviour
{
    public void DestroyPersistentClone(string persistentCloneTag)
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag(persistentCloneTag); // 使用标签来查找克隆物体
        foreach (GameObject clone in clones)
        {
            Destroy(clone); // 删除找到的每个克隆物体
        }
    }

}
