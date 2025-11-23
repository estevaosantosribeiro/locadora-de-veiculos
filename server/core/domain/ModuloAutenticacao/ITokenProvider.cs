namespace LocadoraDeVeiculos.Dominio.ModuloAutenticacao;

public interface ITokenProvider
{
    Task<IAccessToken> GerarTokenDeAcesso(Usuario usuario);
}