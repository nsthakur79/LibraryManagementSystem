namespace LibraryManagementSystem.Models
{
    /// <summary>
    /// Marker interface to identify entities that can be stored in the repository
    /// </summary>
    public interface IEntity 
    {
        int Id { get; set; }
    }
}