﻿namespace Core.Entities;

public class QuestionTag : BaseEntity
{
    public int QuestionId { get; set; }
    public int TagId { get; set; }
}