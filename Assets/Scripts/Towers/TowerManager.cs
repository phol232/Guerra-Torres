using UnityEngine;
using System.Collections.Generic;
using GameCore;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    [SerializeField] private TowerPlaceHolder[] _placeHolders;
    [SerializeField] private GameObject _flecheraPrefab;
    [SerializeField] private GameObject _explosivaPrefab;

    private TowerPlaceHolder _selectedPlace;
    private string _selectedTowerType = "";

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void SelectTower(string towerType)
    {
        _selectedTowerType = towerType;
        foreach (var place in _placeHolders)
            place.SetHighlight(!_selectedTowerType.Equals("") && !place.IsOccupied());
    }

    public void OnZoneClicked(TowerPlaceHolder place)
    {
        if (_selectedTowerType.Equals("") || place.IsOccupied()) return;
        int cost = (_selectedTowerType == "Flechera") ? 50 : 100;
        if (!ResourceManager.Instance.TrySpendGold(cost)) return;

        GameObject prefab = (_selectedTowerType == "Flechera") ? _flecheraPrefab : _explosivaPrefab;
        Tower tower = Instantiate(prefab, place.transform.position, Quaternion.identity).GetComponent<Tower>();
        place.SetTower(tower);
        _selectedTowerType = "";
        foreach (var p in _placeHolders)
            p.SetHighlight(false);
    }
}