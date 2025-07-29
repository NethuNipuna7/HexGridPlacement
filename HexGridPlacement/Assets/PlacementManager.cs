using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    public Grid m_hexGrid;
    public GameObject m_hexLandPrefab;
    public LayerMask m_groundMask;
    public GameObject m_hexPreview;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if ((m_groundMask.value & (1 << hit.collider.gameObject.layer)) == 0)
                return;

            Vector3Int cellPosition = m_hexGrid.WorldToCell(hit.point);

            Vector3 cellPositionWorld=m_hexGrid.GetCellCenterWorld(cellPosition);
            m_hexPreview.transform.position = cellPositionWorld;

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Instantiate(m_hexLandPrefab,cellPositionWorld, Quaternion.identity); 
            }

        }
    }
}
