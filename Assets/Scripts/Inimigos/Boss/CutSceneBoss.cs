using System.Collections;
using TMPro;
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
    public AudioSource audioBossFinal;
    public TextMeshProUGUI textoBoss;
    private bool cutsceneStarted = false;

    void Start()
    {
        BarraBossUI.SetActive(false);
        cam = FindFirstObjectByType<CameraDinamica>();
        playerInputSystem = FindFirstObjectByType<PlayerInputSystem>();
        triggerCutSceneBoss = FindFirstObjectByType<TriggerCutSceneBoss>();
        boss = FindFirstObjectByType<Boss>();
        cinemachineCameraPlayer = FindFirstObjectByType<CinemachineCamera>();
        boss.tag = "Untagged";
        textoBoss.enabled = false;

        cinemachineCameraPlayer.Lens.OrthographicSize = 5f;
    }

    void Update()
    {
        if (triggerCutSceneBoss.isCutScene)
        {
            StartCoroutine(delayedIsCutScene());
            if (!cutsceneStarted)
            {
                cutsceneStarted = true;
                StartCoroutine(HandleCutScene());
            }
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
        textoBoss.gameObject.SetActive(false);

        cinemachineCameraPlayer.Lens.OrthographicSize = Mathf.Lerp(cinemachineCameraPlayer.Lens.OrthographicSize, 6.5f, Time.deltaTime * 2f);
    }

    IEnumerator HandleCutScene()
    {
        yield return new WaitForSeconds(1f);
        audioBossFinal.Play();

        textoBoss.enabled = true;
        Color color = textoBoss.color;
        color.a = 0f;
        textoBoss.color = color;

        float timer = 0f;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / 1);
            textoBoss.color = color;
            yield return null;
        }
    }
}
