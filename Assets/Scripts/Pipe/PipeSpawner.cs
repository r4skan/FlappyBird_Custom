using System.Collections;
using UnityEngine;

namespace Pipe
{
    public class PipeSpawner : MonoBehaviour
    {
        public GameObject pipe;
        
        public float spawnDistance = 5f;
        public float spawnDelay = 2f;
        
        [Header("WaveSpawn Options")]
        public float degreeValue;
        public ushort waveGrade;
        public bool isRandomize;


        [SerializeField, ReadOnly]
        private float m_currentWaveDegree = 0;
        private bool m_bIsPlusDegree = true;


        private void OnEnable()
        {
            // 주기적으로 생성
            Invoke(nameof(SpawnPipe), 0);
        }

        private void OnDisable()
        {
            // 코루틴 중지
            CancelInvoke();
            StopCoroutine(nameof(SpawnPipeWave));
        }

        public void SpawnPipe()
        {
            Instantiate(pipe, new Vector2(spawnDistance, Random.Range(-2, 2)), Quaternion.identity);
            
            Invoke(nameof(SpawnPipe), spawnDelay);
        }

        public IEnumerator SpawnPipeWave()
        {
            while (true)
            {
                float sinValue = 0;
                
                if (isRandomize)
                {
                    degreeValue = Random.Range(0, 91);
                }
                
                sinValue = Mathf.Sin(m_currentWaveDegree * Mathf.PI / 180f);
                //Debug.Log(sinValue);

                if (m_bIsPlusDegree)
                {
                    m_bIsPlusDegree = m_currentWaveDegree >= degreeValue * (waveGrade - 1) ? false : true;
                    m_currentWaveDegree += degreeValue;
                }
                else
                {
                    m_bIsPlusDegree = m_currentWaveDegree <= -degreeValue * (waveGrade - 1) ? true : false;
                    m_currentWaveDegree -= degreeValue;
                }

                Instantiate(pipe, new Vector2(spawnDistance, sinValue), Quaternion.identity);

                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
