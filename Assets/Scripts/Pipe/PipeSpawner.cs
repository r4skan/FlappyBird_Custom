using System.Collections;
using UnityEngine;

namespace Pipe
{
    public class PipeSpawner : MonoBehaviour
    {
        public GameObject pipe;
        
        public float spawnDistance = 5f;
        public float spawnDelay = 2f;

        public float waveDegree = 0;

        private void OnEnable()
        {
            // 주기적으로 생성
            Invoke(nameof(SpawnPipe), 0);
            StartCoroutine(nameof(SpawnPipeWave));
        }

        private void OnDisable()
        {
            // 코루틴 중지
            CancelInvoke();
            StopCoroutine(nameof(SpawnPipeWave));
        }

        private void SpawnPipe()
        {
            Instantiate(pipe, new Vector2(spawnDistance, Random.Range(-2, 2)), Quaternion.identity);
            
            Invoke(nameof(SpawnPipe), spawnDelay);
        }

        IEnumerator SpawnPipeWave()
        {
            while (true)
            {
                if (waveDegree <= 180)
                {
                    waveDegree += 30;
                }
                else
                {
                    
                }

                float sinValue = Mathf.Sin(waveDegree * Mathf.PI / 180f);
                Debug.Log(sinValue);

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
