using Moq;
using Moq.AutoMock;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Supply.Domain.Tests.CommandHandlers
{
    public class VeiculoModeloCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly VeiculoModeloCommandHandler _veiculoModeloCommandHandler;

        public VeiculoModeloCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _veiculoModeloCommandHandler = _autoMocker.CreateInstance<VeiculoModeloCommandHandler>();
        }

        #region AddVeiculoModeloCommand
        [Fact]
        public async Task Handle_AddVeiculoModeloCommand_ShouldFailValidation_WhenEmptyNome()
        {
            // Arrange
            var command = new AddVeiculoModeloCommand("", Guid.NewGuid());

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoModeloCommand_ShouldFailValidation_WhenEmptyVeiculoMarcaId()
        {
            // Arrange
            var command = new AddVeiculoModeloCommand("Modelo", Guid.Empty);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("VeiculoMarcaId").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoModeloCommand_ShouldFailValidation_WhenNomeAlreadyInUse()
        {
            // Arrange
            var command = new AddVeiculoModeloCommand("Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
               .ReturnsAsync(new List<VeiculoModelo>() { new VeiculoModelo(command.Nome, Guid.NewGuid()) });

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoModeloCommand_ShouldFailValidation_WhenVeiculoMarcaIdNotFound()
        {
            // Arrange
            var command = new AddVeiculoModeloCommand("Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
                .ReturnsAsync(new List<VeiculoModelo>());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
               .ReturnsAsync(new List<VeiculoMarca>());

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoMarcaId").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoModeloCommand_ShouldAddAndCommit_WhenValid()
        {
            // Arrange
            var command = new AddVeiculoModeloCommand("Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
                .ReturnsAsync(new List<VeiculoModelo>());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
               .ReturnsAsync(new List<VeiculoMarca>() { new VeiculoMarca(command.VeiculoMarcaId, "Marca") });

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Verify(x => x.Add(It.IsAny<VeiculoModelo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region UpdateVeiculoModeloCommand
        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldFailValidation_WhenEmptyId()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.Empty, "Modelo", Guid.NewGuid());

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldFailValidation_WhenEmptyNome()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.NewGuid(), "", Guid.NewGuid());

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldFailValidation_WhenEmptyVeiculoMarcaId()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.NewGuid(), "Modelo", Guid.Empty);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("VeiculoMarcaId").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldFailValidation_WhenVeiculoModeloNotFound()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.NewGuid(), "Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
                .ReturnsAsync(new List<VeiculoModelo>());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync((VeiculoModelo)null);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoModelo").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldFailValidation_WhenNomeAlreadyInUse()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.NewGuid(), "Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
                .ReturnsAsync(new List<VeiculoModelo>() { new VeiculoModelo(command.Nome, Guid.NewGuid()) });

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoModelo(command.AggregateId, "Outro Modelo", Guid.NewGuid()));

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldFailValidation_WhenVeiculoMarcaIdNotFound()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.NewGuid(), "Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
                .ReturnsAsync(new List<VeiculoModelo>());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoModelo(command.AggregateId, "Outro Modelo", Guid.NewGuid()));

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
               .ReturnsAsync(new List<VeiculoMarca>());

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoMarcaId").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoModeloCommand_ShouldUpdateAndCommit_WhenValid()
        {
            // Arrange
            var command = new UpdateVeiculoModeloCommand(Guid.NewGuid(), "Modelo", Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
                .ReturnsAsync(new List<VeiculoModelo>());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoModelo(command.AggregateId, "Outro Modelo", Guid.NewGuid()));

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
               .ReturnsAsync(new List<VeiculoMarca>() { new VeiculoMarca(command.VeiculoMarcaId, "Marca") });

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Verify(x => x.Update(It.IsAny<VeiculoModelo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region RemoveVeiculoModeloCommand
        [Fact]
        public async Task Handle_RemoveVeiculoModeloCommand_ShouldFailValidation_WhenEmptyId()
        {
            // Arrange
            var command = new RemoveVeiculoModeloCommand(Guid.Empty);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_RemoveVeiculoModeloCommand_ShouldFailValidation_WhenVeiculoModeloNotFound()
        {
            // Arrange
            var command = new RemoveVeiculoModeloCommand(Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetByIdWithIncludes(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync((VeiculoModelo)null);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoModelo").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_RemoveVeiculoModeloCommand_ShouldFailValidation_WhenInUseByVeiculo()
        {
            // Arrange
            var command = new RemoveVeiculoModeloCommand(Guid.NewGuid());

            var modelo = new VeiculoModelo(command.AggregateId, "Marca", Guid.NewGuid());
            modelo.Veiculos.Add(new Veiculo("PLA1234", modelo.Id));
            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetByIdWithIncludes(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(modelo);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InUseByAnotherEntity.Format("VeiculoModelo", "Veiculos").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_RemoveVeiculoModeloCommand_ShouldRemoveAndCommit_WhenValid()
        {
            // Arrange
            var command = new RemoveVeiculoModeloCommand(Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.GetByIdWithIncludes(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoModelo(command.AggregateId, "Modelo", Guid.NewGuid()));

            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoModeloCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoModeloRepository>()
                .Verify(x => x.Remove(It.IsAny<VeiculoModelo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion
    }
}
