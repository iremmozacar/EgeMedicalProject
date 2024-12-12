namespace EgeApp.Backend.Entity.Abstract
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now; 
        public DateTime ModifiedDate { get; set; } = DateTime.Now; 
        public bool IsActive { get; set; } = true; 
        public string Name { get; set; } 
        public string Url { get; set; } 
        public string Description { get; set; } 
    }
}