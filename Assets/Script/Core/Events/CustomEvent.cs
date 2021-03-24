namespace Assets.Script.Core.Events
{
    public interface IEvent<T>
    {
        T body { get; set; }
    }

    public class CustomEvent<T> : IEvent<T>
    {
        public T body { get; set; }

        public CustomEvent(T eBody)
        {
            this.body = eBody;
        }
    }
}
