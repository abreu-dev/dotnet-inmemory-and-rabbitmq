using Moq;
using Moq.AutoMock;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VeiculoMarcaCommands;
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
    public class VeiculoMarcaCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly VeiculoMarcaCommandHandler _veiculoMarcaCommandHandler;

        public VeiculoMarcaCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _veiculoMarcaCommandHandler = _autoMocker.CreateInstance<VeiculoMarcaCommandHandler>();
        }

        #region AddVeiculoMarcaCommand
        [Fact]
        public async Task Handle_AddVeiculoMarcaCommand_ShouldFailValidation_WhenEmptyNome()
        {
            // Arrange
            var command = new AddVeiculoMarcaCommand("");

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }
        [Fact]
        public async Task Handle_AddVeiculoMarcaCommand_ShouldFailValidation_WhenNomeAlreadyInUse()
        {
            // Arrange
            var command = new AddVeiculoMarcaCommand("PLA1234");

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
               .ReturnsAsync(new List<VeiculoMarca>() { new VeiculoMarca(command.Nome) });

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_AddVeiculoMarcaCommand_ShouldAddAndCommit_WhenValid(string Nome)
        {
            // Arrange
            var command = new AddVeiculoMarcaCommand(Nome);

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
                .ReturnsAsync(new List<VeiculoMarca>());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Verify(x => x.Add(It.IsAny<VeiculoMarca>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region UpdateVeiculoMarcaCommand
        [Fact]
        public async Task Handle_UpdateVeiculoMarcaCommand_ShouldFailValidation_WhenEmptyId()
        {
            // Arrange
            var command = new UpdateVeiculoMarcaCommand(Guid.Empty, "PLA1234");

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoMarcaCommand_ShouldFailValidation_WhenEmptyNome()
        {
            // Arrange
            var command = new UpdateVeiculoMarcaCommand(Guid.NewGuid(), "");

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoMarcaCommand_ShouldFailValidation_WhenVeiculoMarcaNotFound()
        {
            // Arrange
            var command = new UpdateVeiculoMarcaCommand(Guid.NewGuid(), "PLA1234");

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
                .ReturnsAsync(new List<VeiculoMarca>());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync((VeiculoMarca)null);

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoMarca").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoMarcaCommand_ShouldFailValidation_WhenNomeAlreadyInUse()
        {
            // Arrange
            var command = new UpdateVeiculoMarcaCommand(Guid.NewGuid(), "PLA1234");

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
                .ReturnsAsync(new List<VeiculoMarca>() { new VeiculoMarca(command.Nome) });

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoMarca(command.AggregateId, "PLA7946"));

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Nome").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_UpdateVeiculoMarcaCommand_ShouldUpdateAndCommit_WhenValid(string Nome)
        {
            // Arrange
            var command = new UpdateVeiculoMarcaCommand(Guid.NewGuid(), Nome);

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoMarca, bool>>>()))
                .ReturnsAsync(new List<VeiculoMarca>());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoMarca(command.AggregateId, "PLA7946"));

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Verify(x => x.Update(It.IsAny<VeiculoMarca>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region RemoveVeiculoMarcaCommand
        [Fact]
        public async Task Handle_RemoveVeiculoMarcaCommand_ShouldFailValidation_WhenEmptyId()
        {
            // Arrange
            var command = new RemoveVeiculoMarcaCommand(Guid.Empty);

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_RemoveVeiculoMarcaCommand_ShouldFailValidation_WhenVeiculoMarcaNotFound()
        {
            // Arrange
            var command = new RemoveVeiculoMarcaCommand(Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync((VeiculoMarca)null);

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoMarca").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_RemoveVeiculoMarcaCommand_ShouldRemoveAndCommit_WhenValid(string Nome)
        {
            // Arrange
            var command = new RemoveVeiculoMarcaCommand(Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new VeiculoMarca(command.AggregateId, "PLA7946"));

            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoMarcaCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoMarcaRepository>()
                .Verify(x => x.Remove(It.IsAny<VeiculoMarca>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion
    }
}
