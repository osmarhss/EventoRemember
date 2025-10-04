using Bogus.DataSets;
using EventoRemember.Locais.Tests.Domain.Builders;

namespace EventoRemember.Locais.Tests.Domain.Tests
{
    public class ImagemVoTests
    {
        [Fact(DisplayName = "Adicionar uma imagem conforme os critérios de aceite deve passar")]
        public void AdicionarImagemValida_DevePassar() 
        {
            //Arrange & Act
            var imagemRes = new ImagemBuilder()
                .ComUrl(@"/images/local/2573672642/imagem1.jpg")
                .ComNomeArquivo("imagem1.jpg")
                .ComTamanhoBytes(2000)
                .Gerar();

            //Assert
            Assert.Equal(@"/images/local/2573672642/imagem1.jpg", imagemRes.Value!.Url);
            Assert.Equal("imagem1.jpg", imagemRes.Value!.NomeArquivo);
            Assert.Equal(2000, imagemRes.Value!.TamanhoBytes);
        }

        [Fact(DisplayName = "Adicionar mais de uma imagem conforme os critérios de aceite deve passar")]
        public void AdicionarMaisDeUmaImagemValidas_DevePassar() 
        {
            //Arrange & Act
            var imagensRes = new ImagemBuilder().ComQuantidade(5).GerarLista();

            //Assert
            Assert.Equal(5, imagensRes.Count);
            imagensRes.ForEach(i => Assert.True(i.IsSuccess));
        }

        [Fact(DisplayName = "Adicionar uma imagem com url nulo deve informar erro")]
        public void AdicionarImagemComUrlNuloVazio_DeveInformarErro() 
        {
            //Arrange & Act
            var imagemRes = new ImagemBuilder().ComUrl(null).Gerar();

            //Assert
            Assert.Contains("Caminho não pode ser nulo", imagemRes.Errors);
        }

        [Theory(DisplayName = "Adicionar imagem com nome do arquivo nulo ou vazio deve informar erro")]
        [InlineData(null)]
        [InlineData("")]
        public void AdicionarImagemComNomeArquivoNuloVazio_DeveInformarErro(string nomeInvalido) 
        {
            //Arrange & Act
            var imagemRes = new ImagemBuilder().ComNomeArquivo(nomeInvalido).Gerar();

            //Assert
            Assert.Contains("Caminho não pode ser nulo", imagemRes.Errors);
        }

        [Fact(DisplayName = "Adicionar imagem com tamanho em bytes inválido deve informar erro")]
        public void AdicionarImagemComTamanhoInvalido_DeveInformarErro() 
        {
            //Arrange & Act
            var imagemRes = new ImagemBuilder().ComNomeArquivo("juj.jpg").ComTamanhoBytes(300000).Gerar();

            //Assert
            Assert.Contains($"O arquivo de imagem 'juj.jpg' ultrapassa o tamanho máximo permitido de {30000/1000} MB", imagemRes.Errors);
        }
    }
}
