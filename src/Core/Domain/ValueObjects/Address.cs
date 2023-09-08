namespace Domain.ValueObjects;

public record Address
{
    private readonly Uri _value;

    public Address(Uri address)
    {
        _value = address;
    }

    public Address(string address)
    {
        address = address.Trim();
        ArgumentException.ThrowIfNullOrEmpty(address);

        _value = Uri.TryCreate(address, UriKind.Absolute, out var uri)
            ? uri
            : throw new ArgumentException("Address must be a valid Uri");
    }

    public static explicit operator Address(Uri pictureUrl) => new(pictureUrl);
    public static explicit operator Address(string pictureUrl) => new(pictureUrl);
    public static implicit operator string(Address address) => address._value.ToString();
    public static Address Undefined => new("http://undefined");

    public override string ToString() => _value.ToString();
}