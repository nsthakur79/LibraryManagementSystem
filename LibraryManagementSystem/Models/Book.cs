namespace LibraryManagementSystem.Models
{
 public class Book : IEquatable<Book>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required string PublicationYear { get; set; }

        public override string ToString()
        {
            return $"Book details: {nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(Author)}: {Author}, {nameof(ISBN)}: {ISBN}, {nameof(PublicationYear)}: {PublicationYear}";
        }

        public bool Equals(Book? other)
        {
            if (other is null) return false;
            return Title == other.Title &&
                   Author == other.Author &&
                   ISBN == other.ISBN &&
                   PublicationYear == other.PublicationYear;
        }

        public override bool Equals(object? obj) => Equals(obj as Book);

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Author, ISBN, PublicationYear);
        }
    }
}
