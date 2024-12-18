namespace pupupu.Core;

// Це класс для сбора ошибок пр вадидации. У нас почти нет валидации, но я чот решила его добавить
// чтобы облегчить нам жизнь. Для операция типа создать, обновить, удалить в сервисах отлично подходит
// Вместо того, чтобы выбрасывать исключения, можно возвращать в контроллеры тексты ошибок и выводить пользователю
// Супер гуд, одним словом
public class Errors: Dictionary<string, List<string>>
{
    private readonly Dictionary<string, List<string>> _errors = new();

    public Errors() {}

    public void AddError(string fieldName, string message)
    {
        if (!_errors.ContainsKey(fieldName))
        {
            _errors.Add(fieldName, []);
        }

        if (!_errors[fieldName].Contains(message))
        {
            _errors[fieldName].Add(message);
        }
    }

    public void AddMainError(string message)
    {
        AddError(fieldName: string.Empty, message: message);
    }

    public Dictionary<string, List<string>> ErrorValues => _errors;

    public bool HasErrors => _errors.Any();
}