using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public abstract class StateBase<T> where T : Component
    {
        public abstract void EnterState(T entity);

        public abstract void Execute_Update(T entity);

        public abstract void Execute_LateUpdate(T entity);

        public abstract void Execute_FixedUpdate(T entity);

        public abstract void ExitState(T entity);
    }
}