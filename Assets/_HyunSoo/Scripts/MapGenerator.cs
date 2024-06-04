using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] mapPrefabs; // 인스펙터에서 할당할 맵 프리팹 배열
    private List<GameObject> generatedMaps = new List<GameObject>(); // 생성된 맵을 저장할 리스트

    public Vector3 startPosition = Vector3.zero; // 맵 생성 시작 위치

    void Start()
    {
        GenerateMaps(4); // 시작 시 4개의 맵 생성
    }

    void GenerateMaps(int numberOfMaps)
    {
        // 이전에 생성된 맵 제거
        foreach (GameObject map in generatedMaps)
        {
            Destroy(map);
        }
        generatedMaps.Clear();

        // 랜덤하게 맵 선택 및 생성
        Vector3 currentPosition = startPosition;
        for (int i = 0; i < numberOfMaps; i++)
        {
            GameObject mapPrefab = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // 랜덤하게 프리팹 선택
            GameObject newMap = Instantiate(mapPrefab, currentPosition, Quaternion.identity); // 현재 위치에 프리팹 생성
            generatedMaps.Add(newMap); // 생성된 맵 리스트에 추가

            // 새로 생성된 맵의 길이를 계산하여 다음 맵의 위치 설정
            float mapLength = CalculateMapLength(newMap);
            currentPosition += new Vector3(mapLength, 0, 0);
        }
    }

    float CalculateMapLength(GameObject map)
    {
        // 맵의 모든 렌더러 컴포넌트를 가져옴
        Renderer[] renderers = map.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return 0;

        // 경계 박스를 계산하여 맵의 길이를 결정
        Bounds bounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds.size.x; // 맵의 길이(여기서는 x축 방향 길이)를 반환
    }
}
