using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListToDo.Model
{
    [Table("TodoItem")]

    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool IsCompleted { get; set; }  
        
        public TodoItem()
        {
            this.Description = string.Empty;
            this.CreatedDate = DateTime.Now;
            this.Title = string.Empty;
        }
    }
}
