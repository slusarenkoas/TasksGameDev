using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour
{
    [SerializeField] private LayerMask _layer;
    
    //Сохраняем ориг цвет
    private Color originalColor = Color.white;
    //Поле для последнего измененного тайла
    private Renderer _lastHighlightedRenderer;

    private void Update()
    { 
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out var hitInfo, 100f, _layer))
        {
            var hitObject = hitInfo.collider.gameObject;
            
            //Если луч попал в не обходимый layer
            if (_layer != 0)
            {
                var renderer = hitObject.GetComponent<Renderer>();
                
                //Проверяем не является ли наш объект уже измененным или null
                if (renderer != null && renderer != _lastHighlightedRenderer)
                {
                    //Предыдущий изменный тайл меняем в оригинальный цвет
                    if (_lastHighlightedRenderer != null)
                    {
                        _lastHighlightedRenderer.material.color = originalColor;
                    }
                    
                    //Сохраняем оригинальный цвет для нынешнего тайла
                    originalColor = renderer.material.color;
                    
                    renderer.material.color = Color.gray;
                    //Сохраняем тайл как последний измененный
                    _lastHighlightedRenderer = renderer;
                }
            }
            //Если не попали в необходимый тайл с нужной маской
            else if (_lastHighlightedRenderer != null)
            {
                //Последний измененный возвращаем к оригиналу
                _lastHighlightedRenderer.material.color = originalColor;
                //Последний измененный null
                _lastHighlightedRenderer = null;
            }
        }
        //Если не попали ни накакой объект, меняем последний измененный
        else if (_lastHighlightedRenderer != null)
        {
            _lastHighlightedRenderer.material.color = originalColor;
            _lastHighlightedRenderer = null;
        }
    }
}

