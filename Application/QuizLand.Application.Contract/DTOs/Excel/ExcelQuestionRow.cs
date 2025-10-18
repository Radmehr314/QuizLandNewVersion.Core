namespace QuizLand.Application.Contract.DTOs.Excel;

public sealed record ExcelQuestionRow(
    long CourseId,
    string QuestionText,
    string FirstOptionText,
    string SecondOptionText,
    string ThirdOptionText,
    string FourthOptionText,
    long FirstOptionClicked,
    long SecondOptionClicked,
    long ThirdOptionClicked,
    long FourthOptionClicked,  
    string DescriptiveAnswer,
    string Source,
    int CorrectIndex
);
