using DAL = pupupu.Dal.Models;

namespace pupupu.Bll.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; }

    public Book(DAL.Book fromValue)
    {
        this.Id = fromValue.Id;

        this.Title = fromValue.Title;

        this.Description = fromValue.Description;

        this.AuthorId = fromValue.AuthorId;

        this.Author = new Author(fromValue.Author);
    }
}