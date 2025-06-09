using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CutSceneBoss : MonoBehaviour
{
    public CameraDinamica cam;
    public Transform pontoCentroCam;
    public PlayerInputSystem playerInputSystem;
    public TriggerCutSceneBoss triggerCutSceneBoss;
    public CinemachineCamera cinemachineCameraPlayer;

    public GameObject BarraBossUI;
    public Boss boss;
    public bool isFighting = false;

    void Start()
    {
        BarraBossUI.SetActive(false);
        cam = FindFirstObjectByType<CameraDinamica>();
        playerInputSystem = FindFirstObjectByType<PlayerInputSystem>();
        triggerCutSceneBoss = FindFirstObjectByType<TriggerCutSceneBoss>();
        boss = FindFirstObjectByType<Boss>();
        cinemachineCameraPlayer = FindFirstObjectByType<CinemachineCamera>();
        boss.tag = "Untagged";

        cinemachineCameraPlayer.Lens.OrthographicSize = 5f;
    }

    void Update()
    {
        if (triggerCutSceneBoss.isCutScene)
        {
            StartCoroutine(delayedIsCutScene());
        }
        else
        {
            Debug.Log("Cutscene finalizada");
        }
    }

    IEnumerator delayedIsCutScene()
    {
        playerInputSystem.Moviment.action.Disable();
        playerInputSystem.attack.action.Disable();
        playerInputSystem.estilingueAttack.action.Disable();

        yield return new WaitForSeconds(2f);

        // Move a câmera para o ponto central
        cam.cameraFollowTarget.position = Vector3.Lerp(cam.gameObject.GetComponent<CameraDinamica>().cameraFollowTarget.position, pontoCentroCam.position, Time.deltaTime * 2f);

        yield return new WaitForSeconds(3f);

        cam.cameraFollowTarget.position = Vector3.Lerp(cam.gameObject.GetComponent<CameraDinamica>().cameraFollowTarget.position, cam.player.position, Time.deltaTime * 2f);
        playerInputSystem.Moviment.action.Enable();
        playerInputSystem.attack.action.Enable();
        playerInputSystem.estilingueAttack.action.Enable();
        triggerCutSceneBoss.isCutScene = false;
        BarraBossUI.SetActive(true);
        boss.tag = "Enemy";
        isFighting = true;

        cinemachineCameraPlayer.Lens.OrthographicSize = Mathf.Lerp(cinemachineCameraPlayer.Lens.OrthographicSize, 6.5f, Time.deltaTime * 2f);
    }
}
