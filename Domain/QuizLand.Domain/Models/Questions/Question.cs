﻿using QuizLand.Domain.Models.Courses;

namespace QuizLand.Domain.Models.Questions;

public class Question : BaseEntity<long>
{
    public string QuestionTitle { get; set; }
    public string FirstOption { get; set; }
    public string SecondOption { get; set; }
    public string ThirdOption { get; set; }
    public string FourthOption { get; set; }
    public int CorrectOption { get; set; }
    public long? CountClickFirstOption { get; set; } = 0;
    public long? CountClickSecondOption { get; set; } = 0;
    public long? CountClickThirdOption { get; set; } = 0;
    public long? CountClickFourthOption { get; set; } = 0;
    public string? DescriptiveAnswer { get; set; }
    public string Source { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
}