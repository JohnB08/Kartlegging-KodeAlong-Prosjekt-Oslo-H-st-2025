using System.Collections.Immutable;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace Kartleggign_LiveChat_Applikasjon.Models;

public record User(string NickName, ImmutableArray<byte> Hash, bool Online = false)
{
    public static User New(string nickName, string password)
    {
        var bytes = password.Select(Convert.ToByte).ToArray();
        var hash = SHA256.HashData(bytes).ToImmutableArray();
        return new User(nickName, hash);
    }
};
