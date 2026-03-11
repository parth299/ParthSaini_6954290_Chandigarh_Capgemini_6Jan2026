using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("tblbooks")]
public class BookModel {
    [Key]
    public int BookId{get; set;}

    public string BookName{get; set;}
    public string AuthorName{get; set;}
    public int BookPrice{get; set;}
}