

using QuizLand.Application.Contract.DTOs.Upload;
using QuizLand.Application.Contract.Framework;

namespace QuizLand.Application.Contract.Commands.Question;

public class ImportQuestionsFromExcelCommand : ICommand
{
    public UploadFileDto UploadFile { get; set; }
}