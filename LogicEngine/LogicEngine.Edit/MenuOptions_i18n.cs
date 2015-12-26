//======================================================
// Create by @Peng Guang Hui
// 2015/9/16 12:48:03
//======================================================
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using LogicEngine.Unity;

namespace LogicEngine.Edit
{
    static class MenuOptions_i18n
    {
        const string kUILayerName = "UI";

        const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        const string kKnobPath = "UI/Skin/Knob.psd";
        const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        const string kMaskPath = "UI/Skin/UIMask.psd";

        static DefaultControls.Resources s_StandardResources;

        static DefaultControls.Resources GetStandardResources()
        {
            if (s_StandardResources.standard == null)
            {
                s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
                s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
                s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
                s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
                s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
                s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
                s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
            }
            return s_StandardResources;
        }

        static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
        {
            // Find the best scene view
            SceneView sceneView = SceneView.lastActiveSceneView;
            if (sceneView == null && SceneView.sceneViews.Count > 0)
                sceneView = SceneView.sceneViews[0] as SceneView;

            // Couldn't find a SceneView. Don't set position.
            if (sceneView == null || sceneView.camera == null)
                return;

            // Create world space Plane from canvas position.
            Vector2 localPlanePosition;
            Camera camera = sceneView.camera;
            Vector3 position = Vector3.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
            {
                // Adjust for canvas pivot
                localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
                localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

                localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
                localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

                // Adjust for anchoring
                position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
                position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

                Vector3 minLocalPosition;
                minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
                minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

                Vector3 maxLocalPosition;
                maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
                maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

                position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
                position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
            }

            itemTransform.anchoredPosition = position;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localScale = Vector3.one;
        }

        static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            if (parent == null || parent.GetComponentInParent<Canvas>() == null)
            {
                parent = GetOrCreateCanvasGameObject();
            }

            string uniqueName = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
            element.name = uniqueName;
            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);
            Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);
            GameObjectUtility.SetParentAndAlign(element, parent);
            if (parent != menuCommand.context) // not a context click, so center in sceneview
                SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());

            Selection.activeGameObject = element;
        }

        // Graphic elements

        [MenuItem("GameObject/UI_i18n/Text", false, 3000)]
        public static void AddText(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateText(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI_i18n/Image", false, 3001)]
        public static void AddImage(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateImage(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI_i18n/Raw Image", false, 3002)]
        public static void AddRawImage(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateRawImage(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // Controls

        // Button and toggle are controls you just click on.

        [MenuItem("GameObject/UI_i18n/Button", false, 3030)]
        public static void AddButton(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateButton(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI_i18n/Toggle", false, 3031)]
        public static void AddToggle(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateToggle(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // Slider and Scrollbar modify a number

        [MenuItem("GameObject/UI_i18n/Slider", false, 3033)]
        public static void AddSlider(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateSlider(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI_i18n/Scrollbar", false, 3034)]
        public static void AddScrollbar(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateScrollbar(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // More advanced controls below

        [MenuItem("GameObject/UI_i18n/Dropdown", false, 3035)]
        public static void AddDropdown(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateDropdown(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        [MenuItem("GameObject/UI_i18n/Input Field", false, 3036)]
        public static void AddInputField(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateInputField(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // Containers

        [MenuItem("GameObject/UI_i18n/Canvas", false, 3060)]
        public static void AddCanvas(MenuCommand menuCommand)
        {
            var go = CreateNewUI();
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            if (go.transform.parent as RectTransform)
            {
                RectTransform rect = go.transform as RectTransform;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = Vector2.zero;
            }
            Selection.activeGameObject = go;
        }

        [MenuItem("GameObject/UI_i18n/Panel", false, 3061)]
        public static void AddPanel(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreatePanel(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);

            // Panel is special, we need to ensure there's no padding after repositioning.
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;
        }

        [MenuItem("GameObject/UI_i18n/Scroll View", false, 3062)]
        public static void AddScrollView(MenuCommand menuCommand)
        {
            GameObject go = DefaultControls_i18n.CreateScrollView(GetStandardResources());
            PlaceUIElementRoot(go, menuCommand);
        }

        // Helper methods

        public static GameObject CreateNewUI()
        {
            // Root for the UI
            var root = new GameObject("Canvas");
            root.layer = LayerMask.NameToLayer(kUILayerName);
            Canvas canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            root.AddComponent<CanvasScaler>();
            root.AddComponent<GraphicRaycaster>();
            Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

            // if there is no event system add one...
            CreateEventSystem(false);
            return root;
        }

        [MenuItem("GameObject/UI_i18n/Event System", false, 3101)]
        public static void CreateEventSystem(MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            CreateEventSystem(true, parent);
        }

        static void CreateEventSystem(bool select)
        {
            CreateEventSystem(select, null);
        }

        static void CreateEventSystem(bool select, GameObject parent)
        {
            var esys = Object.FindObjectOfType<EventSystem>();
            if (esys == null)
            {
                var eventSystem = new GameObject("EventSystem");
                GameObjectUtility.SetParentAndAlign(eventSystem, parent);
                esys = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
                eventSystem.AddComponent<TouchInputModule>();

                Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
            }

            if (select && esys != null)
            {
                Selection.activeGameObject = esys.gameObject;
            }
        }

        // Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
        public static GameObject GetOrCreateCanvasGameObject()
        {
            GameObject selectedGo = Selection.activeGameObject;

            // Try to find a gameobject that is the selected GO or one if its parents.
            Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
            if (canvas != null && canvas.gameObject.activeInHierarchy)
                return canvas.gameObject;

            // No canvas in selection or its parents? Then use just any canvas..
            canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
            if (canvas != null && canvas.gameObject.activeInHierarchy)
                return canvas.gameObject;

            // No canvas in the scene at all? Then create a new one.
            return MenuOptions_i18n.CreateNewUI();
        }
    }
}