namespace pupupu.Models.BLL;
using DAL = pupupu.Models.DAL;

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