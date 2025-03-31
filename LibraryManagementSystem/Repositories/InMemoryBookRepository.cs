using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly Dictionary<int, Book> _entities;
        public InMemoryBookRepository() => _entities = new Dictionary<int, Book>();
        private int _nextId = 1;

        public void Add(Book entity)
        {
            entity.Id = _nextId++;
            _entities.Add(entity.Id, entity);
        }
        public void Update(Book entity)
        {
            _entities[entity.Id] = entity;
        }
        public void Delete(int id)
        {
            _entities.Remove(id);
        }
        public bool Exists(int id)
        {
            return _entities.ContainsKey(id);
        }
        public IEnumerable<Book> GetAll()
        {
            return _entities.Values;
        }
        public Book? GetById(int id)
        {
            return Exists(id) ? _entities[id] : null;
        }
    }
}
