using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Avatar;

public class DeleteAvatarCommand : ICommand
{
    public long Id { get; set; }
}