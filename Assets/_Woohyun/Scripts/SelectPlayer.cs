using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{

    [SerializeField]
    private float pickupRange = 2.0f;
    [SerializeField]
    private LayerMask itemLayer;

    public Camera mainCamera;


    public SteamVR_LaserPointer steamVR_LaserPointer;
    public SteamVR_Behaviour_Pose poseBehaviour;
    public SteamVR_Input_Sources handType;


    [Header("사운드 클립")]
    public AudioClip hoverSound;
    public AudioClip clickSound;


    private bool btn_mode1 = false;
    private bool btn_mode2 = false;
    //public AudioClip fireSound;

    void Start()
    {
        // LaserPointer 이벤트 구독
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn += OnPointerIn;
            steamVR_LaserPointer.PointerClick += OnPointerClick;
        }
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (steamVR_LaserPointer != null)
        {
            steamVR_LaserPointer.PointerIn -= OnPointerIn;
            steamVR_LaserPointer.PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        /* 모드 1 */
        if (e.target.gameObject.CompareTag("btn_mode1") == true)
        {
            btn_mode1 = true;
        }
        else
        {
            btn_mode1 = false;
        }

        /* 모드 2 */
        if (e.target.gameObject.CompareTag("btn_mode2") == true)
        {
            btn_mode2 = true;
        }
        else
        {
            btn_mode2 = false;
        }

    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        if (btn_mode1 == true)
        {
            SceneManager.LoadScene("OutMap");
        }
        else if (btn_mode2 == true)
        {
            SceneManager.LoadScene("SciFi_Warehouse");
        }
    }

    void Update()
    {

    }

}
