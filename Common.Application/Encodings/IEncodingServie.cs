namespace Common.Application.Encodings;

public interface IEncodingServie
{
    public byte[] Encoding(string body);

    public byte[] DeCode(string body);
}