using Runtime.Loop.Components;
using Runtime.Player.Components;
using Unity.Entities;
using Unity.Tiny.Input;

namespace Runtime.Player
{
    public class InputSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var input = World.GetExistingSystem<Unity.Tiny.Input.InputSystem>();
            
            var space = input.GetKeyDown(KeyCode.Space);
            var touch = CheckTouch(input);
            var mouse = CheckMouse(input);

            var shouldJump = space || touch || mouse;
            if (shouldJump)
            {
                Entities
                    .WithAll<TagPlayer>()
                    .WithNone<JumpStateComponent>()
                    .ForEach(entity => { PostUpdateCommands.AddComponent(entity, new JumpStateComponent()); });
            }

            var restart = input.GetKeyDown(KeyCode.A);
            if (restart)
            {
                var restartEvent = GetSingleton<RestartEventComponent>();
                restartEvent.Triggered = true;
                SetSingleton(restartEvent);
            }
        }

        private bool CheckMouse(Unity.Tiny.Input.InputSystem input)
        {
            return input.GetMouseButtonDown(0);
        }

        private bool CheckTouch(Unity.Tiny.Input.InputSystem input)
        {
            if (!input.IsTouchSupported())
                return false;

            var touches = input.TouchCount();
            for (var i = 0; i < touches; i++)
            {
                var touch = input.GetTouch(i);
                if (touch.phase == TouchState.Began)
                    return true;
            }

            return false;
        }
    }
}