using EventoRemember.BuildingBlocks.Domain.Exceptions;
using EventoRemember.Locais.Domain.Enum;
using EventoRemember.Locais.Domain.VOs;
using EventoRemember.Locais.Tests.Domain.Builders;
using EventoRemember.Locais.Tests.Domain.Entities;
using NuGet.Frameworks;

namespace EventoRemember.Locais.Tests.Domain.Tests
{
    public class SetorTests
    {
        [Fact(DisplayName = "Deve criar um setor sem filas com os critérios de aceite")]
        public void CriarSetorSemFilas_DevePassar()
        {
            //Arrange & Act
            var setorRes = new SetorBuilder().ComNome("Laranja").ComCapacidade(100).Gerar();

            //Assert
            Assert.True(setorRes.IsSuccess);
            Assert.Equal("Laranja", setorRes.Value!.Nome);
            Assert.Equal(100, setorRes.Value.Capacidade);
            Assert.Equal("EmAnalise", setorRes.Value.Situacao.ToString());
            Assert.True(!setorRes.Value.Ativo);
            Assert.Empty(setorRes.Value.Filas);
        }

        [Fact(DisplayName = "Deve criar um setor com filas com os critérios de aceite")]
        public void CriarSetorComFilas_DevePassar()
        {
            //Arrange & Act
            var filasRes = new FilaBuilder().ComNumeroDeFilas(10).GerarLista();

            var filas = filasRes.Select(f => f.Value!).ToList();

            var setorRes = new SetorBuilder().ComCapacidade(filasRes.Sum(f => f.Value!.QtdCadeiras)).ComFilas(filas).Gerar();

            //Assert
            Assert.True(setorRes.IsSuccess);
            Assert.Equal(10, setorRes.Value!.Filas.Count);
        }

        [Theory(DisplayName = "Criar um Setor com nome estando nulo ou vazio deve gerar lista de erros")]
        [InlineData(null)]
        [InlineData("")]
        public void CriarSetorComNomeNuloOuVazio_DeveLancarErro(string nomeInvalido)
        {
            //Arrange & Act
            var setorRes = new SetorBuilder().ComNome(nomeInvalido).Gerar();

            //Assert
            Assert.False(setorRes.IsSuccess);
            Assert.Contains("O nome do setor não pode ser vazio ou nulo", setorRes.Errors);
        }

        [Fact(DisplayName = "Criar um setor com nomeInvalido inválido pelo tamanho deve gerar lista de erros")]
        public void CriarSetorComNomeInvalidoPeloTamanho()
        {
            //Arrange & Act
            var setorRes = new SetorBuilder().ComNome("AGWE3KDFJJHWEFASDASDJHEFJJDFJ").Gerar();

            //Assert
            Assert.False(setorRes.IsSuccess);
            Assert.Contains("O nome do setor precisa ter entre 1 a 25 caracteres", setorRes.Errors);
        }

        [Fact(DisplayName = "Criar um Setor com filas sem respeitar a capacidade máxima deve gerar lista de erros")]
        public void CriarSetorComFilasUltrapassandoCapacidadeMaxima_DeveLancarErro()
        {
            //Arrange
            var filasRes = new FilaBuilder().ComNumeroDeFilas(5).GerarLista();

            var filas = filasRes.Select(f => f.Value!);
            var capacidadeInvalida = filas.Sum(f => f.QtdCadeiras) + 20000; // Adicionado 20000 ao valor para lançar um erro, pois gerar 20001 filas com 1 cadeira que é o mínimo seria custoso.

            //Act
            var setorRes = new SetorBuilder().ComCapacidade(capacidadeInvalida).ComFilas(filas).Gerar();

            //Assert
            Assert.False(setorRes.IsSuccess);
            Assert.Contains("A capacidade do setor não pode ser menor que 1 e maior que 20.000", setorRes.Errors);
        }

        [Theory(DisplayName = "Criar um setor com valores mínimos e máximos não respeitados deve gerar lista de erros")]
        [InlineData(0)]
        [InlineData(20001)]
        public void CriarSetorComCapacidadesInvalidas_DeveLancarErro(int quantidade)
        {
            //Arrange & Act
            var setorRes = new SetorBuilder().ComCapacidade(quantidade).Gerar();

            //Assert
            Assert.False(setorRes.IsSuccess);
            Assert.Contains("A capacidade do setor não pode ser menor que 1 e maior que 20.000", setorRes.Errors);
        }

        [Fact(DisplayName = "Deve atualizar um setor sem filas com os critérios de aceite")]
        public void AtualizarSetorSemFilas_DevePassar()
        {
            //Arrange
            var antigoRes = new SetorBuilder().Gerar();
            var novoRes = new SetorBuilder().ComNome("Amarelo").ComCapacidade(10000).Gerar();

            //Act
            var resAtt = antigoRes.Value!.AtualizarDados(novoRes.Value!.Nome, novoRes.Value!.Capacidade);

            //Assert
            Assert.True(resAtt.IsSuccess);
            Assert.Equal("Amarelo", antigoRes.Value!.Nome);
            Assert.Equal(10000, antigoRes.Value!.Capacidade);
        }

