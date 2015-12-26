//======================================================
// Create by @Peng Guang Hui
// 2015/12/16 11:43:46
//======================================================
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LogicEngine.Unity.Toolkit
{
    [RequireComponent(typeof(Camera))]
    public class FOV45 : MonoBehaviour
    {
        Camera mCamera;

        void Awake()
        {
            mCamera = GetComponent<Camera>();
            mCamera.transform.rotation = Quaternion.Euler(45f, -45f, 0f);
        }
        void Update()
        {
            CaptureMouse();
        }
        Vector3 Screen2Ground(Vector2 screen)
        {
            return mCamera.Screen2Ground(screen);
        }

        public void FocusOnGround(Vector3 world)
        {
            var cur_world = Screen2Ground(UtilScreen.GetScreenCenter());
            mCamera.transform.Translate(world - cur_world, Space.World);
        }
        public void Zoom(float distance)
        {
            var need = Vector3.Distance(mCamera.transform.position, Screen2Ground(UtilScreen.GetScreenCenter())) - distance;
            mCamera.transform.Translate(0, 0, need, Space.Self);
        }

        #region CaptureMouse
        enum MouseDragState
        {
            None,
            Start,
            Dragging,
        }
        MouseDragState mMouseDragState = MouseDragState.None;
        Vector2 mDragBegin;
        void CaptureMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mMouseDragState = MouseDragState.Start;
                mDragBegin = Input.mousePosition;
                OnDown(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (mMouseDragState == MouseDragState.Start)
                {
                    OnClick(Input.mousePosition);
                }
                mMouseDragState = MouseDragState.None;
                OnUp(Input.mousePosition);
            }
            else if (mMouseDragState == MouseDragState.Start && Vector2.Distance(Input.mousePosition, mDragBegin) > 5f)
            {
                mMouseDragState = MouseDragState.Dragging;
            }
            else if (mMouseDragState == MouseDragState.Dragging)
            {
                OnDrag(Input.mousePosition);
            }
        }
        void OnClick(Vector2 screen_position)
        {
        }
        void OnUp(Vector2 screen_position)
        {
        }
        void OnDown(Vector2 screen_position)
        {
        }
        void OnDrag(Vector2 screen_position)
        {
        }
        #endregion
    }
}