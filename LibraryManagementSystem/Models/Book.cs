namespace LibraryManagementSystem.Models
{
 public class Book : IEquatable<Book>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required string PublisherYear { get; set; }

        public override string ToString()
        {
            return $"Book details: Id: {Id}, Title: {Title}, Author: {Author}, ISBN: {ISBN}, PublisherYear: {PublisherYear}";
        }

        public bool Equals(Book? other)
        {
            if (other is null) return false;
            return Title == other.Title &&
                   Author == other.Author &&
                   ISBN == other.ISBN &&
                   PublisherYear == other.PublisherYear;
        }

        public override bool Equals(object? obj) => Equals(obj as Book);

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Author, ISBN, PublisherYear);
        }
    }
}