        [Fact(DisplayName = "Deve atualizar o setor com filas com os critérios de aceite")]
        public void AtualizarSetorComFilas_DevePassar()
        {
            //Arrange
            var filasAntigasRes = new FilaBuilder().ComNumeroDeFilas(10).GerarLista();
            var filasAntigas = filasAntigasRes.Select(f => f.Value!).ToList();

            var antigoRes = new SetorBuilder()
                .ComNome("Arquibancada Superior")
                .ComCapacidade(filasAntigas.Sum(f => f.QtdCadeiras))
                .ComFilas(filasAntigas)
                .Gerar();

            var filasNovasRes = new FilaBuilder().ComNumeroDeFilas(5).GerarLista();
            var filasNovas = filasNovasRes.Select(f => f.Value!).ToList();

            var novaRes = new SetorBuilder()
                .ComNome("Arquibancada Superior Dir")
                .ComCapacidade(filasNovas.Sum(f => f.QtdCadeiras))
                .ComFilas(filasNovas)
                .Gerar();

            //Act
            var res1 = antigoRes.Value!.AtualizarDados(novaRes.Value!.Nome, novaRes.Value.Capacidade);
            var res2 = antigoRes.Value!.GerirFilas(filasNovas);

            //Assert
            Assert.True(res1.IsSuccess);
            Assert.True(res2.IsSuccess);
            Assert.Equal("Arquibancada Superior Dir", antigoRes.Value!.Nome);
            Assert.Equal(novaRes.Value.Capacidade, antigoRes.Value.Capacidade);
            Assert.Equal(novaRes.Value.Filas.Count, antigoRes.Value.Filas.Count);
            Assert.Equal(novaRes.Value.Filas.Sum(f => f.QtdCadeiras), antigoRes.Value.Filas.Sum(f => f.QtdCadeiras));
        }

        [Theory(DisplayName = "Atualizar o setor com nome nulo ou vazio deve gerar lista de erros")]
        [InlineData(null)]
        [InlineData("")]
        public void AtualizarSetorComNomeNuloOuVazio_DeveLancarErro(string nomeInvalido)
        {
            //Arrange
            var setorRes = new SetorBuilder().ComNome("ABC").Gerar();

            //Act
            var resAtt = setorRes.Value!.AtualizarDados(nomeInvalido, setorRes.Value.Capacidade);

            //Assert
            Assert.False(resAtt.IsSuccess);
            Assert.Contains("O nome do setor não pode ser vazio ou nulo", resAtt.Errors);
        }

        [Fact(DisplayName = "Atualizar o setor com nome inválido por tamanho deve gerar lista de erros")]
        public void AtualizarSetorComNomeInvalidoPeloTamano_DeveLancarErro() 
        {
            //Arrange
            var setorRes = new SetorBuilder().Gerar();

            //Act
            var resAtt = setorRes.Value!.AtualizarDados("iuehguierhgiurhgiuhreiughiueghuierhgiueguirhgih", setorRes.Value.Capacidade);

            //Assert
            Assert.False(resAtt.IsSuccess);
            Assert.Contains("O nome do setor precisa ter entre 1 a 25 caracteres", resAtt.Errors);
        }

        [Theory(DisplayName = "Atualizar o setor com valores mínimos e máximos não respeitados, deve gerar lista de erros")]
        [InlineData(0)]
        [InlineData(20001)]
        public void AtualizarSetorComCapacidadeInvalida_DeveLancarErro(int capacidadeInvalida)
        {
            //Arrange 
            var setorRes = new SetorBuilder().ComCapacidade(10000).Gerar();

            //Act
            var resAtt = setorRes.Value!.AtualizarDados(setorRes.Value.Nome, capacidadeInvalida);

            //Assert
            Assert.False(resAtt.IsSuccess);
            Assert.Contains("A capacidade do setor não pode ser menor que 1 e maior que 20.000", resAtt.Errors);
        }

        [Fact(DisplayName = "O Setor deve ficar ativo quando o Status dele for alterado para Aprovado")]
        public void AtualizarSituacaoParaAprovadoOhSetorFicaAtivo_DevePassar()
        {
            //Arrange
            var setorRes = new SetorBuilder().Gerar();

            //Act
            setorRes.Value!.AtualizarSituacaoPeloManager(SetorSituacao.Aprovado);

            //Assert
            Assert.True(setorRes.IsSuccess);
            Assert.True(setorRes.Value.Ativo);
        }

        [Fact(DisplayName = "O setor não pode ser definido como Indisponível por Managers deve gerar lista de erros")]
        public void AtualizarSituacaoParaIndisponivelPeloManager_DeveLancarErro()
        {
            //Arrange
            var setorRes = new SetorBuilder().Gerar();

            //Act & Assert
            var excecao = Assert.Throws<DomainException>(() => setorRes.Value!.AtualizarSituacaoPeloManager(SetorSituacao.Indisponivel));
            Assert.Equal("Somente o usuário pode definir como indisponível", excecao.Message);
        }

        [Fact(DisplayName = "O setor não pode ser definido para uma situação que não existe, deve gerar lista de erros.")]
        public void AtualizarSituacaoParaOpcoesDeManagerPeloUsuario_DeveLancarErro()
        {
            //Arrange
            var setorRes = new SetorBuilder().Gerar();

            //Act & Assert
            var excecao = Assert.Throws<DomainException>(() => setorRes.Value!.AtualizarSituacaoPeloManager((SetorSituacao)999));
            Assert.Equal("Entrada inválida", excecao.Message);
        }
    }
}
