using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public class StateMachine<T> where T : Component
    {
        private T ownerEntity;
        private StateBase<T> currentState;

        public void SetUp(T owner, StateBase<T> entryState)
        {
            ownerEntity = owner;
            currentState = null;

            ChangeState(entryState);
        }

        public void Updated()
        {
            if (ownerEntity == null)
            {
                Debug.LogError($"{typeof(T).Name}의 StateMachine ownerEntity가 지정되어 있지 않습니다.");
                return;
            }

            if (currentState == null)
            {
                Debug.LogError($"{ownerEntity.gameObject.name}의 상태가 지정되어 있지 않습니다.");
                return;
            }
            currentState.Execute_Update(ownerEntity);
        }

        public void LateUpdated()
        {
            if (ownerEntity == null)
            {
                Debug.LogError($"{typeof(T).Name}의 StateMachine ownerEntity가 지정되어 있지 않습니다.");
                return;
            }

            if (currentState == null)
            {
                Debug.LogError($"{ownerEntity.gameObject.name}의 상태가 지정되어 있지 않습니다.");
                return;
            }
            currentState.Execute_LateUpdate(ownerEntity);
        }

        public void FixedUpdated()
        {
            if (ownerEntity == null)
            {
                Debug.LogError($"{typeof(T).Name}의 StateMachine ownerEntity가 지정되어 있지 않습니다.");
                return;
            }

            if (currentState == null)
            {
                Debug.LogError($"{ownerEntity.gameObject.name}의 상태가 지정되어 있지 않습니다.");
                return;
            }
            currentState.Execute_FixedUpdate(ownerEntity);
        }

        public void ChangeState(StateBase<T> state)
        {
            if (state == null)
            {
                return;
            }

            if (currentState != null)
            {
                currentState.ExitState(ownerEntity);
            }

            currentState = state;

            currentState.EnterState(ownerEntity);
        }
    }
}