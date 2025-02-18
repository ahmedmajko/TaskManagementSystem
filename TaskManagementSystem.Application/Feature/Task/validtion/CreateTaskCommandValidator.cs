using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Feature.Task.Commands;

namespace TaskManagementSystem.Application.Feature.Task.validtion
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(v => v.Title)
                 .MaximumLength(200).WithMessage("Title is too long.")
                 .NotEmpty().WithMessage("Title is requaired.");
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Discription is requaired");
            RuleFor(v => v.Status)
                .MaximumLength(200)
                .NotEmpty().WithMessage("Status is requaird.");
            RuleFor(v => v.DueDate)
                .NotEmpty().WithMessage("you must enter date ");
        }
    }
}
