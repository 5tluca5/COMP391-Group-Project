using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyManager : MonoBehaviour
{
    public EdgeCollider2D edgeCollider; // EdgeCollider2D組件
    public SpriteShapeRenderer spriteRenderer; // EdgeCollider2D組件
    public GameObject enemyPrefab; // 點的預置物
    public int numPointsToGenerate = 20; // 要生成的點的數量

    private void Start()
    {
        GeneratePointsWithinCollider();
    }

    private void GeneratePointsWithinCollider()
    {
        Vector2[] colliderPoints = edgeCollider.points; // 獲取邊界範圍的點

        for (int i = 0; i < numPointsToGenerate; i++)
        {
            //Vector2 randomPoint = GetRandomPointWithinCollider(colliderPoints);
            var randomPoint = GetRandomPointInSpriteShape(spriteRenderer);

            Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPointWithinCollider(Vector2[] colliderPoints)
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        // 找到邊界範圍的最小和最大值
        foreach (Vector2 point in colliderPoints)
        {
            if (point.x < minX)
                minX = point.x;
            if (point.x > maxX)
                maxX = point.x;
            if (point.y < minY)
                minY = point.y;
            if (point.y > maxY)
                maxY = point.y;
        }

        // 在範圍內生成隨機點
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector2(randomX, randomY);
    }

    private Vector2 GetRandomPointInSpriteShape(SpriteShapeRenderer spriteRenderer)
    {
        Bounds bounds = spriteRenderer.bounds;

        // Generate a random point within the bounds of the shape
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        while(!IsPositionInsideBounds(new Vector2(randomX, randomY), bounds))
        {
            randomX = Random.Range(bounds.min.x, bounds.max.x);
            randomY = Random.Range(bounds.min.y, bounds.max.y);
        }

        return new Vector2(randomX, randomY);
    }

    private bool IsPositionInsideBounds(Vector2 position, Bounds bounds)
    {
        return bounds.Contains(position);
    }
}
