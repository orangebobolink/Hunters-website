using Shared.Messages.UserMessages;

namespace Shared.Messages.DataSeedMessages
{
    public class UserDataSeedMessage
    {
        public List<CreateUserMessage> Users { get; set; } = [];
    }
}
