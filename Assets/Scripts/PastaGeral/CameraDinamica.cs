using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CameraDinamica : MonoBehaviour
{
    public Transform player;
    public CinemachineCamera vCam;
    public Transform cameraFollowTarget;
    public List<Transform> visibleEnemies;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }

        if (vCam == null)
        {
            vCam = GameObject.FindFirstObjectByType<CinemachineCamera>();

        }

        if (cameraFollowTarget == null)
        {
            GameObject followTarget = new GameObject("CameraFollowTarget");
            cameraFollowTarget = followTarget.transform;
        }

        vCam.Follow = cameraFollowTarget;
    }

    void Update()
    {
        // Encontrar todos os inimigos visíveis
        visibleEnemies = FindVisibleEnemies();

        if (visibleEnemies.Count > 0)
        {
            Vector3 midpoint = player.position;

            foreach (Transform enemy in visibleEnemies)
            {
                midpoint += enemy.position;
            }

            midpoint /= (visibleEnemies.Count + 1);
            cameraFollowTarget.position = new Vector3(midpoint.x, midpoint.y, cameraFollowTarget.position.z);
        }
        else
        {
            cameraFollowTarget.position = player.position;
        }
    }

    List<Transform> FindVisibleEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies
            .Where(e => IsVisible(e.transform))
            .Select(e => e.transform)
            .ToList();
    }

    bool IsVisible(Transform enemy)
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemy.position);
        return viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1 &&
               viewportPos.z > 0;
    }
}
