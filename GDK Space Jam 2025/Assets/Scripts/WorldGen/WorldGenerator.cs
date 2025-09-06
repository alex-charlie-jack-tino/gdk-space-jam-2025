using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _srcPrefabs;
    [SerializeField] private Transform _mapContainer;
    [SerializeField] private Vector2 _mapDimensions;
    /// <summary>
    /// Value between 0 and 1. Values closer to 0 produce fewer blocks
    /// </summary>
    [SerializeField] private float _perlinNoiseThreshold = 0.5f;
    [SerializeField] private float _perlinNoiseScale = 1;

    private void Start()
    {
        Generate();
    }

    private GameObject ChooseRandPrefab(ref GameObject[] src) => src.Length > 0 ? src[Random.Range(0, src.Length)] : null;

    private void Generate()
    {
        float _mapXHalf = _mapDimensions.x / 2, _mapYHalf = _mapDimensions.y / 2;

        for (int y = 0; y < _mapDimensions.y; y++)
        {
            for (int x = 0; x < _mapDimensions.x; x++)
            {
                float currPerlinValue = Mathf.PerlinNoise((float)(x * _perlinNoiseScale), (float)(y * _perlinNoiseScale));
                if (currPerlinValue < _perlinNoiseThreshold)
                {
                    Instantiate(ChooseRandPrefab(ref _srcPrefabs), new Vector3(x - _mapXHalf, 0, y - _mapYHalf), Quaternion.identity, _mapContainer);
                }
            }
        }
    }
}
