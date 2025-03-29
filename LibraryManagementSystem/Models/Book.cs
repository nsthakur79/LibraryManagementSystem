namespace LibraryManagementSystem.Models
{
    public class Book
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
    }
}
