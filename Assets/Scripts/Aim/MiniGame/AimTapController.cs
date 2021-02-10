using UnityEngine;

namespace AimCraftMiniGame
{
    public class AimTapController
    {
        private Camera _camera;
        private IPlayerInput _playerInput;
        
        private RaycastHit2D _hit;
        private Vector3 _touchWorldPosition;
        private Vector2 _touchWorldPosition2D;
        public AimTapController(Camera camera )
        {
            _camera = camera;
            _playerInput = new PlayerInput();
        }

        public void Tick()
        {
            if (_playerInput.LeftClickDown)
            {
                ProcessPlayerClick(_playerInput.MousePosition);
            }
        }

        private void ProcessPlayerClick(Vector3 mousePosition)
        {
            _touchWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
            _touchWorldPosition2D = new Vector2(_touchWorldPosition.x,_touchWorldPosition.y);
            _hit = Physics2D.Raycast(_touchWorldPosition2D,Vector2.zero);
        
        
            if(_hit.collider==null)
                return;
        
            var hitAimTarget = _hit.collider.GetComponentInParent<AimTarget>();
            if (hitAimTarget)
            {
                hitAimTarget.TargetGetHit();
            }
        }
    }
}