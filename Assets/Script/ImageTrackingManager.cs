using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR.ARCore;

public class ImageTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject loading;
    [SerializeField] private GameObject UI;
    [SerializeField] private ARSession arSession;
    [SerializeField] private GameObject[] _arGameObjects;
    [SerializeField] private PopupContentData[] _popupContentDatas;
    [SerializeField] private ARTrackedImageManager _arTrackedImageManager;

    [Header("Pop-up Settings")]
    [SerializeField] private GameObject _infoPopupPrefab;
    [SerializeField] private Vector3 _popupOffset = new Vector3(0f, 0.2f, 0f);
    [SerializeField] private LayerMask _raycastLayers;

    private readonly Dictionary<string, GameObject> _instantiatedModels = new();
    private readonly Dictionary<string, GameObject> _activePopups = new();
    private bool wait = true;

    void OnStart()
    {
        loading.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _raycastLayers))
            {
                foreach (var entry in _instantiatedModels)
                {
                    if (hit.collider.gameObject == entry.Value)
                    {
                        ToggleInfoPopup(entry.Key, entry.Value.transform);
                        return;
                    }
                }

                if (hit.collider.CompareTag("next"))
                {
                    var controller = hit.collider.GetComponentInParent<InfoPopupController>();
                    controller?.GoToNextPage();
                    return;
                }

                var popup = hit.collider.GetComponentInParent<InfoPopupController>();
                if (popup != null && popup.gameObject.activeSelf)
                {
                    popup.gameObject.SetActive(false);
                }
            }
        }
    }
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        if (!wait) return;

        foreach (var trackedImage in eventArgs.added)
            SpawnOrUpdateModel(trackedImage);

        foreach (var trackedImage in eventArgs.updated)
            SpawnOrUpdateModel(trackedImage);

        foreach (var trackedImage in eventArgs.removed)
        {
            string name = trackedImage.referenceImage.name;
            if (_instantiatedModels.ContainsKey(name))
            {
                Destroy(_instantiatedModels[name]);
                _instantiatedModels.Remove(name);
            }

            if (_activePopups.ContainsKey(name))
            {
                Destroy(_activePopups[name]);
                _activePopups.Remove(name);
            }
        }
    }
    void SpawnOrUpdateModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject prefab = null;

        for (int i = 0; i < _arGameObjects.Length; i++)
        {
            if (imageName.Contains((i + 1).ToString()))
            {
                prefab = _arGameObjects[i];
                break;
            }
        }

        if (prefab == null)
        {
            Debug.LogWarning($"Model untuk {imageName} tidak ditemukan.");
            return;
        }

        if (!_instantiatedModels.ContainsKey(imageName))
        {
            GameObject newModel = Instantiate(prefab, trackedImage.transform);
            _instantiatedModels.Add(imageName, newModel);
        }
        else
        {
            GameObject existingModel = _instantiatedModels[imageName];
            existingModel.transform.position = trackedImage.transform.position;
            existingModel.SetActive(true); 
        }
        StartCoroutine(ScanCooldown());
    }
    IEnumerator ScanCooldown()
    {
        wait = false;
        yield return new WaitForSeconds(5f);
        wait = true;
    }
    void ToggleInfoPopup(string imageName, Transform modelTransform)
    {
        if (_infoPopupPrefab == null) return;

        PopupContentData data = null;
        for (int i = 0; i < _popupContentDatas.Length; i++)
        {
            if (imageName.Contains((i + 1).ToString()))
            {
                data = _popupContentDatas[i];
                break;
            }
        }
        if (data == null)
        {
            Debug.LogWarning($"Tidak ada data pop-up untuk {imageName}");
            return;
        }

        if (_activePopups.ContainsKey(imageName) && _activePopups[imageName] != null)
        {
            bool isActive = _activePopups[imageName].activeSelf;
            _activePopups[imageName].SetActive(!isActive);

            if (!isActive)
            {
                var controller = _activePopups[imageName].GetComponent<InfoPopupController>();
                controller?.SetPopupData(data);
                controller?.ResetToFirstPage();
            }
        }
        else
        {
            
            GameObject popup = Instantiate(_infoPopupPrefab, modelTransform);
            _activePopups[imageName] = popup;

            popup.GetComponent<InfoPopupController>()?.SetPopupData(data);
            popup.SetActive(true);
        }
    }
    public void Refresh3Dobjects()
    {
        foreach (var model in _instantiatedModels.Values)
        {
            if (model != null) Destroy(model);
        }
        _instantiatedModels.Clear();

        foreach (var popup in _activePopups.Values)
        {
            if (popup != null) Destroy(popup);
        }
        _activePopups.Clear();
        StartCoroutine(ResetTracking());
    }
    IEnumerator ResetTracking()
    {
        UI.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        arSession.Reset();
        yield return new WaitForSeconds(1f);
        loading.gameObject.SetActive(false);
        UI.gameObject.SetActive(true);
    }
}