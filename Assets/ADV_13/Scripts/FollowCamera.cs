using Cinemachine;

namespace ADV_13
{
    public class FollowCamera
    {
        private readonly CinemachineVirtualCamera _camera;

        public FollowCamera(CinemachineVirtualCamera camera)
        {
            _camera = camera;
        }

        public void SetFollow(Character character)
        {
            _camera.Follow = character.transform;
        }

        public void ClearFollow()
        {
            _camera.Follow = null;
        }
    }
}