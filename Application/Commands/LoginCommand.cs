using BlogApi.Application.DTOs.Auth;
using BlogApi.Application.Interfaces.CQRS;
using MediatR;

namespace BlogApi.Application.Commands;

public record LoginCommand(string Username, string Password) : IRequest<(int, TokenDto?, string)>, ICommand;