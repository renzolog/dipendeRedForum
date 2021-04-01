using System.Collections.Generic;

namespace DipendeForum.Interfaces.Repositories
{
    public interface ICrudRepository<T, E>
    {
        void Add(T element);

        T GetById(E id);

        ICollection<T> GetAll();

        void Update(T element);

        void Delete(T element);
    }
}