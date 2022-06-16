using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    private static Camera _mainCam;
    private static CinemachineFreeLook _cmFLCam = null;

    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }

            return _mainCam;
        }
        
    }

    public static CinemachineFreeLook FLCam
    {
        get
        {
            if (_cmFLCam == null)
            {
                _cmFLCam = GameObject.FindObjectOfType<CinemachineFreeLook>();
            }

            return _cmFLCam;
        }
    }




    private static Transform _player;

    public static Transform PlayerRef
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").transform;
            }

            return _player;
        }

    }
}
