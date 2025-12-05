using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SystemSoftware.Application.Common.Interfaces;
using SystemSoftware.Domain.Entities;
using SystemSoftware.Domain.Interfaces;

namespace SystemSoftware.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly Domain.Interfaces.IPasswordHasher _passwordHasher;

        public CreateUserCommandHandler(
            IApplicationDbContext context,
            Domain.Interfaces.IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = User.Create(
                request.Username,
                request.Email,
                request.Password,
                _passwordHasher);

            if (!string.IsNullOrEmpty(request.FirstName))
                user.FirstName = request.FirstName;

            if (!string.IsNullOrEmpty(request.LastName))
                user.LastName = request.LastName;

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
