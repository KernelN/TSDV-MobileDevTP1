using Terresquall;
using UnityEngine;

public class CustomInputManager : MonoBehaviour
{
    public static CustomInputManager inst;
    enum InputDevice { Keyboard, Mobile, Gamepad}
    
    [Header("Set Values")]
    [SerializeField] VirtualJoystick Joystick1;
    [SerializeField] VirtualJoystick Joystick2;
    [SerializeField] bool forceMobile = false;
    [Header("Runtime Values")]
    public bool has2Players;
    [SerializeField] InputDevice inputDevice;

    public Vector2 Axis1 { get; private set; }
    public Vector2 Axis2 { get; private set; }
    
    void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
            return;
        }
        
        inst = this;
    }
    void OnDestroy()
    {
        if(inst == this)
            inst = null;
    }
    void Start()
    {
        if(Application.isMobilePlatform || forceMobile)
            inputDevice = InputDevice.Mobile;
        else
        {
            string[] gamepads = Input.GetJoystickNames();
            bool hasGamepad = false;
            for (int i = 0; i < gamepads.Length && !hasGamepad; i++)
                hasGamepad = gamepads[i] != "";
            if (hasGamepad)
                inputDevice = InputDevice.Gamepad;
            else
                inputDevice = InputDevice.Keyboard;
        }

        if(inputDevice != InputDevice.Mobile)
        {
            Joystick1.gameObject.SetActive(false);
            if(has2Players)
                Joystick2.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        switch (inputDevice)
        {
            case InputDevice.Mobile:
                Axis1 = Joystick1.GetAxis();
                if (has2Players)
                    Axis2 = Joystick2.GetAxis();
                break;
            case InputDevice.Keyboard:
                //Get axis of player 1
                Vector2 axis = new Vector2();
                axis.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
                axis.y = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
                Axis1 = axis;

                if (!has2Players) return;

                //Get axis of player 2
                axis = new Vector2();
                axis.x = Input.GetKey(KeyCode.LeftArrow) ? -1 :
                    Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
                axis.y = Input.GetKey(KeyCode.UpArrow) ? 1 :
                    Input.GetKey(KeyCode.DownArrow) ? -1 : 0;
                Axis2 = axis;
                break;
            case InputDevice.Gamepad:
                //Get axis of player 1
                axis = new Vector2();
                axis.x = Input.GetAxis("Joy_Horizontal");
                axis.y = Input.GetAxis("Joy_Vertical");
                Axis1 = axis;

                if (!has2Players) return;

                //Get axis of player 2
                axis = new Vector2();
                axis.x = Input.GetKey(KeyCode.A) ? -1 :
                    Input.GetKey(KeyCode.D) ? 1 : 0;
                axis.y = Input.GetKey(KeyCode.W) ? 1 :
                    Input.GetKey(KeyCode.S) ? -1 : 0;
                Axis2 = axis;
                break;
        }
    }
}