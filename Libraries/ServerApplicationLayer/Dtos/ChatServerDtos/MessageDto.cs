namespace ServerApplicationLayer.Dtos.ChatServerDtos;
public record MessageDto ( string Message, string From, string To, DateTime SentAt, bool IsRead );

