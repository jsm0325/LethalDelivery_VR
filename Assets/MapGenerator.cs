using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] mapPrefabs; // �ν����Ϳ��� �Ҵ��� �� ������ �迭
    private List<GameObject> generatedMaps = new List<GameObject>(); // ������ ���� ������ ����Ʈ

    public Vector3 startPosition = Vector3.zero; // �� ���� ���� ��ġ

    void Start()
    {
        GenerateMaps(4); // ���� �� 4���� �� ����
    }

    void GenerateMaps(int numberOfMaps)
    {
        // ������ ������ �� ����
        foreach (GameObject map in generatedMaps)
        {
            Destroy(map);
        }
        generatedMaps.Clear();

        // �����ϰ� �� ���� �� ����
        Vector3 currentPosition = startPosition;
        for (int i = 0; i < numberOfMaps; i++)
        {
            GameObject mapPrefab = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // �����ϰ� ������ ����
            GameObject newMap = Instantiate(mapPrefab, currentPosition, Quaternion.identity); // ���� ��ġ�� ������ ����
            generatedMaps.Add(newMap); // ������ �� ����Ʈ�� �߰�

            // ���� ������ ���� ���̸� ����Ͽ� ���� ���� ��ġ ����
            float mapLength = CalculateMapLength(newMap);
            currentPosition += new Vector3(mapLength, 0, 0);
        }
    }

    float CalculateMapLength(GameObject map)
    {
        // ���� ��� ������ ������Ʈ�� ������
        Renderer[] renderers = map.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return 0;

        // ��� �ڽ��� ����Ͽ� ���� ���̸� ����
        Bounds bounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds.size.x; // ���� ����(���⼭�� x�� ���� ����)�� ��ȯ
    }
}
