namespace ProjectState.Public
{
    public abstract class StateBase
    {
        public static StateBase Create<T>() where T : StateBase, new() => new T();
    }
}