namespace Office_supplies_management.Models
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
