using UnityEngine;

namespace ShopComplex.Services
{
    public class SceneService : MonoBehaviour
    {
        [SerializeField] private string _url;
        [SerializeField] private string _accessToken;

        public string URL => _url;
        public string AccessToken => _accessToken;
    }
}