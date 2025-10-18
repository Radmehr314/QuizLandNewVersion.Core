using ClosedXML.Excel;
using QuizLand.Application.Contract.Commands.Question;
using QuizLand.Application.Contract.Exceptions;
using QuizLand.Application.Contract.Framework;
using QuizLand.Application.Mapper;
using QuizLand.Domain;
using QuizLand.Domain.Models.Questions;

namespace QuizLand.Application.CommandHandler;

public class QuestionCommandHandler : ICommandHandler<AddQuestionCommand>, ICommandHandler<UpdateQuestionCommand>, ICommandHandler<DeleteQuestionCommand>,ICommandHandler<ImportQuestionsFromExcelCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public QuestionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CommandResult> Handle(AddQuestionCommand command)
    {
        var question = command.Factory();
        await _unitOfWork.QuestionRepository.Add(question);
        await _unitOfWork.Save();
        return new CommandResult(){Id = question.Id};
    }

    public async Task<CommandResult> Handle(UpdateQuestionCommand command)
    {
        var question =  await _unitOfWork.QuestionRepository.GetById(command.Id);
        question.QuestionTitle = command.QuestionTitle;
        question.FirstOption = command.FirstOption;
        question.SecondOption = command.SecondOption;
        question.ThirdOption = command.ThirdOption;
        question.FourthOption = command.FourthOption;
        question.CorrectOption = command.CorrectOption;
        question.CountClickFirstOption = command.CountClickFirstOption;
        question.CountClickSecondOption = command.CountClickSecondOption;
        question.CountClickThirdOption = command.CountClickThirdOption;
        question.CountClickFourthOption = command.CountClickFourthOption;
        question.DescriptiveAnswer = command.DescriptiveAnswer;
        question.Source = command.Source;
        question.CourseId = command.CourseId;
        await _unitOfWork.Save();
        return new CommandResult(){Id = question.Id};
    }

    public async Task<CommandResult> Handle(DeleteQuestionCommand command)
    {
        await _unitOfWork.QuestionRepository.Delete(command.Id);
        await _unitOfWork.Save();
        return new CommandResult(){Id = command.Id};
    }


    public async Task<CommandResult> Handle(ImportQuestionsFromExcelCommand command)
    {
        if (command?.UploadFile?.File == null || command.UploadFile.File.Length == 0)
            throw new ValidationException("فایل اکسل ارسال نشده است.");
        
        var file = command.UploadFile.File;
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (ext is not ".xlsx" and not ".xls")
            throw new ValidationException("تنها فایل‌های اکسل با پسوند xlsx/xls پشتیبانی می‌شوند.");
        
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        ms.Position = 0;
        
        using var wb = new XLWorkbook(ms);
        var ws = wb.Worksheets.Worksheet(1);              // Sheet اول
        var lastRow = ws.LastRowUsed()?.RowNumber() ?? 1; // اگر فقط هدر بود
        
        if (lastRow < 2)
            throw new ValidationException("هیچ سطر داده‌ای در فایل یافت نشد.");
        
        var rows = new List<Question>();

        for (int r = 2; r <= lastRow; r++)
        {
            // CourseId
            if (!long.TryParse(ws.Cell(r, 1).GetValue<string>()?.Trim(), out var courseId))
                throw new ValidationException($"ردیف {r}: CourseId نامعتبر است.");

            var questionText = ws.Cell(r, 2).GetString().Trim();
            var firstOption     = ws.Cell(r, 3).GetString().Trim();
            var secondOption     = ws.Cell(r, 4).GetString().Trim();
            var thirdOption     = ws.Cell(r, 5).GetString().Trim();
            var fourthOption     = ws.Cell(r, 6).GetString().Trim();
            var correct     = Convert.ToInt32(ws.Cell(r, 7).GetString().Trim());
            var firstOptionClicked     = Convert.ToInt64(ws.Cell(r, 8).GetString().Trim());
            var secondOptionClicked     = Convert.ToInt64(ws.Cell(r, 9).GetString().Trim());
            var thirdOptionClicked     = Convert.ToInt64(ws.Cell(r, 10).GetString().Trim());
            var fourthOptionClicked     = Convert.ToInt64(ws.Cell(r, 11).GetString().Trim());
            var descriptiveAnswer     = ws.Cell(r, 12).GetString().Trim();
            var source     = ws.Cell(r, 13).GetString().Trim();
            
            
            if (string.IsNullOrWhiteSpace(source))
                throw new ValidationException($"ردیف {r}: متن سؤال خالی است.");
            if (string.IsNullOrWhiteSpace(firstOption) || string.IsNullOrWhiteSpace(secondOption) ||
                string.IsNullOrWhiteSpace(thirdOption) || string.IsNullOrWhiteSpace(fourthOption))
                throw new ValidationException($"ردیف {r}: همه گزینه‌ها باید مقدار داشته باشند.");
            
            if (firstOptionClicked < 1 || secondOptionClicked < 1 || thirdOptionClicked < 1 || fourthOptionClicked < 1)
                throw new ValidationException($"ردیف {r}: همه تعداد کلیک ها باید مقدار داشته باشند.");
            
            if (correct is not (1 or 2 or 3 or 4))
                throw new ValidationException($"ردیف {r}: مقدار CorrectOption باید یکی از 1/2/3/4 باشد.");


            if (string.IsNullOrWhiteSpace(descriptiveAnswer))
                throw new ValidationException($"ردیف {r}: توضیحات سوال نامعتبر است.");


            rows.Add(new Question(){CourseId = courseId, QuestionTitle = questionText,FirstOption = firstOption,SecondOption = secondOption,ThirdOption = thirdOption,FourthOption = fourthOption,CorrectOption = correct,CountClickFirstOption = firstOptionClicked,CountClickSecondOption = secondOptionClicked,CountClickThirdOption = thirdOptionClicked,CountClickFourthOption = fourthOptionClicked,DescriptiveAnswer = descriptiveAnswer,Source = source});
        }

        await _unitOfWork.QuestionRepository.AddRange(rows);
        await _unitOfWork.Save();
        return new CommandResult {  };


    }
}