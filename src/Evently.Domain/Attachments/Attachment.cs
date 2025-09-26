using Evently.Domain.Abstractions;

namespace Evently.Domain.Attachments;

public class Attachment : Entity
{
    public string Name { get; set; }

    public string ContentType { get; set; }

    public long Size { get; set; }

    public string Key { get; set; }
}
