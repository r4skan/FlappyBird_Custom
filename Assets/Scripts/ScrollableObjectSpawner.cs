using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollableObjectSpawner : MonoBehaviour
{
    public Scroll[] targetObject;
    public GameObject[] spawnedObject;

    public float generateTime;

    private Dictionary<Scroll, ushort> m_dicManagedObj;
    private Vector2 m_generatePos;
    
    [SerializeField] private ushort maxObjCount = 4;

    private void Awake()
    {
        m_dicManagedObj = new Dictionary<Scroll, ushort>();

        foreach (Scroll obj in targetObject)
        {
            // SortedDictionary에서 스폰할 오브젝트 관리
            m_dicManagedObj.Add(obj, 0);
        }
    }

    private void Start()
    {
        InstNextBackground();
    }

    private void InstNextBackground()
    {
        Invoke(nameof(InstNextBackground), generateTime);

        for (int i = 0; i < m_dicManagedObj.Count; ++i)
        {
            Vector2 spriteSize = spawnedObject[i].GetComponent<Collider2D>().bounds.size;
            
            // 조금이라도 겹치지 않으면 실선이 보이는 문제가 있어서 조금 겹치게 함
            var nextPos = new Vector2(spawnedObject[i].transform.position.x + spriteSize.x - 0.5f,
                spawnedObject[i].transform.position.y);
            
            // 생성할 위치 설정
            m_generatePos = nextPos;
            
            // 이미 최대 개수에 도달해 있다면 생성 X
            if (m_dicManagedObj[targetObject[i]] == maxObjCount) continue;
            spawnedObject[i] = Instantiate(targetObject[i].gameObject, m_generatePos, Quaternion.identity);
            m_dicManagedObj[targetObject[i]]++;
            
            // 오브젝트가 삭제될 때 이벤트 호출되어 RemoveObjectCount 실행
            spawnedObject[i].GetComponent<Scroll>().OnCountUpdate += RemoveObjectCount;
        }
    }

    private void RemoveObjectCount(string strName)
    {
        Scroll matchedObj = null;
        
        foreach (Scroll obj in m_dicManagedObj.Keys.ToList())
        {
            string objName = obj.name.Insert(obj.name.Length, "(Clone)");
            if (objName != strName) continue;
            
            matchedObj = obj;
            break;
        }

        if (matchedObj == null) return;
        m_dicManagedObj[matchedObj]--;
    }
}
