using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.DTOs;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao.Services;

public class JwtProvider : ITokenProvider
{
    private readonly UserManager<Usuario> userManager;
    private readonly string? chaveJwt;
    private readonly DateTime dataExpiracaoJwt;
    private string? audienciaValida;

    public JwtProvider(IConfiguration config, UserManager<Usuario> userManager)
    {
        this.userManager = userManager;

        chaveJwt = config["JWT_GENERATION_KEY"];

        if (string.IsNullOrEmpty(chaveJwt))
            throw new ArgumentException("Chave de geração de tokens não configurada");

        audienciaValida = config["JWT_AUDIENCE_DOMAIN"];

        if (string.IsNullOrEmpty(audienciaValida))
            throw new ArgumentException("Audiência válida para transmissão de tokens não configurada");

        dataExpiracaoJwt = DateTime.UtcNow.AddMinutes(5);
    }

    public async Task<IAccessToken> GerarTokenDeAcesso(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var chaveEmBytes = Encoding.ASCII.GetBytes(chaveJwt!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "OrganizaMed",
            Audience = audienciaValida,
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email!),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.UserName!)
            }),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(chaveEmBytes),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Expires = dataExpiracaoJwt,
            NotBefore = DateTime.UtcNow
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        var roles = await userManager.GetRolesAsync(usuario);
        var tipoUsuario = roles.FirstOrDefault() ?? "User";

        return new TokenResponse()
        {
            Chave = tokenString,
            DataExpiracao = dataExpiracaoJwt,
            Usuario = new UsuarioAutenticadoDto
            {
                Id = usuario.Id,
                UserName = usuario.UserName!,
                Email = usuario.Email!,
                TipoUsuario = tipoUsuario
            }
        };
    }
}