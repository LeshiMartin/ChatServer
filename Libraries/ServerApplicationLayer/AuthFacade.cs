using ServerApplicationLayer.Dtos.AuthDtos;

namespace ServerApplicationLayer;
public interface AuthFacade
{
  Task RegisterAsync ( RegisterDto dto, CancellationToken cancellationToken = default );
  Task<IdentityResponse> LoginAsync ( LoginDto dto, CancellationToken cancellationToken = default );
}
