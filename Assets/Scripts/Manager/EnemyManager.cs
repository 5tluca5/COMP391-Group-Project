using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public EdgeCollider2D edgeCollider;
    public SpriteShapeRenderer spriteRenderer; 
    public GameObject enemyPrefab; 
    public int numPointsToGenerate = 20;
    public float spawnInterval = 0.2f;

    List<Enemy> enemies = new List<Enemy>();
    private void Start()
    {
        
    }

    public void GenerateZombies()
    {
        for (int i = 0; i < numPointsToGenerate; i++)
        {
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

    private void Reset()
    {
        enemies.ForEach(x => Destroy(x.gameObject));
        enemies.Clear();
    }
}
