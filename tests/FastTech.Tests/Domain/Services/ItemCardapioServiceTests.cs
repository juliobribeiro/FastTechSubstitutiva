using AutoMapper;
using FastTech.Api.Controllers;
using FastTech.Application.DataTransferObjects;
using FastTech.Application.Interfaces;
using FastTech.Application.Mappings;
using FastTech.Application.Services;
using FastTech.Domain.Interfaces;
using FastTech.Domain.Interfaces.Infrastructure;
using FastTech.Domain.Services;
using FastTech.Infrastructure.Data;
using FastTech.Infrastructure.Data.Repositories;
using FastTech.Tests.Shared.Fixtures.DataTransferObjects;
using FastTech.Tests.Shared.Fixtures.Utils;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using EN = FastTech.Domain.Entities;

namespace FastTech.Tests.Domain.Services
{
    public class ItemCardapioControllerIntegrationTests : IAsyncLifetime
    {
        private readonly ApplicationDBContext _context;
        private readonly IItemCardapioRepository _ItemCardapioRepository;
        private readonly IItemCardapioService _ItemCardapioService;
        private readonly ItemCardapioController _controller;
        private readonly IItemCardapioApplicationService _ItemCardapioApplicationService;
        private readonly IMapper _mapper;

        private readonly EN.UserData _userData;

        public ItemCardapioControllerIntegrationTests()
        {
            // Configura o DbContext em memória
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDBContext(options);
            // Configura o AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ItemCardapioMapper>();
            });
            _mapper = config.CreateMapper();

            // Configura as dependências
            _userData = UserDataFixtures.CreateAs_Base();
            _ItemCardapioRepository = new ItemCardapioRepository(_context);
            _ItemCardapioService = new ItemCardapioService(_ItemCardapioRepository, _userData);
           
            _ItemCardapioApplicationService = new ItemCardapioApplicationService(_ItemCardapioService, _mapper);
            var logger = NullLogger<ItemCardapioController>.Instance;
            // Instancia o ItemCardapioController diretamente
            _controller = new ItemCardapioController(logger, _ItemCardapioApplicationService);
        }

        public async Task DisposeAsync()
        {
            await _context.Database.EnsureDeletedAsync();
            _context.Dispose();
        }

        //[Fact]
        //public async Task ShouldInsertItemCardapio()
        //{
        //    // Arrange
        //    var ItemCardapio = BasicItemCardapioFixtures.CreateAs_Base();
        //    // Act
        //    var result = await _controller.Create(ItemCardapio);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnedItemCardapio = Assert.IsType<BasicItemCardapio>(okResult.Value);
        //    Assert.NotNull(returnedItemCardapio);
        //    Assert.Equal(ItemCardapio.Nome, returnedItemCardapio.Nome);
        //    Assert.Equal(ItemCardapio.Descricao, returnedItemCardapio.Descricao);
        //    Assert.Equal(ItemCardapio.Preco, returnedItemCardapio.Preco);
        //    Assert.Equal(ItemCardapio.Disponivel, returnedItemCardapio.Disponivel);
        //}

        //[Fact]
        //public async Task ShouldUpdateItemCardapio()
        //{
        //    // Arrange
        //    await ShouldInsertItemCardapio();
        //    var ItemCardapios = _ItemCardapioService.GetAll();
        //    var ItemCardapio = _mapper.Map<ItemCardapio>(ItemCardapios.FirstOrDefault());

        //    Assert.NotNull(ItemCardapio); // Garante que existe um contato para atualizar

        //    // Atualiza o contato
        //    ItemCardapio.Nome = "Updated Name";

        //    // Act
        //    var result = await _controller.Update(ItemCardapio);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var updatedItemCardapio = Assert.IsType<ItemCardapio>(okResult.Value);
        //    Assert.NotNull(updatedItemCardapio);
        //    Assert.Equal("Updated Name", updatedItemCardapio.Nome);
        //}

        public async Task InitializeAsync()
        {
            // Limpa o banco de dados e cria um novo
            await _context.Database.EnsureDeletedAsync();

            // Carrega os dados iniciais (seeds)

            await _context.SaveChangesAsync();
        }
    }
}