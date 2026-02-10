using EventoRemember.Locais.Domain.VOs;
using EventoRemember.Locais.Dominio.Enum;
using EventoRemember.Locais.Tests.Domain.Builders;
using EventoRemember.Locais.Tests.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventoRemember.Locais.Tests.Domain.Tests
{
    public class LocalTests
    {
        [Fact(DisplayName = "Adicionar um novo local conforme os critérios de aceite deve passar")]
        public void AdicionarNovoLocalConformeOsCriterios_DevePassar() 
        {
            //Arrange & Act
            var localRes = new LocalBuilder().Gerar();

            //Assert
            Assert.True(localRes.IsSuccess);
        }

        [Fact(DisplayName = "Ao atualizar o local com critérios de aceite deve passar")]
        public void AtualizarLocalComCriteriosDeAceite_DevePassar()
        {
            //Arrange
            var localRes = new LocalBuilder().Gerar();
            var novoNome = "Local Atualizado";
            var novaCapacidade = 10000;
            var novoTipo = TipoLocal.Arena;

            //Act
            var atualizacaoRes = localRes.Value!.AtualizarDados(novoNome, novaCapacidade, novoTipo);

            //Assert
            Assert.True(atualizacaoRes.IsSuccess);
            Assert.Equal(novoNome, localRes.Value!.Nome);
            Assert.Equal(novaCapacidade, localRes.Value!.CapacaidadeTotal);
            Assert.Equal(novoTipo, localRes.Value!.Tipo);
        }

        [Theory(DisplayName = "Adicionar um novo local sem imagens deve lançar erro")]
        [InlineData(true)]
        [InlineData(false)]
        public void AdicionarNovoLocalSemImagem_DeveLancarErro(bool listaVazia) 
        {
            //Arrange & Act
            var imagens = listaVazia ? new List<ImagemVo>() : null;

            var localRes = new LocalBuilder().ComImagens(imagens).Gerar();

            //Assert
            Assert.Contains("É necessário possuir ao menos uma foto do local", localRes.Errors);
        }

        [Theory(DisplayName = "Adicionar um local com nome nulo deve lançar erro")]
        [InlineData(null)]
        [InlineData("")]
        public void AdicionarNovoLocalComNomeNulo_DeveLancarErro(string nome) 
        {
            //Arrange & Act
            var localRes = new LocalBuilder().ComNome(nome).Gerar();

            //Assert
            Assert.Contains("O nome do local não pode ser nulo ou vazio", localRes.Errors);
        }

        [Fact(DisplayName = "Adicionar um local com nome de tamanho inválido deve lançar erro")]
        public void AdicionarNovoLocalComNomeTamanhoInvalido_DeveLancarErro() 
        {
            //Arrange & Act
            var nomeInvalido = "ja";
            var localRes = new LocalBuilder().ComNome(nomeInvalido).Gerar();

            //Assert
            Assert.Contains("O nome do local deve possuir entre 4 a 20 caracteres", localRes.Errors);
        }

        [Theory(DisplayName = "Adicionar um local com descrição nula ou vazia deve lançar erro")]
        [InlineData(null)]
        [InlineData("")]
        public void AdicionarLocalComDescricaoNulaOuVazia_DeveLancarErro(string descricao)
        {
            //Arrange & Act
            var localRes = new LocalBuilder().ComDescricao(descricao).Gerar();

            //Assert
            Assert.Contains("A descrição do local não pode ser nula ou vazia", localRes.Errors);
        }

        [Fact(DisplayName = "Adicionar um local com descrição inválida deve lançar erro")]
        public void AdicionarLocalComDescricaoInvalida_DeveLancarErro() 
        {
            //Arrange & Act
            var descricaoInvalidaCurta = new Random().Next(1, 14).ToString();
            var descricaoInvalidaLonga = new string('a', 501);

            var localRes1 = new LocalBuilder().ComDescricao(descricaoInvalidaCurta).Gerar();
            var localRes2 = new LocalBuilder().ComDescricao(descricaoInvalidaLonga).Gerar();

            //Assert
            Assert.Contains("A descrição precisa conter entre 15 a 500 caracteres", localRes1.Errors);
            Assert.Contains("A descrição precisa conter entre 15 a 500 caracteres", localRes2.Errors);
        }

        [Fact(DisplayName = "Adicionar local com capacidade inválida deve lançar erro")]
        public void AdicionarLocalComCapacidadeInvalida_DeveLancarErro() 
        {
            //Arrange & Act
            var capacidadeInvalidaBaixa = 10;
            var capacidadeInvalidaAlta = 600000;

            var localRes1 = new LocalBuilder().ComCapacidadeTotal(capacidadeInvalidaBaixa).Gerar();
            var localRes2 = new LocalBuilder().ComCapacidadeTotal(capacidadeInvalidaAlta).Gerar();

            //Assert
            Assert.Contains("O local precisa ter capacidade entre 20 e 500000 pessoas", localRes1.Errors);
            Assert.Contains("O local precisa ter capacidade entre 20 e 500000 pessoas", localRes2.Errors);
        }

        [Fact(DisplayName = "Ao adicionar uma imagem repetida a um local deve lançar erro")]
        public void AdicionarImagemRepetidaAoLocal_DeveLancarErro() 
         {
            //Arrange
            var imagemRepetida = new ImagemBuilder().Gerar().Value;

            var localRes = new LocalBuilder()
                .ComImagens(new List<ImagemVo> { imagemRepetida })
                .Gerar();

            //Act
            var atualizacaoRes = localRes.Value!.AdicionarNovasImagens(new List<ImagemVo> { imagemRepetida });

            //Assert
            Assert.Contains("Há uma imagem com o mesmo nome em nossos registros", atualizacaoRes.Errors);
        }

        [Fact(DisplayName = "Ao adicionar imagens ultrapassando o limite de imagens deve lançar erro")]
        public void AdicionarImagensUltrapassandoLimite_DeveLancarErro() 
        {
            //Arrange
            var imagensNovas = new ImagemBuilder().ComQuantidade(10).GerarLista();

            var localRes = new LocalBuilder().Gerar();

            //Act
            var atualizacaoRes = localRes.Value!.AdicionarNovasImagens(imagensNovas.Select(i => i.Value!));

            //Assert
            Assert.Contains("O máximo de fotos permitido é de 10.", atualizacaoRes.Errors);
        }

        [Fact(DisplayName = "Ao adicionar um local com número de setores inválidos deve lançar erro")]
        public void AdicionarLocalComNumeroDeSetoresInvalido_DeveLancarErro() 
        {
            //Arrange & Act
            var setoresRes = new SetorBuilder().ComQuantidadeParaLista(26).GerarLista().Select(s => s.Value!);
            var localRes = new LocalBuilder().ComSetores(setoresRes).Gerar();

            //Assert
            Assert.Contains("O número máximo de setores permitido é 25.", localRes.Errors);
        }

        [Fact(DisplayName = "Ao adicionar um local com tamanho máximo menor que a soma da capacidade dos setores deve lançar erro")]
        public void AdicionarLocalComCapacidadeMenorQueCapacidadeDaSomaDosSetores_DeveLancarErro() 
        {
            //Arrange & Act
            var setoresRes = new SetorBuilder().ComQuantidadeParaLista(3).GerarLista();

            var localRes = new LocalBuilder().ComSetores(setoresRes.Select(s => s.Value!)).ComCapacidadeTotal(setoresRes.Sum(s => s.Value!.Capacidade) - 1).Gerar();

            //Assert
            Assert.Contains("A soma da capacidade dos setores é maior que a capacidade máxima do local", localRes.Errors);
        }

    }
}
