using pupupu.Models.DAL;

namespace pupupu.Models.Bll;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Author(DAL.Author fromValue)
    {
        this.Id = fromValue.Id;

        this.Name = fromValue.Name;
    }
}