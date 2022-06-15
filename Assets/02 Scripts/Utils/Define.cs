using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    private static Camera _mainCam;
    private static CinemachineVirtualCamera _cmVCam = null;

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

    public static CinemachineVirtualCamera VCam
    {
        get
        {
            if (_cmVCam == null)
            {
                _cmVCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            }

            return _cmVCam;
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
