namespace RequestBus
{
    public interface IRequestBusSender
    {
        void Send<T>(ref T request) where T : struct, IRequest;
    }
}