using ShopComplex.Data;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Services
{
    public class SceneService : MonoBehaviour
    {
        [SerializeField] private string _url;
        [SerializeField] private string _accessToken;
        
        [SerializeField] private ItemsData _items;
        [SerializeField] private ShopPanelView _panelView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private FastBuyView _fastBuyView;
        [SerializeField] private Transform _canvasParent;
        
        public string URL => _url;
        public string AccessToken => _accessToken;
        
        public ItemsData Data => _items;
        public ShopPanelView ShopPanel => _panelView;
        public InventoryView Inventory => _inventoryView;
        public FastBuyView FastBuy => _fastBuyView;
        public Transform CanvasParent => _canvasParent;
    }
}