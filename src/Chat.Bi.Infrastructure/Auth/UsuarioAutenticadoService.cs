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
            .FirstOrDefault(c => c.Type == TiposClaims.id)?.Value;

        _id = int.TryParse(idClaim, out var id) ? id : 0;
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
            .FirstOrDefault(c => c.Type == TiposClaims.id_empresa)?.Value;

        _idEmpresa = int.TryParse(idClaim, out var id) ? id : 0;
        return _idEmpresa.Value;
    }

    public int ObterIdUsuarioAdmin()
    {
        if (_idUsuarioAdmin.HasValue)
            return _idUsuarioAdmin.Value;

        var idClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.id_usuario_admin)?.Value;

        _id = int.TryParse(idClaim, out var id) ? id : 0;
        return _id.Value;
    }

    public bool EhMaster()
    {
        if (_ehMaster.HasValue)
            return _ehMaster.Value;

        var masterClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.master)?.Value;

        _ehMaster = masterClaim == "1" ? true : false;
        return _ehMaster.Value;
    }
  
    public bool EhAdmin()
    {
        if (_ehAdmin.HasValue)
            return _ehAdmin.Value;

        var masterClaim = httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.admin)?.Value;

        _ehAdmin = masterClaim == "1" ? true : false;
        return _ehAdmin.Value;
    }

    public string ObterNome()
    {
        return httpContextAccessor
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == TiposClaims.nome)?.Value;
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