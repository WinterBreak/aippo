using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

public static class SessionExtensions
{
    // Сохранить объект в сессию
    public static void Set<T>(this ISession session, string key, T value)
    {
        var json = JsonSerializer.Serialize(value); // Сериализация в JSON
        var bytes = Encoding.UTF8.GetBytes(json); // Преобразование JSON в byte[]
        session.Set(key, bytes); // Сохранение byte[] в сессии
    }

    // Получить объект из сессии
    public static T Get<T>(this ISession session, string key)
    {
        session.TryGetValue(key, out var bytes); // Получение byte[] из сессии
        if (bytes == null)
        {
            return default; // Возвращаем значение по умолчанию, если ключ не найден
        }
        var json = Encoding.UTF8.GetString(bytes); // Преобразование byte[] в JSON
        return JsonSerializer.Deserialize<T>(json); // Десериализация JSON в объект
    }
}