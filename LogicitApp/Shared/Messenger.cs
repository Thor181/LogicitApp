namespace LogicitApp.Shared
{
    public static class Messenger
    {
        public class Message
        {
            public Guid Id = Guid.NewGuid();
            public string Addresse { get; set; }
            public object MessageEntity { get; set; }
        }

        public static List<Message> Messages { get; set; } = new();

        public static List<Message> GetMessages(string addresse)
        {
            var messages = Messages.Where(x => x.Addresse.Equals(addresse)).ToList();

            foreach (var item in messages)
                Messages.Remove(item);

            return messages;
        }

        public static void AddMessage(string addresse, object messageEntity)
        {
            Messages.Add(new Message { Addresse = addresse, MessageEntity = messageEntity });
        }

    }
}
