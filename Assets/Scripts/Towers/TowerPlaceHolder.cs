using UnityEngine;
using GameCore;

public class TowerPlaceHolder : MonoBehaviour
{
    private Tower _tower;

    [SerializeField] private Renderer _zoneRenderer;

    private bool _isHighlighted = false;

    private void OnMouseDown()
    {
        TowerManager.Instance.OnZoneClicked(this);
    }

    public void SetHighlight(bool highlight)
    {
        _isHighlighted = highlight;
        _zoneRenderer.material.color = highlight ? Color.yellow : Color.white;
    }

    public void SetTower(Tower tower)
    {
        _tower = tower;
    }

    public bool IsOccupied()
    {
        return _tower != null;
    }
}