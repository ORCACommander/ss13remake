﻿using GameObject;
using GameObject.System;
using SS13_Shared;
using SS13_Shared.GO;

namespace CGO.EntitySystems
{
    public class InputSystem : EntitySystem
    {
        public InputSystem(EntityManager em, EntitySystemManager esm)
            : base(em, esm)
        {
            EntityQuery = new EntityQuery();
            EntityQuery.OneSet.Add(typeof(KeyBindingInputComponent));
        }

        public override void Update(float frametime)
        {
            var entities = EntityManager.GetEntities(EntityQuery);
            foreach (var entity in entities)
            {
                var inputs = entity.GetComponent<KeyBindingInputComponent>(ComponentFamily.Input);

                //Animation setting
                if (entity.GetComponent(ComponentFamily.Renderable) is AnimatedSpriteComponent)
                {
                    var animation = entity.GetComponent<AnimatedSpriteComponent>(ComponentFamily.Renderable);
                    
                    //Char is moving
                    if (inputs.GetKeyState(BoundKeyFunctions.MoveRight) ||
                        inputs.GetKeyState(BoundKeyFunctions.MoveDown) ||
                        inputs.GetKeyState(BoundKeyFunctions.MoveLeft) ||
                        inputs.GetKeyState(BoundKeyFunctions.MoveUp))
                    {
                        animation.SetAnimationState(inputs.GetKeyState(BoundKeyFunctions.Run) ? "run" : "walk");
                    }
                        //Char is not moving
                    else
                    {
                        animation.SetAnimationState("idle");
                    }
                }
            }
        }
    }
}