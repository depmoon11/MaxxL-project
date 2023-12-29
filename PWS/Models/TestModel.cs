using FluentValidation;

namespace MaxL.Models
{
    public class MyModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool TF { get; set; }
    }
    public class MyModelValidator : AbstractValidator<MyModel>
        {
            public MyModelValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
                RuleFor(x => x.Age).GreaterThan(0).WithMessage("Surname must be greater than 0.");
                RuleFor(x => x.TF).Must(x => x == true).WithMessage("TF must be true.");
            }
        }

}
