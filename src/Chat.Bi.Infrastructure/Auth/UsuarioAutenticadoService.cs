using Chat.Bi.Core.Constantes.Claims;
using Chat.Bi.Core.Services;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Chat.Bi.Infrastructure.Auth;

public class UsuarioAutenticadoService(IHttpContextAccessor httpContextAccessor)
    : IUsuarioAutenticadoService
{
    int? _id;
    int? _idEmpresa;
    int? _idUsuarioAdmin;
    bool? _ehAdmin;
    bool? _ehMaster;

    public int ObterId()
    {
        if (_id.HasValue)
            return _id.Value;

        var idClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.Id)?.Value;

        if (!int.TryParse(idClaim, out var id) || id <= 0)
            throw new UnauthorizedAccessException("ID do usuário inválido.");

        _id = id;
        return _id.Value;
    }

    public int ObterIdEmpresa()
    {
        if (_idEmpresa.HasValue)
            return _idEmpresa.Value;

        var idClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.IdEmpresa)?.Value;

        _idEmpresa = int.TryParse(idClaim, out var id) ? id : 0;
        return _idEmpresa.Value;
    }

    public int ObterIdUsuarioAdmin()
    {
        if (_idUsuarioAdmin.HasValue)
            return _idUsuarioAdmin.Value;

        var httpContext = httpContextAccessor.HttpContext;
        var user = httpContext?.User;

        var idClaim = user?.Claims.FirstOrDefault(c => c.Type == TiposClaims.IdUsuarioAdmin)?.Value;

        if (int.TryParse(idClaim, out var id))
            _idUsuarioAdmin = id;
        else
            _idUsuarioAdmin = ObterId();

        return _idUsuarioAdmin.Value;
    }

    public bool EhMaster()
    {
        if (_ehMaster.HasValue)
            return _ehMaster.Value;

        var masterClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.Master)?.Value;

        _ehMaster = masterClaim == "1" ? true : false;
        return _ehMaster.Value;
    }

    public bool EhAdmin()
    {
        if (_ehAdmin.HasValue)
            return _ehAdmin.Value;

        var adminClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.Admin)?.Value;

        _ehAdmin = adminClaim == "1" ? true : false;
        return _ehAdmin.Value;
    }

    public string ObterNome()
    {
        return httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.Nome)?.Value;
    }

    public bool EhAutenticado()
    {
        return httpContextAccessor
            .HttpContext?
            .User?
            .Identity?
            .IsAuthenticated == true;
    }

    public string ObterEmail()
    {
        return httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?
            .Value;
    }
}