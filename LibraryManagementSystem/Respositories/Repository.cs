using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Respositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly Dictionary<int, T> _entities;
        private int _nextId = 1;
        public Repository() => _entities = new Dictionary<int, T>();
        public void Add(T entity)
        {
            entity.Id = _nextId++;
            _entities.Add(entity.Id, entity);
        }
        public void Update(T entity)
        {
            if (_entities.ContainsKey(entity.Id))
            {
                _entities[entity.Id] = entity;
            }
        }
        public void Delete(int id)
        {
            _ = Exists(id) && _entities.Remove(id);
        }
        public bool Exists(int id)
        {
            return _entities.ContainsKey(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _entities.Values;
        }
        public T? GetById(int id)
        {
            return Exists(id) ? _entities[id] : default(T);
        }
    }
    
}
