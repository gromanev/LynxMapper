namespace LynxMapper.Abstractions
{
    public interface ILynxMapper
    {
        T1 Map<T1, T2>(T2 value) 
            where T1 : class
            where T2 : class;
    }
}
