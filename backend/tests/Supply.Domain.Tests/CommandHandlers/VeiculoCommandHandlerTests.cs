using Moq;
using Moq.AutoMock;
using Supply.Domain.CommandHandlers;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Data;
using Supply.Domain.Core.Domain;
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
    public class VeiculoCommandHandlerTests
    {
        private readonly AutoMocker _autoMocker;
        private readonly VeiculoCommandHandler _veiculoCommandHandler;

        public VeiculoCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _veiculoCommandHandler = _autoMocker.CreateInstance<VeiculoCommandHandler>();
        }

        #region AddVeiculoCommand
        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenEmptyPlaca()
        {
            // Arrange
            var command = new AddVeiculoCommand("", DateTime.Now, 100.25, Guid.NewGuid());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenInvalidPlaca()
        {
            // Arrange
            var command = new AddVeiculoCommand("ABCDEFG", DateTime.Now, 100.25, Guid.NewGuid());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InvalidFormat.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenPlacaAlreadyInUse()
        {
            // Arrange
            var command = new AddVeiculoCommand("PLA1234", DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
               .ReturnsAsync(new List<Veiculo>() { new Veiculo(command.Placa, DateTime.Now, 100.25, Guid.NewGuid()) });

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_AddVeiculoCommand_ShouldFailValidation_WhenVeiculoModeloIdNotFound()
        {
            // Arrange
            var command = new AddVeiculoCommand("PLA1234", DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
               .ReturnsAsync(new List<Veiculo>());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
               .ReturnsAsync(new List<VeiculoModelo>());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoModeloId").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_AddVeiculoCommand_ShouldAddAndCommit_WhenValid(string placa)
        {
            // Arrange
            var command = new AddVeiculoCommand(placa, DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
                .ReturnsAsync(new List<Veiculo>());

            _autoMocker.GetMock<IVeiculoModeloRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
               .ReturnsAsync(new List<VeiculoModelo>() { new VeiculoModelo(command.VeiculoModeloId, "Modelo", Guid.NewGuid()) });

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoRepository>()
                .Verify(x => x.Add(It.IsAny<Veiculo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region UpdateVeiculoCommand
        [Fact]
        public async Task Handle_UpdateVeiculoCommand_ShouldFailValidation_WhenEmptyId()
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.Empty, "PLA1234", DateTime.Now, 100.25, Guid.NewGuid());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoCommand_ShouldFailValidation_WhenEmptyPlaca()
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.NewGuid(), "", DateTime.Now, 100.25, Guid.NewGuid());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoCommand_ShouldFailValidation_WhenInvalidPlaca()
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.NewGuid(), "ABCDEFG", DateTime.Now, 100.25, Guid.NewGuid());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.InvalidFormat.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoCommand_ShouldFailValidation_WhenVeiculoNotFound()
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.NewGuid(), "PLA1234", DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
                .ReturnsAsync(new List<Veiculo>());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync((Veiculo)null);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("Veiculo").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoCommand_ShouldFailValidation_WhenPlacaAlreadyInUse()
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.NewGuid(), "PLA1234", DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
                .ReturnsAsync(new List<Veiculo>() { new Veiculo(command.Placa, DateTime.Now, 100.25, Guid.NewGuid()) });

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new Veiculo(command.AggregateId, "PLA7946", DateTime.Now, 100.25, Guid.NewGuid()));

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.AlreadyInUse.Format("Placa").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_UpdateVeiculoCommand_ShouldFailValidation_WhenVeiculoModeloIdNotFound()
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.NewGuid(), "PLA1234", DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
                .ReturnsAsync(new List<Veiculo>());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new Veiculo(command.AggregateId, "PLA7946", DateTime.Now, 100.25, Guid.NewGuid()));

            _autoMocker.GetMock<IVeiculoModeloRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
               .ReturnsAsync(new List<VeiculoModelo>());

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("VeiculoModeloId").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Theory]
        [InlineData("ABC1D23")]
        [InlineData("ABC1234")]
        public async Task Handle_UpdateVeiculoCommand_ShouldUpdateAndCommit_WhenValid(string placa)
        {
            // Arrange
            var command = new UpdateVeiculoCommand(Guid.NewGuid(), placa, DateTime.Now, 100.25, Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.Search(It.IsAny<Expression<Func<Veiculo, bool>>>()))
                .ReturnsAsync(new List<Veiculo>());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new Veiculo(command.AggregateId, "PLA7946", DateTime.Now, 100.25, Guid.NewGuid()));

            _autoMocker.GetMock<IVeiculoModeloRepository>()
               .Setup(x => x.Search(It.IsAny<Expression<Func<VeiculoModelo, bool>>>()))
               .ReturnsAsync(new List<VeiculoModelo>() { new VeiculoModelo(command.VeiculoModeloId, "Modelo", Guid.NewGuid()) });


            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoRepository>()
                .Verify(x => x.Update(It.IsAny<Veiculo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion

        #region RemoveVeiculoCommand
        [Fact]
        public async Task Handle_RemoveVeiculoCommand_ShouldFailValidation_WhenEmptyId()
        {
            // Arrange
            var command = new RemoveVeiculoCommand(Guid.Empty);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.RequiredField.Format("Id").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_RemoveVeiculoCommand_ShouldFailValidation_WhenVeiculoNotFound()
        {
            // Arrange
            var command = new RemoveVeiculoCommand(Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync((Veiculo)null);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(DomainMessages.NotFound.Format("Veiculo").Message, validationResult.Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Handle_RemoveVeiculoCommand_ShouldRemoveAndCommit_WhenValid()
        {
            // Arrange
            var command = new RemoveVeiculoCommand(Guid.NewGuid());

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.GetById(It.Is<Guid>(g => g.Equals(command.AggregateId))))
                .ReturnsAsync(new Veiculo(command.AggregateId, "PLA7946", DateTime.Now, 100.25, Guid.NewGuid()));

            _autoMocker.GetMock<IVeiculoRepository>()
                .Setup(x => x.UnitOfWork).Returns(_autoMocker.GetMock<IUnitOfWork>().Object);

            _autoMocker.GetMock<IUnitOfWork>()
                .Setup(x => x.Commit()).ReturnsAsync(true);

            // Act
            var validationResult = await _veiculoCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            _autoMocker.GetMock<IVeiculoRepository>()
                .Verify(x => x.Remove(It.IsAny<Veiculo>()), Times.Once);

            _autoMocker.GetMock<IUnitOfWork>()
                .Verify(x => x.Commit(), Times.Once);

            Assert.True(validationResult.IsValid);
        }
        #endregion
    }
}
