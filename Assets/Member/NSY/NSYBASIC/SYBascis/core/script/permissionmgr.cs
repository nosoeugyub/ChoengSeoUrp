using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;
//# if PLATFORM_ANDROID
# if UNITY_ANDROID
using UnityEngine.Android;
# endif

namespace JMBasic
{
    public class permissionmgr : MonoBehaviour
    {
        void Start()
        {
            //#if PLATFORM_ANDROID
            //AskForPermissions();
        }

        public void AskForPermissions()
        {
#if UNITY_ANDROID
            StartCoroutine(AskForPermissionsIE());
#endif
        }
        private IEnumerator AskForPermissionsIE()
        {
#if UNITY_ANDROID
            //퍼미션 갯수만큼 false 할당한다

            //List<bool> permissions = new List<bool>() { false, false };
            //List<bool> permissionsAsked = new List<bool>() { false, false };
            List<bool> permissions = new List<bool>() { false, false };
            List<bool> permissionsAsked = new List<bool>() { false, false };
            List<Action> actions = new List<Action>()
    {
        //*
        new Action(() => {
            permissions[0] = Permission.HasUserAuthorizedPermission(Permission.Camera);
            if (!permissions[0] && !permissionsAsked[0])
            {
                Permission.RequestUserPermission(Permission.Camera);
                permissionsAsked[0] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            if (!permissions[1] && !permissionsAsked[1])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                permissionsAsked[1] = true;
                return;
            }
        })
        /*
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            if (!permissions[1] && !permissionsAsked[1])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                permissionsAsked[1] = true;
                return;
            }
        })
        */
        /*
        new Action(() => {
            permissions[2] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            if (!permissions[2] && !permissionsAsked[2])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                permissionsAsked[2] = true;
                return;
            }
        })
        */
        /*/
        new Action(() => {
            permissions[0] = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
            if (!permissions[0] && !permissionsAsked[0])
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                permissionsAsked[0] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.Camera);
            if (!permissions[1] && !permissionsAsked[1])
            {
                Permission.RequestUserPermission(Permission.Camera);
                permissionsAsked[1] = true;
                return;
            }
        })
        //*/
        /*
        new Action(() => {
            permissions[2] = Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite);
            if (!permissions[2] && !permissionsAsked[2])
            {
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
                permissionsAsked[2] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[3] = Permission.HasUserAuthorizedPermission("android.permission.READ_PHONE_STATE");
            if (!permissions[3] && !permissionsAsked[3])
            {
                Permission.RequestUserPermission("android.permission.READ_PHONE_STATE");
                permissionsAsked[3] = true;
                return;
            }
        })
        */
    };

            for (int i = 0; i < permissionsAsked.Count;)
            {
                actions[i].Invoke();
                if (permissions[i])
                {
                    ++i;
                }
                yield return new WaitForEndOfFrame();
            }
#else
    yield return new WaitForEndOfFrame();
#endif
        }
    }
}
