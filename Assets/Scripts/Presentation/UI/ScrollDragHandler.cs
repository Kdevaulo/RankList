using UnityEngine;
using UnityEngine.UIElements;

namespace Kdevaulo.RankList.UI
{
    public class ScrollDragHandler
    {
        private readonly UIDocument _uiDocument;

        private ScrollView _scrollView;
        private bool _isDragging;
        private Vector2 _startPointer;
        private Vector2 _startOffset;

        public ScrollDragHandler(UIDocument uiDocument)
        {
            _uiDocument = uiDocument;
        }

        public void Initialize()
        {
            var root = _uiDocument.rootVisualElement;
            _scrollView = root.Q<ScrollView>("RanksList");

            _scrollView.RegisterCallback<PointerDownEvent>(OnPointerDown);
            _scrollView.RegisterCallback<PointerMoveEvent>(OnPointerMove);
            _scrollView.RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnPointerDown(PointerDownEvent evt)
        {
            _scrollView.CapturePointer(evt.pointerId);
            _isDragging = true;
            _startPointer = evt.position;
            _startOffset = _scrollView.scrollOffset;
            evt.StopPropagation();
        }

        private void OnPointerMove(PointerMoveEvent evt)
        {
            if (!_isDragging || !_scrollView.HasPointerCapture(evt.pointerId))
                return;

            var delta = (Vector2) evt.position - _startPointer;

            var newOffset = _startOffset - delta;
            _scrollView.scrollOffset = newOffset;
            evt.StopPropagation();
        }

        private void OnPointerUp(PointerUpEvent evt)
        {
            if (!_isDragging)
                return;

            _isDragging = false;
            _scrollView.ReleasePointer(evt.pointerId);
            evt.StopPropagation();
        }

        private void Dispose()
        {
            // todo: dispose call
            _scrollView.UnregisterCallback<PointerDownEvent>(OnPointerDown);
            _scrollView.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
            _scrollView.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }
    }
}