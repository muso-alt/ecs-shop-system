using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Newtonsoft.Json;
using ShopComplex.Containers;
using ShopComplex.Data;
using ShopComplex.Services;
using UnityEngine;
using UnityEngine.Networking;

namespace ShopComplex.Systems
{
    public class RequestSystem : IEcsInitSystem
    {
        private EcsCustomInject<SceneService> _sceneService;

        private UnityWebRequest _webRequest;
        private Container _container;
        private List<ItemStruct> _items;

        public void Init(IEcsSystems systems)
        {
            StartAsync().Forget();
        }

        private async UniTask StartAsync()
        {
            var json = await LoadRemoteJson();

            DeserializeContainer(json);

            foreach (var item in _items)
            {
                Debug.Log(item.Name + " | " + item.Price);
            }

            //_items.Add(new ItemStruct("Bracer", 505, false, 0));

            SerializeContainer();

            await PostUpdate();
        }

        private async UniTask<string> LoadRemoteJson()
        {
            var webRequest = new UnityWebRequest(_sceneService.Value.URL + "?id=1", "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            webRequest.SetRequestHeader("Content-Type", "multipart/form-data");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _sceneService.Value.AccessToken);
            
            await webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                return string.Empty;
            }
            
            var jsonText = webRequest.downloadHandler.text;
            
            Debug.Log(jsonText);
            
            return jsonText;
        }

        private async UniTask PostUpdate()
        {
            var form = new WWWForm();
            form.AddField("value", _container.value);

            var webRequest = UnityWebRequest.Post(_sceneService.Value.URL + $"?id={_container.id}", form);
        
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Authorization", "Bearer " + _sceneService.Value.AccessToken);

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Request failed: " + webRequest.error);
            }
        }

        private void DeserializeContainer(string json)
        {
            _container = JsonConvert.DeserializeObject<Container>(json);

            if (_container != null)
            {
                _items = JsonConvert.DeserializeObject<List<ItemStruct>>(_container.value);
            }
        }
        
        private void SerializeContainer()
        {
            _container.value = JsonConvert.SerializeObject(_items);
        }
    }
}