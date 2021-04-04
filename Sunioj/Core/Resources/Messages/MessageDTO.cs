using System;

namespace Sunioj.Core.Resources.Messages
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public int? ServicePackageId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
