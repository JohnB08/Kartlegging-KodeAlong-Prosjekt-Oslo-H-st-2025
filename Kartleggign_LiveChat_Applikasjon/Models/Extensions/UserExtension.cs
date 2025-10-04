using System.Collections.Immutable;
using System.Security.Cryptography;

namespace Kartleggign_LiveChat_Applikasjon.Models.Extensions;

public static class UserExtension
{
    public static bool Verify(this User user, string nickName, string password)
    {
        var bytes = password.Select(Convert.ToByte).ToArray();
        var hash = SHA256.HashData(bytes).ToImmutableArray();
        return user.NickName.Equals(nickName, StringComparison.OrdinalIgnoreCase) && user.Hash.SequenceEqual(hash);
    }
}