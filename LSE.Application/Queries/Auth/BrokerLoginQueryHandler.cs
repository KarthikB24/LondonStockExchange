using LSE.Application.Abstraction.Broker;
using LSE.Application.Abstraction.Security;
using LSE.Application.Queries.Auth;
using LSE.Application.DTOs.BrokerAuth;
using MediatR;

public class LoginQueryHandler : IRequestHandler<BrokerLoginQuery, BrokerLoginResponseDto?>
{
    private readonly IBrokerAuthRepository _repo;
    private readonly IJwtTokenService _jwt;

    public LoginQueryHandler(IBrokerAuthRepository repo, IJwtTokenService jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    public async Task<BrokerLoginResponseDto?> Handle(BrokerLoginQuery request, CancellationToken token)
    {
        var validated = await _repo.ValidateLoginAsync(request.UserName, request.Password, token);
        if (validated == null)
            return null;  // or throw UnauthorizedAccessException

        var (tokenString, expiresAt) = _jwt.GenerateToken(new Dictionary<string, string>
        {
            { "username", validated.Username },
            { "brokerCode", validated.BrokerCode }
        });

        return new BrokerLoginResponseDto
        {
            Username = validated.Username,
            BrokerCode = validated.BrokerCode,
            JwtToken = tokenString
        };
    }
}
