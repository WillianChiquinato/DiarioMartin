using System.Collections.Generic;
using UnityEngine;

public class AiData : MonoBehaviour
{
    public List<Transform> targets = null;
    public Collider2D[] obstaculos = null;

    public Transform currentTarget;

    public int GetTargetsCount() => targets == null ? 0 : targets.Count;
}
