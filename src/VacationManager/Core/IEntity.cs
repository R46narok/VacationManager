namespace VacationManager.Core;

public interface IEntity<T>
{
    public T Id { get; set; }
}

public class EntityBase<T> : IEntity<T>
{
    public T Id { get; set; }
}
