using DAL = pupupu.Dal.Models;

namespace pupupu.Bll.Models;

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